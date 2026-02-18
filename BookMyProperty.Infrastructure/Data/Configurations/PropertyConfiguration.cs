using BookMyProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyProperty.Infrastructure.Data.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> entity)
    {
        entity.HasKey(p => p.Id);

        entity.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);

        entity.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(2000);

        entity.Property(p => p.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        entity.Property(p => p.AreaSqFt)
            .IsRequired();

        entity.Property(p => p.Bedrooms)
            .IsRequired();

        entity.Property(p => p.Bathrooms)
            .IsRequired();

        entity.Property(p => p.Status)
            .IsRequired()
            .HasMaxLength(50);

        entity.HasOne(p => p.Agent)
            .WithMany(u => u.Properties)
            .HasForeignKey(p => p.AgentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
