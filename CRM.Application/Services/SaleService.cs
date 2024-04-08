using AutoMapper;
using CRM.DAL.DbContexts;
using CRM.Domain.Contracts.Sale;
using CRM.Domain.Entities;
using CRM.Domain.Interfaces.Services;
using CRM.Domain.Interfaces.Validations;
using CRM.Domain.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CRM.Application.Services;

public class SaleService : ISaleService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly HttpContext _httpContext;
    private readonly ISaleValidation _validation;

    public SaleService(AppDbContext dbContext, IMapper mapper, IHttpContextAccessor htppContextAccessor, ISaleValidation validation)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _httpContext = htppContextAccessor.HttpContext;
        _validation = validation;
    }

    public async Task<BaseResult<SaleResponse>> CreateSale(long leadId)
    {
        var lead = await _dbContext.Leads.AsNoTracking().FirstOrDefaultAsync(lead => lead.ContactId == leadId);

        var result = _validation.ValidateOnCreateSale(lead);

        if (!result.IsSuccess) return new BaseResult<SaleResponse> { ErrorMessage = result.ErrorMessage };

        var currentSellerId = long.Parse(_httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        var newSale = new Sale
        {
            LeadId = leadId,
            SellerId = currentSellerId,
            SoldDate = DateTime.UtcNow,
            Id = 0
        };

        await _dbContext.Sales.AddAsync(newSale);
        await _dbContext.SaveChangesAsync();
        var sale = await _dbContext.Sales
            .AsNoTracking()
            .Include(sale => sale.Seller)
            .Include(sale => sale.Lead)
            .FirstOrDefaultAsync(sale => sale.Id == newSale.Id);

        return new BaseResult<SaleResponse> { Data = _mapper.Map<SaleResponse>(sale) };
    }

    public async Task<BaseResult<List<SaleResponse>>> GetAllSalesAsync()
    {
        var sales = await _dbContext.Sales
            .AsNoTracking()
            .Include(sale => sale.Lead)
            .Include(sale => sale.Seller)
            .ToListAsync();

        return new BaseResult<List<SaleResponse>> { Data = _mapper.Map<List<SaleResponse>>(sales) };
    }

    public async Task<BaseResult<List<SaleResponse>>> GetCurrentSellerSalesAsync()
    {
        var currentSellerId = long.Parse(_httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        var sales = await _dbContext.Sales
            .AsNoTracking()
            .Where(sale => sale.Seller.Id == currentSellerId)
            .Include(sale => sale.Lead)
            .Include(sale => sale.Seller)
            .ToListAsync();

        return new BaseResult<List<SaleResponse>> { Data = _mapper.Map<List<SaleResponse>>(sales) };
    }
}
