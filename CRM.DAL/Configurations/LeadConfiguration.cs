using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.DAL.Configurations;

public class LeadConfiguration : IEntityTypeConfiguration<Lead>
{
    public void Configure(EntityTypeBuilder<Lead> builder)
    {
        builder.HasOne(l => l.Contact).WithOne().HasForeignKey<Lead>(l => l.ContactId);

        builder.HasOne(l => l.Seller).WithMany().HasForeignKey(l => l.SellerId);
    }
}