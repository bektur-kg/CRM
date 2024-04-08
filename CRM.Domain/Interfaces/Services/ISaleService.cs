using CRM.Domain.Contracts.Sale;
using CRM.Domain.Results;

namespace CRM.Domain.Interfaces.Services;

public interface ISaleService
{
    Task<BaseResult<List<SaleResponse>>> GetAllSalesAsync();
    Task<BaseResult<List<SaleResponse>>> GetCurrentSellerSalesAsync();
    Task<BaseResult<SaleResponse>> CreateSale(long leadId);
}
