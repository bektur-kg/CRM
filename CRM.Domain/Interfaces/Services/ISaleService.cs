using CRM.Domain.Contracts.Sale;

namespace CRM.Domain.Interfaces.Services;

public interface ISaleService
{
    Task<BaseResult<List<SaleResponse>>> GetAllSalesAsync();
    Task<BaseResult<List<SaleResponse>>> GetCurrentSellerSalesAsync();
    Task<BaseResult<SaleResponse>> CreateSale(long leadId);
}
