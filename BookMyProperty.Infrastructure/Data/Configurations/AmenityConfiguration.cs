using BookMyProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyProperty.Infrastructure.Data.Configurations;

public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
{
    public void Configure(EntityTypeBuilder<Amenity> entity)
    {
        entity.HasKey(a => a.Id);

        entity.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        entity.HasMany(a => a.PropertyAmenities)
            .WithOne(pa => pa.Amenity)
            .HasForeignKey(pa => pa.AmenityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
