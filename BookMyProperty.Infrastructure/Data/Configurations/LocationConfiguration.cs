using BookMyProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyProperty.Infrastructure.Data.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> entity)
    {
        entity.HasKey(l => l.Id);

        entity.Property(l => l.City)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(l => l.State)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(l => l.Country)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(l => l.ZipCode)
            .IsRequired()
            .HasMaxLength(20);

        entity.HasMany(l => l.Properties)
            .WithOne(p => p.Location)
            .HasForeignKey(p => p.LocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
