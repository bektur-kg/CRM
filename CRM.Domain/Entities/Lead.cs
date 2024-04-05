using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Entities;

public class Lead
{
    [Key]
    public required long ContactId { get; set; }
    public Contact? Contact { get; set; }
    public long? SellerId { get; set; }
    public User? Seller { get; set; }
    public required LeadStatus Status { get; set; }
}
