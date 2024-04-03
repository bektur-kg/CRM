using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Contracts.User;

public record UserRegisterRequest
{
    [StringLength(300)]
    public string? FullName { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    public required string Password { get; set; }

    public required UserRole Role { get; set; }
}
