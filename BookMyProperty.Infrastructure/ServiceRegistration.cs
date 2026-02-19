using BookMyProperty.Application.Features.Auth.Commands;
using BookMyProperty.Application.Features.Properties.Commands;
using BookMyProperty.Application.Features.Properties.Queries;
using BookMyProperty.Domain.Interfaces;
using BookMyProperty.Infrastructure.Data;
using BookMyProperty.Infrastructure.Repositories;
using BookMyProperty.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookMyProperty.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        string connectionString,
        string jwtSecret,
        string jwtIssuer,
        string jwtAudience,
        int jwtExpirationMinutes)
    {
        // Add DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                }));

        // Add repositories
        services.AddScoped(typeof(Repository<>));
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IPropertyQueryRepository, PropertyRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IAmenityRepository, AmenityRepository>();
        services.AddScoped<IPropertyTypeRepository, PropertyTypeRepository>();
        services.AddScoped<IContactInquiryRepository, ContactInquiryRepository>();
        services.AddScoped<IWishlistRepository, WishlistRepository>();
        services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // Add UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Add services
        services.AddScoped<IAuthService>(provider =>
            new AuthService(
                provider.GetRequiredService<ApplicationDbContext>(),
                jwtSecret,
                jwtIssuer,
                jwtAudience,
                jwtExpirationMinutes));

        return services;
    }
}
