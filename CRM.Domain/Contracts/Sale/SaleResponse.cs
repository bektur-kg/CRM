using CRM.Domain.Contracts.Lead;
using CRM.Domain.Contracts.User;

namespace CRM.Domain.Contracts.Sale;

public record SaleResponse
{
    public required long Id { get; set; }
    public LeadSaleResponse? Lead { get; set; }
    public UserResponse? Seller { get; set; }
    public required DateTime SoldDate { get; set; }
}
