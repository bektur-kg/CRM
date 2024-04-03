using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Contracts.User;

public record UserRegisterResponse
{
    public required long Id { get; set; }

    public string? FullName { get; set; }

    public required string Email { get; set; }

    public required UserRole Role { get; set; }

    public DateTime? BlockDate { get; set; }
}
