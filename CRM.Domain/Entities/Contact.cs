namespace CRM.Domain.Entities;

public class Contact
{
    public required long Id { get; set; }

    public required long MarketerId { get; set; }

    public User? Marketer { get; set; }

    [StringLength(AttributeConstants.CONTACT_FIRST_NAME_LENGTH)]
    public required string FirstName { get; set; }

    [StringLength(AttributeConstants.CONTACT_LAST_NAME_LENGTH)]
    public string? LastName { get; set; }

    [StringLength(AttributeConstants.CONTACT_SURNAME_LENGTH)]
    public string? Surname { get; set; }

    public required string PhoneNumber { get; set; }

    [StringLength(AttributeConstants.EMAIL_LENGTH)]
    public string? Email { get; set; }

    public required ContactStatus Status { get; set; }

    public required DateTime LastUpdated { get; set; }
}
