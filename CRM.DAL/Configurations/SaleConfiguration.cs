namespace CRM.DAL.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder
            .HasOne(s => s.Seller)
            .WithMany()
            .HasForeignKey(s => s.SellerId);
    }
}
