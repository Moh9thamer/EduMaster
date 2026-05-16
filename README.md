# EduMaster API

A Clean Architecture ASP.NET Core 8 REST API for an educational platform. Handles authentication, role-based access control, and user management with JWT + refresh tokens.

## Tech Stack

- **ASP.NET Core 8** — Web API
- **Entity Framework Core** — SQL Server
- **ASP.NET Identity** — User & role management
- **JWT Bearer** — Authentication
- **Twilio** — SMS for password reset

---

## Setup

### 1. Configuration

Create `API/appsettings.Development.json` (gitignored) with real values:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=EduMaster;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "your-secret-key-at-least-32-characters",
    "Issuer": "EduMasterAPI",
    "Audience": "EduMasterUsers"
  },
  "Twilio": {
    "AccountSid": "ACxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
    "AuthToken": "your_auth_token",
    "FromPhone": "+1XXXXXXXXXX"
  },
  "Settings": {
    "CountryCode": "+973"
  },
  "SeedAdmin": {
    "FullName": "Admin",
    "Email": "admin@edumaster.com",
    "PhoneNumber": "XXXXXXXX",
    "Password": "Admin@123"
  },
  "Cors": {
    "AllowedOrigins": ["http://localhost:3000"]
  }
}
```

### 2. Run

```bash
dotnet run --project API
```

Migrations, roles, and the seed admin are applied automatically on first run.

### 3. Add a migration

```bash
dotnet ef migrations add <MigrationName> --project Infrastructure --startup-project API
```

---

## How to Use

### Base URL

```
https://localhost:{port}/api
```

Swagger UI is available at `/swagger` in development.

---

### Login

```http
POST /api/auth/login
Content-Type: application/json

{
  "phoneNumber": "12345678",
  "password": "Admin@123"
}
```

**Response**

```json
{
  "token": "eyJhbGci...",
  "refreshToken": "base64string..."
}
```

Use the `token` as a Bearer token in subsequent requests:

```
Authorization: Bearer eyJhbGci...
```

---

### Register a User *(Admin or Manager only)*

```http
POST /api/auth/register
Authorization: Bearer {token}
Content-Type: application/json

{
  "fullName": "John Doe",
  "email": "john@example.com",
  "mobileNumber": "12345678",
  "password": "Pass@123",
  "role": "Student"
}
```

Valid roles: `Admin`, `Manager`, `Teacher`, `Student`  
Managers can only create `Teacher` and `Student`.

---

### Get Current User Profile

```http
GET /api/auth/me
Authorization: Bearer {token}
```

**Response**

```json
{
  "id": "guid...",
  "fullName": "John Doe",
  "phoneNumber": "12345678",
  "email": "john@example.com"
}
```

---

### Update Profile

```http
PUT /api/auth/update
Authorization: Bearer {token}
Content-Type: application/json

{
  "fullName": "New Name",
  "email": "newemail@example.com",
  "phoneNumber": "87654321"
}
```

All fields are optional — only provided fields are updated.

---

### Refresh Token

```http
POST /api/auth/refresh
Content-Type: application/json

{
  "refreshToken": "base64string..."
}
```

Returns a new `token` and `refreshToken`. The old refresh token is invalidated.

---

### Logout

```http
POST /api/auth/logout
Authorization: Bearer {token}
Content-Type: application/json

{
  "refreshToken": "base64string..."
}
```

---

### Forgot Password

Sends an SMS with a reset token to the user's phone number.

```http
POST /api/auth/forgot-password
Content-Type: application/json

{
  "phoneNumber": "12345678"
}
```

> Rate limited to **3 requests/min** to protect SMS credits.

---

### Reset Password

```http
POST /api/auth/reset-password
Content-Type: application/json

{
  "phoneNumber": "12345678",
  "token": "token-received-via-sms",
  "newPassword": "NewPass@123"
}
```

---

### Admin: Force Reset a User's Password *(Admin only)*

```http
PUT /api/admin/users/{userId}/reset-password
Authorization: Bearer {adminToken}
Content-Type: application/json

{
  "newPassword": "NewPass@123"
}
```

---

## Rate Limits

| Endpoint | Limit |
|---|---|
| `POST /login` | 10 req / min |
| `POST /forgot-password` | 3 req / min |
| `POST /reset-password` | 5 req / min |
| `POST /refresh` | 5 req / min |

Exceeds limit → `429 Too Many Requests`

---

## Password Requirements

ASP.NET Identity defaults apply:
- Minimum 6 characters
- At least one uppercase letter
- At least one lowercase letter
- At least one digit
- At least one non-alphanumeric character (e.g. `@`, `!`, `#`)
