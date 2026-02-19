using BookMyProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyProperty.Infrastructure.Data.Configurations;

public class PropertyTypeConfiguration : IEntityTypeConfiguration<PropertyType>
{
    public void Configure(EntityTypeBuilder<PropertyType> entity)
    {
        entity.HasKey(pt => pt.Id);

        entity.Property(pt => pt.Name)
            .IsRequired()
            .HasMaxLength(100);

        entity.HasMany(pt => pt.Properties)
            .WithOne(p => p.PropertyType)
            .HasForeignKey(p => p.PropertyTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
