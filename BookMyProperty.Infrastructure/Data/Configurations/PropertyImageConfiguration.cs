using BookMyProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyProperty.Infrastructure.Data.Configurations;

public class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
{
    public void Configure(EntityTypeBuilder<PropertyImage> entity)
    {
        entity.HasKey(pi => pi.Id);

        entity.Property(pi => pi.ImageUrl)
            .IsRequired()
            .HasMaxLength(2000);

        entity.HasOne(pi => pi.Property)
            .WithMany(p => p.Images)
            .HasForeignKey(pi => pi.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
