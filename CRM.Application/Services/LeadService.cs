using CRM.Domain.Contracts.Lead;

namespace CRM.Application.Services;

public class LeadService : ILeadService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILeadValidation _validation;
    private readonly HttpContext _httpContext;

    public LeadService(AppDbContext dbContext, IMapper mapper, ILeadValidation validation, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validation = validation;
        _httpContext = httpContextAccessor.HttpContext;
    }

    public async Task<BaseResult<LeadResponse>> ChangeLeadStatusAsync(long leadId, LeadStatus newStatus)
    {
        var lead = await _dbContext.Leads
            .Include(lead => lead.Contact)
            .Include(lead => lead.Seller)
            .FirstOrDefaultAsync(lead => lead.ContactId == leadId);

        var result = _validation.ValidateOnLeadStatusChange(lead, newStatus);

        if (!result.IsSuccess) return new BaseResult<LeadResponse> { ErrorMessage = result.ErrorMessage };

        lead.Status = newStatus;
        await _dbContext.SaveChangesAsync();

        return new BaseResult<LeadResponse> { Data = _mapper.Map<LeadResponse>(lead) };
    }

    public async Task<BaseResult<LeadResponse>> CreateLeadAsync(LeadCreateRequest requestDto)
    {
        var doesLeadExist = await _dbContext.Leads.AsNoTracking().AnyAsync(lead => lead.ContactId == requestDto.ContactId);
        var doesContactExist = await _dbContext.Contacts.AsNoTracking().AnyAsync(contact => contact.Id == requestDto.ContactId);

        var result = _validation.ValidateOnCreateLead(doesLeadExist, doesContactExist, requestDto.Status);

        if (!result.IsSuccess) return new BaseResult<LeadResponse> { ErrorMessage = result.ErrorMessage};

        var newLead = _mapper.Map<Lead>(requestDto);
        newLead.SellerId = long.Parse(_httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        await _dbContext.Leads.AddAsync(newLead);
        await _dbContext.SaveChangesAsync();

        var lead = await _dbContext.Leads
            .AsNoTracking()
            .Include(lead => lead.Contact)
            .Include(lead => lead.Seller)
            .FirstOrDefaultAsync(lead => lead.ContactId == newLead.ContactId);

        return new BaseResult<LeadResponse> { Data = _mapper.Map<LeadResponse>(lead) };
    }

    public async Task<BaseResult<List<LeadResponse>>> GetAllLeadsAsync()
    {
        var leads = await _dbContext.Leads
            .AsNoTracking()
            .Include(lead => lead.Contact)
            .Include(lead => lead.Seller)
            .Include(lead => lead.Contact.Marketer)
            .ToListAsync();

        return new BaseResult<List<LeadResponse>> { Data = _mapper.Map<List<LeadResponse>>(leads) };
    }
}
