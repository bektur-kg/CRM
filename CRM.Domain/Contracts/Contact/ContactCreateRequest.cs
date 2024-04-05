using CRM.Domain.Constants;
using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Contracts.Contact;

public record ContactCreateRequest
{
    [StringLength(AttributeConstants.CONTACT_FIRST_NAME_LENGTH)]
    public required string FirstName { get; set; }

    [StringLength(AttributeConstants.CONTACT_LAST_NAME_LENGTH)]
    public string? LastName { get; set; }

    [StringLength(AttributeConstants.CONTACT_SURNAME_LENGTH)]
    public string? Surname { get; set; }

    [Phone]
    public required string PhoneNumber { get; set; }

    [StringLength(AttributeConstants.EMAIL_LENGTH)]
    [EmailAddress]  
    public string? Email { get; set; }

    public required ContactStatus Status { get; set; }
}
