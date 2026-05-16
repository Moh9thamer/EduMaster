# EduMaster API

A backend API for managing the full academic lifecycle of a school — academic years, semesters, grades, sections, subjects, teachers, students, exams, attendance, and grading.

Built as a learning project to apply **Clean Architecture** and **Domain-Driven Design (DDD)** in a real-world .NET system.

---

## Project Aim

The goal of EduMaster is not just to build a school management system — it is to practice building software the right way:

- Business rules enforced inside the domain, not scattered across services
- Clear boundaries between layers with no leaking dependencies
- Every new feature follows the same structure, with no exceptions

---

## Architecture

The project follows **Clean Architecture** combined with **DDD**. Dependencies point inward only:

```
API  →  Infrastructure  →  Application  →  Domain
```

| Layer | Responsibility |
|---|---|
| **Domain** | Rich aggregate roots, business rules, domain exceptions |
| **Application** | Contracts only — service interfaces, repository interfaces, DTOs |
| **Infrastructure** | Implementations — EF Core, Identity, JWT, SMS, OTP |
| **API** | Controllers only — receives HTTP, calls a service, returns HTTP |

The key principle: the inner layers define **what** the system can do. The outer layers define **how**. You can swap any Infrastructure implementation without touching a single line of business logic.

---

## Tech Stack

- **ASP.NET Core 8** — Web API
- **Entity Framework Core** — SQL Server
- **ASP.NET Identity** — User and role management
- **JWT + Refresh Tokens** — Authentication
- **Twilio** — SMS for OTP password reset

---

## Setup

Create `API/appsettings.Development.json` (gitignored) with:

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
  }
}
```

Then run:

```bash
dotnet run --project API
```

Migrations, roles, and the seed admin are applied automatically on startup. Swagger UI is available at `/swagger` in development.

---

## Adding a Migration

```bash
dotnet ef migrations add <MigrationName> --project Infrastructure --startup-project API
```
