using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Entities;

public class Contact
{
    public required long Id { get; set; }

    public required long MarketerId { get; set; }

    public User? Marketer { get; set; }

    [StringLength(100)]
    public required string FirstName { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }

    [StringLength(100)]
    public string? Surname { get; set; }

    public required string PhoneNumber { get; set; }

    [StringLength(200)]
    public string? Email { get; set; }

    public required ContactStatus Status { get; set; }

    public required DateTime LastUpdated { get; set; }
}
