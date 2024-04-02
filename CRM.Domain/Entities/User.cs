using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Entities;

public class User
{
    public required long Id { get; set; }

    [StringLength(300)]
    public string? FullName { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    public required string PassordHash { get; set; }

    public required Role Role { get; set; }

    public DateTime? BlockDate { get; set; }
}
