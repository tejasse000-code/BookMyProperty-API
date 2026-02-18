using BookMyProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyProperty.Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> entity)
    {
        entity.HasKey(r => r.Id);

        entity.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);

        entity.HasIndex(r => r.Name)
            .IsUnique();
    }
}
