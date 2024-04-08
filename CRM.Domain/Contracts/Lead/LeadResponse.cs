using CRM.Domain.Contracts.Contact;
using CRM.Domain.Contracts.User;

namespace CRM.Domain.Contracts.Lead;

public record LeadResponse
{
    public ContactResponse? Contact { get; set; }
    public LeadUserResponse? Seller { get; set; }
    public required LeadStatus Status { get; set; }
}
