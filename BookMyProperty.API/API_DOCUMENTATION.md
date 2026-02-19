# BookMyProperty API Documentation

## Base URL
```
https://localhost:5001/api
```

## Authentication
The API uses JWT (JSON Web Token) for authentication. Include the token in the `Authorization` header:
```
Authorization: Bearer {token}
```

---

## Authentication Endpoints

### 1. Register User
**Endpoint:** `POST /auth/register`

**Description:** Register a new user account

**Request Body:**
```json
{
  "email": "user@example.com",
  "password": "SecurePassword123",
  "firstName": "John",
  "lastName": "Doe",
  "phoneNumber": "+1234567890"
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Registration successful",
  "data": {
    "isSuccess": true,
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "message": "Registration successful."
  }
}
```

**Error Response (400 Bad Request):**
```json
{
  "success": false,
  "message": "Email already registered."
}
```

---

### 2. Login User
**Endpoint:** `POST /auth/login`

**Description:** Authenticate user and receive JWT token

**Request Body:**
```json
{
  "email": "user@example.com",
  "password": "SecurePassword123"
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Login successful",
  "data": {
    "isSuccess": true,
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "message": "Login successful."
  }
}
```

**Error Response (401 Unauthorized):**
```json
{
  "success": false,
  "message": "Invalid email or password."
}
```

---

## Property Endpoints

### 1. Get All Properties
**Endpoint:** `GET /property?pageNumber=1&pageSize=10`

**Description:** Get paginated list of available properties

**Query Parameters:**
- `pageNumber` (optional, default: 1): Page number
- `pageSize` (optional, default: 10): Number of items per page

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Properties retrieved successfully",
  "data": {
    "items": [
      {
        "id": 1,
        "title": "Beautiful House in Downtown",
        "description": "3 bedroom house with spacious garden",
        "price": 350000,
        "location": "New York, NY",
        "propertyType": 2,
        "imageUrl": "https://...",
        "area": 2500,
        "bedrooms": 3,
        "bathrooms": 2,
        "isAvailable": true,
        "createdDate": "2025-01-15T10:30:00Z"
      }
    ],
    "pageNumber": 1,
    "pageSize": 10,
    "total": 50,
    "totalPages": 5
  }
}
```

---

### 2. Get Property by ID
**Endpoint:** `GET /property/{id}`

**Description:** Get detailed information about a specific property

**Path Parameters:**
- `id` (required): Property ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Property retrieved successfully",
  "data": {
    "id": 1,
    "title": "Beautiful House in Downtown",
    "description": "3 bedroom house with spacious garden",
    "price": 350000,
    "location": "New York, NY",
    "propertyType": 2,
    "imageUrl": "https://...",
    "area": 2500,
    "bedrooms": 3,
    "bathrooms": 2,
    "isAvailable": true,
    "createdDate": "2025-01-15T10:30:00Z"
  }
}
```

**Error Response (404 Not Found):**
```json
{
  "success": false,
  "message": "Property not found"
}
```

---

### 3. Search Properties
**Endpoint:** `GET /property/search?location=NewYork&propertyType=2&minPrice=100000&maxPrice=500000&pageNumber=1&pageSize=10`

**Description:** Search properties with multiple filters

**Query Parameters:**
- `location` (optional): City name or location string
- `propertyType` (optional): Property type ID
- `minPrice` (optional): Minimum price filter
- `maxPrice` (optional): Maximum price filter
- `pageNumber` (optional, default: 1): Page number
- `pageSize` (optional, default: 10): Items per page

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Properties searched successfully",
  "data": {
    "items": [
      // Array of matching properties
    ],
    "pageNumber": 1,
    "pageSize": 10,
    "total": 25,
    "totalPages": 3
  }
}
```

---

### 4. Create Property
**Endpoint:** `POST /property`

**Description:** Create a new property listing (Requires Authentication)

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Request Body:**
```json
{
  "title": "Modern Apartment",
  "description": "2 bed, 1 bath modern apartment",
  "price": 250000,
  "location": "Downtown",
  "propertyType": 1,
  "imageUrl": "https://...",
  "area": 1200,
  "bedrooms": 2,
  "bathrooms": 1
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Property created successfully",
  "data": {
    "id": 5,
    "title": "Modern Apartment",
    "description": "2 bed, 1 bath modern apartment",
    "price": 250000,
    "location": "Downtown",
    "propertyType": 1,
    "imageUrl": "https://...",
    "area": 1200,
    "bedrooms": 2,
    "bathrooms": 1,
    "isAvailable": true,
    "createdDate": "2025-01-20T14:30:00Z"
  }
}
```

---

### 5. Update Property
**Endpoint:** `PUT /property/{id}`

**Description:** Update an existing property (Requires Authentication)

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Path Parameters:**
- `id` (required): Property ID

**Request Body:**
```json
{
  "id": 1,
  "title": "Updated Property Title",
  "description": "Updated description",
  "price": 300000,
  "location": "Downtown",
  "propertyType": 2,
  "imageUrl": "https://...",
  "area": 2000,
  "bedrooms": 3,
  "bathrooms": 2,
  "isAvailable": true
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Property updated successfully",
  "data": {
    // Updated property object
  }
}
```

---

### 6. Delete Property
**Endpoint:** `DELETE /property/{id}`

**Description:** Delete a property (Requires Authentication)

**Headers:**
```
Authorization: Bearer {token}
```

**Path Parameters:**
- `id` (required): Property ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Property deleted successfully",
  "data": true
}
```

---

## Location Endpoints

### 1. Get All Locations
**Endpoint:** `GET /location`

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Locations retrieved successfully",
  "data": [
    {
      "id": 1,
      "city": "New York",
      "state": "NY",
      "country": "USA",
      "zipCode": "10001"
    }
  ]
}
```

### 2. Get Location by ID
**Endpoint:** `GET /location/{id}`

### 3. Create Location
**Endpoint:** `POST /location` (Requires Authentication)

**Request Body:**
```json
{
  "city": "Los Angeles",
  "state": "CA",
  "country": "USA",
  "zipCode": "90001"
}
```

### 4. Update Location
**Endpoint:** `PUT /location/{id}` (Requires Authentication)

### 5. Delete Location
**Endpoint:** `DELETE /location/{id}` (Requires Authentication)

---

## Amenity Endpoints

### 1. Get All Amenities
**Endpoint:** `GET /amenity`

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Amenities retrieved successfully",
  "data": [
    {
      "id": 1,
      "name": "Swimming Pool"
    },
    {
      "id": 2,
      "name": "Gym"
    }
  ]
}
```

### 2. Get Amenity by ID
**Endpoint:** `GET /amenity/{id}`

### 3. Create Amenity
**Endpoint:** `POST /amenity` (Requires Authentication)

**Request Body:**
```json
{
  "name": "Parking"
}
```

### 4. Update Amenity
**Endpoint:** `PUT /amenity/{id}` (Requires Authentication)

### 5. Delete Amenity
**Endpoint:** `DELETE /amenity/{id}` (Requires Authentication)

---

## Property Type Endpoints

### 1. Get All Property Types
**Endpoint:** `GET /propertytype`

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Property types retrieved successfully",
  "data": [
    {
      "id": 1,
      "name": "Apartment"
    },
    {
      "id": 2,
      "name": "House"
    }
  ]
}
```

### 2. Create Property Type
**Endpoint:** `POST /propertytype` (Requires Authentication)

**Request Body:**
```json
{
  "name": "Villa"
}
```

---

## Wishlist Endpoints

### 1. Get User's Wishlist
**Endpoint:** `GET /wishlist`

**Headers:**
```
Authorization: Bearer {token}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Wishlist retrieved successfully",
  "data": [
    {
      "id": 1,
      "userId": 1,
      "propertyId": 5,
      "property": {
        // Property details
      },
      "createdDate": "2025-01-20T10:00:00Z"
    }
  ]
}
```

### 2. Add to Wishlist
**Endpoint:** `POST /wishlist`

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Request Body:**
```json
{
  "propertyId": 5
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Property added to wishlist",
  "data": {
    "id": 1,
    "userId": 1,
    "propertyId": 5,
    "createdDate": "2025-01-20T10:00:00Z"
  }
}
```

### 3. Remove from Wishlist
**Endpoint:** `DELETE /wishlist/{id}`

**Headers:**
```
Authorization: Bearer {token}
```

---

## Contact Inquiry Endpoints

### 1. Create Contact Inquiry
**Endpoint:** `POST /contactinquiry`

**Request Body:**
```json
{
  "name": "John Smith",
  "email": "john@example.com",
  "phone": "+1234567890",
  "message": "Interested in this property",
  "propertyId": 1
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Contact inquiry created successfully",
  "data": {
    "id": 1,
    "name": "John Smith",
    "email": "john@example.com",
    "phone": "+1234567890",
    "message": "Interested in this property",
    "propertyId": 1,
    "createdDate": "2025-01-20T10:00:00Z"
  }
}
```

### 2. Get Inquiries for Property
**Endpoint:** `GET /contactinquiry/property/{propertyId}`

**Headers:**
```
Authorization: Bearer {token}
```

---

## Property Image Endpoints

### 1. Get Images for Property
**Endpoint:** `GET /propertyimage/property/{propertyId}`

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Property images retrieved successfully",
  "data": [
    {
      "id": 1,
      "imageUrl": "https://...",
      "isPrimary": true,
      "propertyId": 1
    }
  ]
}
```

### 2. Upload Property Image
**Endpoint:** `POST /propertyimage`

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Request Body:**
```json
{
  "imageUrl": "https://example.com/image.jpg",
  "isPrimary": true,
  "propertyId": 1
}
```

---

## User Endpoints

### 1. Get Current User Profile
**Endpoint:** `GET /user/profile/me`

**Headers:**
```
Authorization: Bearer {token}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "User profile retrieved successfully",
  "data": {
    "id": 1,
    "email": "user@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "phoneNumber": "+1234567890",
    "role": 3
  }
}
```

### 2. Get All Users
**Endpoint:** `GET /user`

**Headers:**
```
Authorization: Bearer {token}
```

---

## Error Codes

| Code | Meaning |
|------|---------|
| 200 | OK - Successful GET, PUT, or DELETE request |
| 201 | Created - Successful POST request |
| 400 | Bad Request - Invalid parameters or validation error |
| 401 | Unauthorized - Authentication required or failed |
| 404 | Not Found - Resource does not exist |
| 500 | Internal Server Error - Server-side error |

---

## Rate Limiting

Currently, there is no rate limiting implemented. This is recommended for production deployment.

---

## Version History

- **v1.0.0** (January 2025) - Initial release
  - User authentication and authorization
  - Property CRUD operations
  - Search and filtering
  - Wishlist management
  - Contact inquiries

---

## Support

For issues or questions, please contact: support@bookmyproperty.com
