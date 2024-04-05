using CRM.Domain.Constants;
using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Entities;

public class User
{
    public required long Id { get; set; }

    [StringLength(AttributeConstants.USER_FULL_NAME_LENGTH)]
    public string? FullName { get; set; }

    [StringLength(AttributeConstants.EMAIL_LENGTH)]
    public required string Email { get; set; }

    public required string PasswordHash {  get; set; }

    public required UserRole Role { get; set; }

    public DateTime? BlockDate { get; set; }
}
