using CRM.Domain.Enums;

namespace CRM.Domain.Contracts.Lead;

public record LeadSaleResponse
{
    public required long ContactId { get; set; }
    public long? SellerId { get; set; }
    public required LeadStatus Status { get; set; }
}
