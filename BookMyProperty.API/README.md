# BookMyProperty - Real Estate Backend API

A comprehensive RESTful API for a real estate property management platform built with ASP.NET Core 10, Entity Framework Core, and SQL Server.

## Project Overview

BookMyProperty is a robust backend system designed for managing real estate properties, user accounts, property listings, amenities, locations, wishlists, and inquiries. The system follows clean architecture principles with clear separation of concerns across Domain, Application, Infrastructure, and API layers.

## Project Structure

```
BookMyProperty/
??? BookMyProperty.Domain/           # Domain Layer (Entities, Interfaces, Enums)
?   ??? Entities/                    # Core domain entities
?   ??? Common/                      # Shared base classes
?   ??? Enums/                       # Domain enumerations
?   ??? Interfaces/                  # Repository and service interfaces
??? BookMyProperty.Application/      # Application Layer (DTOs, Business Logic)
?   ??? DTOs/                        # Data Transfer Objects
?   ??? Features/                    # CQRS Commands & Queries
?   ??? Validators/                  # FluentValidation validators
?   ??? Mappings/                    # AutoMapper profiles
?   ??? Exceptions/                  # Custom exceptions
??? BookMyProperty.Infrastructure/   # Infrastructure Layer (Data Access, Services)
?   ??? Data/                        # DbContext and configurations
?   ??? Repositories/                # Repository implementations
?   ??? Services/                    # Business services
?   ??? Migrations/                  # EF Core migrations
??? BookMyProperty.API/              # Presentation Layer (Controllers, Middleware)
    ??? Controllers/                 # API endpoints
    ??? Middleware/                  # HTTP middleware
    ??? Models/                      # API response models
    ??? Program.cs                   # Application entry point
```

## Technology Stack

- **Framework**: ASP.NET Core 10
- **Database**: SQL Server
- **ORM**: Entity Framework Core 10
- **Authentication**: JWT (JSON Web Tokens)
- **Mapping**: AutoMapper
- **Validation**: FluentValidation
- **Architecture**: Clean Architecture with CQRS pattern

## Features

### 1. Authentication & Authorization
- User registration and login with JWT token generation
- Role-based access control (Admin, Agent, User)
- Secure password hashing using BCrypt
- Token validation and expiration management

### 2. Property Management
- Create, read, update, and delete properties
- Property search with filters (location, type, price range)
- Pagination support for property listings
- Property status management (Available, Sold, etc.)
- Featured property management

### 3. Property Details
- **Property Types**: Apartment, House, Villa, Land, Commercial, Townhouse
- **Locations**: City, State, Country, ZipCode management
- **Amenities**: Pool, Gym, Parking, etc.
- **Property Images**: Multiple images per property with primary image selection
- **Property Metadata**: Area, bedrooms, bathrooms, parking spaces

### 4. User Features
- User profile management
- User role management (Admin, Agent, User)
- User activation/deactivation
- Multi-role support

### 5. Wishlist Management
- Add/remove properties from wishlist
- View personal wishlists
- Prevent duplicate entries in wishlist

### 6. Contact Inquiries
- Submit property inquiries
- Track inquiries by property
- Delete inquiries

## API Endpoints

### Authentication Endpoints
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login user

### Property Endpoints
- `GET /api/property` - Get all properties (paginated)
- `GET /api/property/{id}` - Get property by ID
- `GET /api/property/search` - Search properties with filters
- `POST /api/property` - Create new property (Authorized)
- `PUT /api/property/{id}` - Update property (Authorized)
- `DELETE /api/property/{id}` - Delete property (Authorized)

### Location Endpoints
- `GET /api/location` - Get all locations
- `GET /api/location/{id}` - Get location by ID
- `POST /api/location` - Create location (Authorized)
- `PUT /api/location/{id}` - Update location (Authorized)
- `DELETE /api/location/{id}` - Delete location (Authorized)

### Amenity Endpoints
- `GET /api/amenity` - Get all amenities
- `GET /api/amenity/{id}` - Get amenity by ID
- `POST /api/amenity` - Create amenity (Authorized)
- `PUT /api/amenity/{id}` - Update amenity (Authorized)
- `DELETE /api/amenity/{id}` - Delete amenity (Authorized)

### Property Type Endpoints
- `GET /api/propertytype` - Get all property types
- `GET /api/propertytype/{id}` - Get property type by ID
- `POST /api/propertytype` - Create property type (Authorized)
- `PUT /api/propertytype/{id}` - Update property type (Authorized)
- `DELETE /api/propertytype/{id}` - Delete property type (Authorized)

### Contact Inquiry Endpoints
- `GET /api/contactinquiry` - Get all inquiries (Authorized)
- `GET /api/contactinquiry/{id}` - Get inquiry by ID
- `GET /api/contactinquiry/property/{propertyId}` - Get inquiries for property
- `POST /api/contactinquiry` - Create contact inquiry
- `DELETE /api/contactinquiry/{id}` - Delete inquiry (Authorized)

### Wishlist Endpoints
- `GET /api/wishlist` - Get user's wishlist (Authorized)
- `GET /api/wishlist/{id}` - Get wishlist item by ID (Authorized)
- `POST /api/wishlist` - Add to wishlist (Authorized)
- `DELETE /api/wishlist/{id}` - Remove from wishlist (Authorized)

### Property Image Endpoints
- `GET /api/propertyimage/property/{propertyId}` - Get images for property
- `GET /api/propertyimage/{id}` - Get image by ID
- `POST /api/propertyimage` - Upload property image (Authorized)
- `PUT /api/propertyimage/{id}` - Update property image (Authorized)
- `DELETE /api/propertyimage/{id}` - Delete property image (Authorized)

### User Endpoints
- `GET /api/user` - Get all users (Authorized)
- `GET /api/user/{id}` - Get user by ID (Authorized)
- `GET /api/user/profile/me` - Get current user profile (Authorized)
- `DELETE /api/user/{id}` - Delete user (Authorized)

## Data Models

### Property
- Id, Title, Description, Price
- PropertyTypeId, LocationId, AgentId
- AreaSqFt, Bedrooms, Bathrooms, Parking
- Status, IsFeatured, IsDeleted
- CreatedDate, ModifiedDate, CreatedBy, ModifiedBy
- Navigation: PropertyType, Location, Agent, Images, PropertyAmenities, ContactInquiries

### User
- Id, FirstName, LastName, Email, PasswordHash
- PhoneNumber, RoleId, IsActive, IsDeleted
- CreatedDate, ModifiedDate, CreatedBy, ModifiedBy
- Navigation: Role, Properties, Wishlists

### Location
- Id, City, State, Country, ZipCode
- IsDeleted, CreatedDate, ModifiedDate

### PropertyType
- Id, Name
- IsDeleted, CreatedDate, ModifiedDate

### Amenity
- Id, Name
- IsDeleted, CreatedDate, ModifiedDate

### PropertyImage
- Id, ImageUrl, IsPrimary
- PropertyId, IsDeleted, CreatedDate, ModifiedDate

### ContactInquiry
- Id, Name, Email, Phone, Message
- PropertyId, IsDeleted, CreatedDate, ModifiedDate

### Wishlist
- Id, UserId, PropertyId
- IsDeleted, CreatedDate, ModifiedDate

## Setup Instructions

### Prerequisites
- .NET 10 SDK
- SQL Server 2019 or later
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the Repository**
```bash
git clone https://github.com/tejasse000-code/BookMyProperty.git
cd BookMyProperty
```

2. **Configure Database Connection**
Update `appsettings.json` with your SQL Server connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=BookMyPropertyDb;Trusted_Connection=true;TrustServerCertificate=true;"
  },
  "JwtSettings": {
    "Secret": "your_secret_key_here",
    "Issuer": "BookMyPropertyAPI",
    "Audience": "BookMyPropertyClient",
    "ExpirationMinutes": 60
  }
}
```

3. **Apply Database Migrations**
```bash
dotnet ef database update --project BookMyProperty.Infrastructure
```

4. **Build and Run**
```bash
dotnet build
dotnet run
```

The API will be available at `https://localhost:5001` (or your configured port).

## Authentication

### Login Example
```bash
curl -X POST "https://localhost:5001/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "password123"
  }'
```

**Response:**
```json
{
  "success": true,
  "message": "Login successful",
  "data": {
    "isSuccess": true,
    "token": "eyJhbGciOiJIUzI1NiIs...",
    "message": "Login successful."
  }
}
```

### Using JWT Token
Include the token in the Authorization header for protected endpoints:
```bash
curl -X GET "https://localhost:5001/api/property" \
  -H "Authorization: Bearer your_jwt_token_here"
```

## Response Format

All API responses follow a consistent format:

### Success Response
```json
{
  "success": true,
  "message": "Operation completed successfully",
  "data": {
    // Response data object
  }
}
```

### Error Response
```json
{
  "success": false,
  "message": "Error description",
  "data": null
}
```

## Error Handling

The API includes global exception handling middleware that catches and properly formats all exceptions:
- `NotFoundException` (404) - Resource not found
- `ValidationException` (400) - Validation errors
- `UnauthorizedAccessException` (401) - Authentication/Authorization failures
- Generic `Exception` (500) - Internal server errors

## Validation

Input validation is performed using FluentValidation with validators for:
- CreatePropertyDto
- AuthDto
- RegisterDto
- And other DTOs as needed

## Database Migrations

### Create Migration
```bash
dotnet ef migrations add MigrationName --project BookMyProperty.Infrastructure
```

### Update Database
```bash
dotnet ef database update --project BookMyProperty.Infrastructure
```

### Remove Latest Migration
```bash
dotnet ef migrations remove --project BookMyProperty.Infrastructure
```

## Best Practices Implemented

1. **Clean Architecture** - Separation of concerns across layers
2. **CQRS Pattern** - Separate command and query operations
3. **Repository Pattern** - Abstraction for data access
4. **Unit of Work Pattern** - Transaction management
5. **Dependency Injection** - Loose coupling and testability
6. **AutoMapper** - DTO to Entity mapping
7. **Global Exception Handling** - Centralized error handling
8. **Soft Deletes** - Logical deletion of records
9. **Audit Fields** - Track creation and modification timestamps
10. **JWT Authentication** - Secure token-based authentication

## Future Enhancements

- [ ] Add property ratings and reviews
- [ ] Implement messaging system between agents and buyers
- [ ] Add property viewing schedule management
- [ ] Implement admin dashboard analytics
- [ ] Add property export functionality (PDF, CSV)
- [ ] Implement email notifications
- [ ] Add property virtual tours support
- [ ] Implement advanced search filters
- [ ] Add transaction history tracking
- [ ] Implement payment gateway integration

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For support, email support@bookmyproperty.com or open an issue in the GitHub repository.

## Authors

- Development Team: BookMyProperty Team
- Repository: https://github.com/tejasse000-code/BookMyProperty

---

**Last Updated**: January 2025
**Version**: 1.0.0
