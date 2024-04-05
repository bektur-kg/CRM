using CRM.Domain.Constants;
using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Contracts.User;

public record UserRegisterRequest
{
    [StringLength(AttributeConstants.USER_FULL_NAME_LENGTH)]
    public string? FullName { get; set; }

    [EmailAddress]
    [StringLength(AttributeConstants.EMAIL_LENGTH)]
    public required string Email { get; set; }

    public required string Password { get; set; }

    public required UserRole Role { get; set; }
}
