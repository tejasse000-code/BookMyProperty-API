using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookMyProperty.Domain.Entities;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(u => u.Id);

        entity.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);

        entity.HasIndex(u => u.Email)
            .IsUnique();

        entity.Property(u => u.PasswordHash)
            .IsRequired();

        entity.Property(u => u.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        entity.Property(u => u.IsActive)
            .HasDefaultValue(true);

        entity.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
