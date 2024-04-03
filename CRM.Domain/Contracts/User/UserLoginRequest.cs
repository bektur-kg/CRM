using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Contracts.User;

public record UserLoginRequest
{
    [EmailAddress]
    public required string Email { get; set; }

    public required string Password { get; set; }
}
