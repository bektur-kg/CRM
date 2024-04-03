namespace CRM.Domain.Entities;

public class Sale
{
    public required long Id { get; set; }
    public required long LeadId { get; set; }
    public Lead? Lead { get; set; }
    public long? SellerId { get; set; }
    public User? Seller { get; set; }
    public required DateTime SoldDate { get; set; }
}
