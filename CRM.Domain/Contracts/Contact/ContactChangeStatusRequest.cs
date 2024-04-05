using CRM.Domain.Enums;

namespace CRM.Domain.Contracts.Contact;

public record ContactChangeStatusRequest
{
    public ContactStatus NewContactStatus { get; set; }
    public long ContactId { get; set; }
}
