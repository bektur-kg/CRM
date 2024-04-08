using CRM.Domain.Contracts.User;

namespace CRM.Domain.Contracts.Contact;

public record ContactResponse
{
    public required long Id { get; set; }

    public ContactUserResponse? Marketer { get; set; }

    public required string FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Surname { get; set; }

    public required string PhoneNumber { get; set; }

    public string? Email { get; set; }

    public required ContactStatus Status { get; set; }

    public required DateTime LastUpdated { get; set; }
}
