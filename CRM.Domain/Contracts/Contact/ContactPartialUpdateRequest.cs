namespace CRM.Domain.Contracts.Contact;

public record ContactPartialUpdateRequest
{
    public long? MarketerId { get; set; }

    [StringLength(AttributeConstants.CONTACT_FIRST_NAME_LENGTH)]
    public string? FirstName { get; set; }

    [StringLength(AttributeConstants.CONTACT_LAST_NAME_LENGTH)]
    public string? LastName { get; set; }

    [StringLength(AttributeConstants.CONTACT_SURNAME_LENGTH)]
    public string? Surname { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    [StringLength(AttributeConstants.EMAIL_LENGTH)]
    [EmailAddress]
    public string? Email { get; set; }

    public ContactStatus? Status { get; set; }
}
