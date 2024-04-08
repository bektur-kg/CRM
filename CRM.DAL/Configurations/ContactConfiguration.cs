namespace CRM.DAL.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder
            .HasOne(c => c.Marketer)
            .WithMany()
            .HasForeignKey(c => c.MarketerId);
    }
}
