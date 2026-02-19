# BookMyProperty API - Quick Start Guide

## ?? Getting Started in 5 Minutes

### Prerequisites
- .NET 10 SDK installed
- SQL Server 2019+ installed
- Visual Studio 2022 or VS Code

### Step 1: Clone Repository
```bash
git clone https://github.com/tejasse000-code/BookMyProperty.git
cd BookMyProperty
```

### Step 2: Configure Database
Edit `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BookMyPropertyDb;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### Step 3: Create Database
```bash
dotnet ef database update --project BookMyProperty.Infrastructure
```

### Step 4: Run the Application
```bash
dotnet run
```

The API will be available at `https://localhost:5001`

---

## ?? Quick API Test

### 1. Register a User
```bash
curl -X POST "https://localhost:5001/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Password123!",
    "firstName": "John",
    "lastName": "Doe",
    "phoneNumber": "+1234567890"
  }'
```

Copy the `token` from response.

### 2. Create a Property
```bash
curl -X POST "https://localhost:5001/api/property" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Beautiful House",
    "description": "3 bed house with garden",
    "price": 350000,
    "location": "New York",
    "propertyType": 2,
    "area": 2500,
    "bedrooms": 3,
    "bathrooms": 2
  }'
```

### 3. Search Properties
```bash
curl -X GET "https://localhost:5001/api/property" \
  -H "Content-Type: application/json"
```

---

## ?? Project Structure

```
BookMyProperty/
??? Domain/              # Entities, Interfaces
??? Application/         # DTOs, Business Logic
??? Infrastructure/      # Database, Repositories
??? API/                # Controllers, Middleware
```

---

## ??? Available Commands

### Build Project
```bash
dotnet build
```

### Run Project
```bash
dotnet run
```

### Run Tests
```bash
dotnet test
```

### Create Migration
```bash
dotnet ef migrations add MigrationName --project BookMyProperty.Infrastructure
```

### Update Database
```bash
dotnet ef database update --project BookMyProperty.Infrastructure
```

### Reset Database
```bash
dotnet ef database drop --project BookMyProperty.Infrastructure
dotnet ef database update --project BookMyProperty.Infrastructure
```

---

## ?? API Endpoints

### Most Used Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/auth/register | Register user |
| POST | /api/auth/login | Login user |
| GET | /api/property | List properties |
| GET | /api/property/{id} | Get property |
| GET | /api/property/search | Search properties |
| POST | /api/property | Create property |
| PUT | /api/property/{id} | Update property |
| DELETE | /api/property/{id} | Delete property |
| GET | /api/wishlist | Get wishlist |
| POST | /api/wishlist | Add to wishlist |
| DELETE | /api/wishlist/{id} | Remove from wishlist |
| POST | /api/contactinquiry | Create inquiry |

---

## ?? Authentication

All endpoints marked as "Authorized" require JWT token in header:

```bash
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

---

## ??? Database Entities

- **Users** - User accounts with roles
- **Properties** - Real estate listings
- **Locations** - Geographic locations
- **PropertyTypes** - Classification (Apartment, House, etc.)
- **Amenities** - Features (Pool, Gym, etc.)
- **PropertyImages** - Multiple images per property
- **Wishlists** - User favorites
- **ContactInquiries** - Property inquiries
- **Roles** - User roles (Admin, Agent, User)

---

## ?? Common Tasks

### Add a New Property Type
```bash
POST /api/propertytype
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Townhouse"
}
```

### Search by Location
```bash
GET /api/property/search?location=NewYork&pageNumber=1&pageSize=10
```

### Get User Profile
```bash
GET /api/user/profile/me
Authorization: Bearer {token}
```

---

## ? Troubleshooting

### Database Connection Error
- Verify SQL Server is running
- Check connection string in appsettings.json
- Ensure database user has proper permissions

### JWT Token Errors
- Verify token is included in Authorization header
- Check token hasn't expired
- Ensure token format is: `Bearer {token}`

### Port Already in Use
```bash
netstat -ano | findstr :5001
taskkill /PID {process_id} /F
```

---

## ?? Documentation

- **README.md** - Full project documentation
- **API_DOCUMENTATION.md** - API endpoint details
- **IMPLEMENTATION_SUMMARY.md** - Implementation details

---

## ?? Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

---

## ?? Support

For issues or questions:
- Create a GitHub issue
- Email: support@bookmyproperty.com

---

## ?? License

MIT License - See LICENSE file

---

## ? Verification Checklist

Before deployment, verify:
- [ ] All projects build successfully
- [ ] Database migrations are applied
- [ ] JWT settings configured in appsettings.json
- [ ] Connection string is correct
- [ ] API endpoints are responding
- [ ] Authentication is working
- [ ] Database operations are functional

---

## ?? Next Steps

1. Test all API endpoints
2. Set up logging and monitoring
3. Configure production database
4. Implement rate limiting
5. Add unit tests
6. Set up CI/CD pipeline

---

**Ready to go!** ??

Questions? Check the full documentation in README.md or API_DOCUMENTATION.md
