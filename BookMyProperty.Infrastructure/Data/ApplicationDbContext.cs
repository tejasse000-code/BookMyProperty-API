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
        // Seed Roles
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

        // Seed Users (Admin + Agents)
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
            },
            new User
            {
                Id = 2,
                FirstName = "John",
                LastName = "Smith",
                Email = "john.smith@bookmyproperty.com",
                PhoneNumber = "+919876543210",
                PasswordHash = adminPasswordHash,
                RoleId = 2,
                IsActive = true,
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new User
            {
                Id = 3,
                FirstName = "Sarah",
                LastName = "Johnson",
                Email = "sarah.johnson@bookmyproperty.com",
                PhoneNumber = "+918765432109",
                PasswordHash = adminPasswordHash,
                RoleId = 2,
                IsActive = true,
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            }
        );

        // Seed Property Types
        modelBuilder.Entity<PropertyType>().HasData(
            new PropertyType
            {
                Id = 1,
                Name = "Apartment",
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new PropertyType
            {
                Id = 2,
                Name = "House",
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new PropertyType
            {
                Id = 3,
                Name = "Villa",
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new PropertyType
            {
                Id = 4,
                Name = "Penthouse",
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            }
        );

        // Seed Locations
        modelBuilder.Entity<Location>().HasData(
            new Location
            {
                Id = 1,
                City = "Mumbai",
                State = "Maharashtra",
                Country = "India",
                ZipCode = "400001",
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new Location
            {
                Id = 2,
                City = "Bangalore",
                State = "Karnataka",
                Country = "India",
                ZipCode = "560001",
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new Location
            {
                Id = 3,
                City = "Delhi",
                State = "Delhi",
                Country = "India",
                ZipCode = "110001",
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new Location
            {
                Id = 4,
                City = "Pune",
                State = "Maharashtra",
                Country = "India",
                ZipCode = "411001",
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            }
        );

        // Seed Amenities
        modelBuilder.Entity<Amenity>().HasData(
            new Amenity
            {
                Id = 1,
                Name = "Swimming Pool",
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new Amenity
            {
                Id = 2,
                Name = "Gym",
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new Amenity
            {
                Id = 3,
                Name = "Parking",
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new Amenity
            {
                Id = 4,
                Name = "Security",
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new Amenity
            {
                Id = 5,
                Name = "Garden",
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new Amenity
            {
                Id = 6,
                Name = "Balcony",
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            }
        );

        // Seed Properties
        modelBuilder.Entity<Property>().HasData(
            new Property
            {
                Id = 1,
                Title = "Modern Apartment in Downtown Mumbai",
                Description = "Beautiful 3 BHK apartment with all modern amenities and sea view.",
                Price = 15000000,
                PropertyTypeId = 1,
                LocationId = 1,
                AreaSqFt = 1500,
                Bedrooms = 3,
                Bathrooms = 2,
                Parking = 2,
                Status = "Available",
                IsFeatured = true,
                AgentId = 2,
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new Property
            {
                Id = 2,
                Title = "Luxury Villa in Bangalore",
                Description = "Spacious 5 BHK villa with private pool and garden.",
                Price = 25000000,
                PropertyTypeId = 3,
                LocationId = 2,
                AreaSqFt = 3500,
                Bedrooms = 5,
                Bathrooms = 4,
                Parking = 3,
                Status = "Available",
                IsFeatured = true,
                AgentId = 3,
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new Property
            {
                Id = 3,
                Title = "Cozy House in Delhi",
                Description = "Well-maintained 2 BHK house in a peaceful locality.",
                Price = 8000000,
                PropertyTypeId = 2,
                LocationId = 3,
                AreaSqFt = 1200,
                Bedrooms = 2,
                Bathrooms = 2,
                Parking = 1,
                Status = "Available",
                IsFeatured = false,
                AgentId = 2,
                CreatedDate = new DateTime(2025, 2, 2),
                IsDeleted = false
            },
            new Property
            {
                Id = 4,
                Title = "Premium Penthouse in Pune",
                Description = "Ultra-modern penthouse with panoramic city views.",
                Price = 20000000,
                PropertyTypeId = 4,
                LocationId = 4,
                AreaSqFt = 2800,
                Bedrooms = 4,
                Bathrooms = 3,
                Parking = 2,
                Status = "Available",
                IsFeatured = true,
                AgentId = 3,
                CreatedDate = new DateTime(2025, 2, 2),
                IsDeleted = false
            },
            new Property
            {
                Id = 5,
                Title = "Budget-Friendly Apartment in Mumbai",
                Description = "Affordable 1 BHK apartment near railway station.",
                Price = 4500000,
                PropertyTypeId = 1,
                LocationId = 1,
                AreaSqFt = 600,
                Bedrooms = 1,
                Bathrooms = 1,
                Parking = 0,
                Status = "Available",
                IsFeatured = false,
                AgentId = 2,
                CreatedDate = new DateTime(2025, 2, 3),
                IsDeleted = false
            }
        );

        // Seed Property Images
        modelBuilder.Entity<PropertyImage>().HasData(
            new PropertyImage
            {
                Id = 1,
                ImageUrl = "https://via.placeholder.com/800x600?text=Modern+Apartment+View1",
                IsPrimary = true,
                PropertyId = 1,
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new PropertyImage
            {
                Id = 2,
                ImageUrl = "https://via.placeholder.com/800x600?text=Modern+Apartment+View2",
                IsPrimary = false,
                PropertyId = 1,
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new PropertyImage
            {
                Id = 3,
                ImageUrl = "https://via.placeholder.com/800x600?text=Luxury+Villa+View1",
                IsPrimary = true,
                PropertyId = 2,
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new PropertyImage
            {
                Id = 4,
                ImageUrl = "https://via.placeholder.com/800x600?text=Luxury+Villa+View2",
                IsPrimary = false,
                PropertyId = 2,
                CreatedDate = new DateTime(2025, 2, 1),
                IsDeleted = false
            },
            new PropertyImage
            {
                Id = 5,
                ImageUrl = "https://via.placeholder.com/800x600?text=House+View1",
                IsPrimary = true,
                PropertyId = 3,
                CreatedDate = new DateTime(2025, 2, 2),
                IsDeleted = false
            },
            new PropertyImage
            {
                Id = 6,
                ImageUrl = "https://via.placeholder.com/800x600?text=Penthouse+View1",
                IsPrimary = true,
                PropertyId = 4,
                CreatedDate = new DateTime(2025, 2, 2),
                IsDeleted = false
            }
        );

        // Seed Property Amenities
        modelBuilder.Entity<PropertyAmenity>().HasData(
            new PropertyAmenity { PropertyId = 1, AmenityId = 1 },
            new PropertyAmenity { PropertyId = 1, AmenityId = 2 },
            new PropertyAmenity { PropertyId = 1, AmenityId = 3 },
            new PropertyAmenity { PropertyId = 1, AmenityId = 4 },
            new PropertyAmenity { PropertyId = 2, AmenityId = 1 },
            new PropertyAmenity { PropertyId = 2, AmenityId = 2 },
            new PropertyAmenity { PropertyId = 2, AmenityId = 5 },
            new PropertyAmenity { PropertyId = 3, AmenityId = 3 },
            new PropertyAmenity { PropertyId = 3, AmenityId = 4 },
            new PropertyAmenity { PropertyId = 4, AmenityId = 1 },
            new PropertyAmenity { PropertyId = 4, AmenityId = 2 },
            new PropertyAmenity { PropertyId = 4, AmenityId = 6 }
        );
    }
}
