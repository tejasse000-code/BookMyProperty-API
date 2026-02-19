using BookMyProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyProperty.Infrastructure.Data.Configurations;

public class ContactInquiryConfiguration : IEntityTypeConfiguration<ContactInquiry>
{
    public void Configure(EntityTypeBuilder<ContactInquiry> entity)
    {
        entity.HasKey(ci => ci.Id);

        entity.Property(ci => ci.Name)
            .IsRequired()
            .HasMaxLength(200);

        entity.Property(ci => ci.Email)
            .IsRequired()
            .HasMaxLength(255);

        entity.Property(ci => ci.Phone)
            .IsRequired()
            .HasMaxLength(20);

        entity.Property(ci => ci.Message)
            .IsRequired()
            .HasMaxLength(2000);

        entity.HasOne(ci => ci.Property)
            .WithMany(p => p.ContactInquiries)
            .HasForeignKey(ci => ci.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
