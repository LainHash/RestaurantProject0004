# Authentication - Login & Logout Implementation Guide

## ✅ Implementation Complete

Authentication has been fully implemented with JWT token-based login/logout functionality.

---

## 📋 What Was Implemented

### 1. **Infrastructure Layer**

- `PasswordHashService` - BCrypt password hashing/verification
- `JwtTokenService` - JWT token generation with claims
- `AuthenticationService` - Login/Logout business logic
- `UserRepository` - Database queries for users

### 2. **Application Layer**

- `LoginCommand` & `LoginCommandHandler` - Login CQRS command
- `LogoutCommand` & `LogoutCommandHandler` - Logout CQRS command
- `LoginCommandValidator` - Fluent validation for login inputs
- `IAuthenticationService`, `IJwtTokenService`, `IPasswordHashService` - Interfaces
- `IUserRepository` - User data access interface
- `AuthResponseDto` - Login response DTO

### 3. **API Layer**

- `AuthController` - REST endpoints:
  - `POST /api/auth/login` - Login endpoint
  - `POST /api/auth/logout` - Logout endpoint (requires authentication)
- JWT Bearer scheme configured in `Program.cs`

### 4. **Configuration**

- Added JWT settings in `appsettings.json`:
  - Secret key (32+ characters)
  - Issuer, Audience
  - Token expiration (60 minutes)
- JWT middleware configured in `Program.cs`

---

## 🧪 How to Test

### Test Users (Seed Data)

Use the following credentials to login:

| Username  | Password    | Role     |
| --------- | ----------- | -------- |
| admin     | admin123    | Admin    |
| manager   | manager123  | Manager  |
| staff     | staff123    | Staff    |
| customer1 | customer123 | Customer |
| customer2 | customer123 | Customer |

### 1. Login Request

```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"userName":"admin","password":"admin123"}'
```

**Response:**

```json
{
  "success": true,
  "message": "Login successful",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "user": {
    "id": 1,
    "userName": "admin",
    "email": "admin@restaurant.com",
    "roleName": "Admin"
  }
}
```

### 2. Use Token for Protected Requests

```bash
curl -X POST http://localhost:5000/api/auth/logout \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

### 3. Logout Request

Returns:

```json
{
  "success": true,
  "message": "Logout successful"
}
```

---

## 📁 File Locations

**Interfaces:**

- `Restaurant.Application/Interfaces/Services/IAuthenticationService.cs`
- `Restaurant.Application/Interfaces/Services/IJwtTokenService.cs`
- `Restaurant.Application/Interfaces/Services/IPasswordHashService.cs`
- `Restaurant.Application/Interfaces/Repositories/IUserRepository.cs`

**Services:**

- `Restaurant.Infrastructure/Services/AuthenticationService.cs`
- `Restaurant.Infrastructure/Services/JwtTokenService.cs`
- `Restaurant.Infrastructure/Services/PasswordHashService.cs`

**Repository:**

- `Restaurant.Infrastructure/Persistence/Repositories/Identity/UserRepository.cs`

**Features (CQRS):**

- `Restaurant.Application/Features/Identity/Commands/Login/`
- `Restaurant.Application/Features/Identity/Commands/Logout/`

**Controller:**

- `Restaurant.API/Controllers/Identity/AuthController.cs`

**Configuration:**

- `Restaurant.API/Program.cs` - JWT middleware setup
- `Restaurant.API/appsettings.json` - JWT settings
- `Restaurant.Infrastructure/Services/DependencyInjection.cs` - Service registration

---

## 🔒 Security Features

✅ Password hashing with BCrypt (workFactor: 10)
✅ JWT token with expiration (60 minutes)
✅ Token validation on each request
✅ Role-based claims in token
✅ [Authorize] attribute support on endpoints

---

## 🔄 Token Payload

JWT tokens contain the following claims:

```json
{
  "nameid": "1",
  "unique_name": "admin",
  "email": "admin@restaurant.com",
  "role": "Admin"
}
```

---

## ⚙️ Configuration Details

**JWT Settings (appsettings.json):**

- Key: 32-character secret for signing
- Issuer: "RestaurantAPI"
- Audience: "RestaurantAPI"
- ExpirationMinutes: 60

---

## 📝 Next Steps (Optional Enhancements)

- [ ] Implement refresh token mechanism
- [ ] Add token blacklisting for logout
- [ ] Implement forgot password functionality
- [ ] Add email verification for registration
- [ ] Implement rate limiting for login attempts
- [ ] Add two-factor authentication
- [ ] Create registration endpoint
