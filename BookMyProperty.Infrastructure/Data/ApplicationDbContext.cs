using BookMyProperty.Domain.Common;
using BookMyProperty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookMyProperty.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<PropertyType> PropertyTypes => Set<PropertyType>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<PropertyImage> PropertyImages => Set<PropertyImage>();
    public DbSet<Amenity> Amenities => Set<Amenity>();
    public DbSet<PropertyAmenity> PropertyAmenities => Set<PropertyAmenity>();
    public DbSet<ContactInquiry> ContactInquiries => Set<ContactInquiry>();
    public DbSet<Wishlist> Wishlists => Set<Wishlist>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all Fluent Configurations automatically
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Global Soft Delete Filter
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");

                var propertyMethod = typeof(EF)
                    .GetMethod(nameof(EF.Property))!
                    .MakeGenericMethod(typeof(bool));

                var isDeletedProperty = Expression.Call(
                    propertyMethod,
                    parameter,
                    Expression.Constant("IsDeleted")
                );

                var compareExpression = Expression.Equal(
                    isDeletedProperty,
                    Expression.Constant(false)
                );

                var lambda = Expression.Lambda(compareExpression, parameter);

                modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter(lambda);
            }
        }

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = 1,
                Name = "Admin",
                CreatedDate = new DateTime(2025, 1, 1),
                IsDeleted = false
            },
            new Role
            {
                Id = 2,
                Name = "Agent",
                CreatedDate = new DateTime(2025, 1, 1),
                IsDeleted = false
            },
            new Role
            {
                Id = 3,
                Name = "User",
                CreatedDate = new DateTime(2025, 1, 1),
                IsDeleted = false
            }
        );

        var adminPasswordHash =
            "$2a$11$9fF5Qw1F6qH8nVJX6Vx9bOZqVxUuD9nC2RjF5eTqK8JXk9bVxYwzW";

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FirstName = "System",
                LastName = "Administrator",
                Email = "admin@bookmyproperty.com",
                PhoneNumber = "+911234567890",
                PasswordHash = adminPasswordHash,
                RoleId = 1,
                IsActive = true,
                CreatedDate = new DateTime(2025, 1, 1),
                IsDeleted = false
            }
        );
    }
}
