using CRM.Domain.Enums;

namespace CRM.Domain.Contracts.Lead;

public record LeadCreateRequest
{
    public required long ContactId { get; set; }
    public required LeadStatus Status { get; set; }
}
