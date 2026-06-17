# Low-Level Design (LLD) — Workforce Management System (WMS)

**Document version:** 1.0
**Scope:** Detailed design — database schema, REST API contract, class/layer
design, validation rules, and key operation flows.

---

## 1. Solution / Package Structure

```
WMS-Solution
├── WMS.API              ASP.NET Core Web API
│   ├── Controllers      Auth, Employees, Departments, Roles, Attendance,
│   │                    Leave, Clients, Projects, Allocations, Announcements,
│   │                    AuditLogs, Dashboard
│   ├── Middleware       ExceptionMiddleware
│   ├── Services         JwtTokenService, DashboardService
│   └── Program.cs       DI, JWT, Swagger, CORS, pipeline
├── WMS.Application
│   ├── DTOs             per-module request/response DTOs
│   ├── Interfaces       I*Service contracts
│   ├── Services         *Service implementations
│   ├── Mapping          AutoMapper profiles
│   ├── Validation       MinimumAgeAttribute
│   └── Helpers          PasswordHasher
├── WMS.Domain
│   ├── Entities         11 entities
│   └── Interfaces       I*Repository contracts
├── WMS.Infrastructure
│   ├── Data             ApplicationDbContext
│   ├── Repositories     *Repository implementations
│   └── Migrations       EF Core migrations
└── WMS.Tests            xUnit tests
```

---

## 2. Database Schema (SQL Server / Azure SQL)

### 2.1 Tables

**Employees**
| Column | Type | Constraints |
|--------|------|-------------|
| EmployeeId | int | PK, Identity |
| FirstName | nvarchar(50) | NOT NULL |
| LastName | nvarchar(50) | NOT NULL |
| Email | nvarchar(80) | NOT NULL (unique by app rule) |
| PhoneNumber | nvarchar(15) | NOT NULL |
| Gender | nvarchar | M / F / O |
| DOB | date | NOT NULL (≥ 18 yrs) |
| DOJ | date | NOT NULL |
| DepartmentId | int | FK → Departments |
| RoleId | int | FK → Roles |
| Status | nvarchar(20) | default 'Active' |
| CreatedOn | datetime2 | default now |
| UpdatedOn | datetime2 | NULL |

**Departments** — DepartmentId (PK), DepartmentName (NOT NULL), Description, CreatedOn
**Roles** — RoleId (PK), RoleName (NOT NULL), Description
**Attendances** — AttendanceId (PK), EmpId (FK→Employees), CheckIn (NOT NULL), CheckOut (NULL), TotalHours (float), WorkMode (WFO/WFH/Hybrid), AttendanceDate (date)
**Leaves** — LeaveId (PK), EmpId (FK), LeaveType (Sick/Casual/Earned), Reason, FromDate, ToDate, Status (Pending/Approved/Rejected/Cancelled), AppliedOn, ApprovedBy (NULL), ApprovedOn (NULL)
**Projects** — ProjectId (PK), ProjectName, ClientId (FK→Clients), StartDate, EndDate, Status (Active/Completed)
**Clients** — ClientId (PK), ClientName, ClientAddress, ClientPhoneNumber, ClientLocation, Status (bit)
**EmployeeProjectAllocations** — AllocationId (PK), EmpId (FK), ProjectId (FK), AssignedOn, CreateDate, CreatedBy, Status (bit), UpdatedBy, UpdatedDate
**Announcements** — AnnouncementId (PK), Title, Message, CreatedBy, CreatedOn, IsActive (bit)
**UserLogins** — UserId (PK), Username (unique), PasswordHash, RoleId (FK→Roles), LastLogin
**AuditLogs** — AuditLogId (PK), EntityName, RecordId, Action (Insert/Update/Delete), CreatedBy, CreatedOn

### 2.2 Relationships (ER summary)

```
Roles 1───* Employees        Departments 1───* Employees
Roles 1───* UserLogins        Employees 1───* Attendances
Employees 1───* Leaves        Clients   1───* Projects
Employees 1───* EmployeeProjectAllocations *───1 Projects
```

---

## 3. REST API Contract

Base path: `/api`. All endpoints require a JWT Bearer token unless marked **[Anon]**.
Authorization roles shown in **[ ]**.

### 3.1 Auth — `/api/Auth`
| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| POST | `/login` | [Anon] | Authenticate, returns `{ token, username, role }` |
| POST | `/register` | [Admin] | Create a login account |
| GET | `/me` | [Auth] | Current user claims |

### 3.2 Employees — `/api/Employees`
| Method | Route | Auth |
|--------|-------|------|
| GET | `/` | [Auth] |
| GET | `/{id}` | [Auth] |
| POST | `/` | [Auth] |
| PUT | `/{id}` | [Auth] |
| DELETE | `/{id}` | [Auth] |
| GET | `/search/name/{name}` | [Auth] |
| GET | `/search/department/{departmentId}` | [Auth] |
| GET | `/search/role/{roleId}` | [Auth] |

### 3.3 Leave — `/api/Leave`
| Method | Route | Auth |
|--------|-------|------|
| GET | `/` | [Auth] |
| GET | `/{id}` | [Admin, Manager] |
| POST | `/` | [Employee, Admin, Manager] |
| PUT | `/{id}` | [Admin, Manager] |
| DELETE | `/{id}` | [Admin] |
| PUT | `/{id}/approve` | [Admin, Manager] |
| PUT | `/{id}/reject` | [Admin, Manager] |
| PUT | `/{id}/cancel` | [Employee, Admin, Manager] |

### 3.4 Attendance — `/api/Attendance`
| Method | Route | Auth |
|--------|-------|------|
| GET | `/` | [Admin, Manager] |
| GET | `/{id}` | [Admin, Manager] |
| POST | `/` | [Employee, Admin, Manager] |
| PUT | `/{id}` | [Admin, Manager] |
| DELETE | `/{id}` | [Admin] |
| POST | `/checkin` | [Employee] |
| POST | `/checkout` | [Auth] |
| GET | `/monthly?employeeId&month&year` | [Auth] |

### 3.5 Standard CRUD controllers
`Departments`, `Roles`, `Clients`, `Projects`, `EmployeeProjectAllocations`,
`Announcement` — each exposes: `GET /`, `GET /{id}`, `POST /`, `PUT /{id}`,
`DELETE /{id}` (management actions restricted to Admin/Manager; delete to Admin).

### 3.6 Dashboard / Audit
| Method | Route | Auth | Returns |
|--------|-------|------|---------|
| GET | `/api/Dashboard` | [Admin, Manager] | counts (employees, departments, projects, clients, active employees, pending leaves) |
| GET | `/api/AuditLogs` | [Admin] | audit entries |

---

## 4. Layer Design (per module)

Each module follows the same vertical slice:

```
Controller  ──uses──▶  I{X}Service  ──impl──▶  {X}Service
                                                  │ uses
                                                  ▼
                                       I{X}Repository ──impl──▶ {X}Repository
                                                                   │ EF Core
                                                                   ▼
                                                          ApplicationDbContext
DTOs  ◀── AutoMapper ──▶  Domain Entities
```

**Example — Employee:**
- `EmployeesController` → `IEmployeeService` → `EmployeeService` → `IEmployeeRepository` → `EmployeeRepository` → `ApplicationDbContext`
- DTOs: `CreateEmployeeDto`, `UpdateEmployeeDto`, `EmployeeResponseDto`
- Mapping: `EmployeeProfile`

---

## 5. Key DTOs & Validation Rules

**CreateEmployeeDto** (DataAnnotations)
- FirstName/LastName: Required, MaxLength(50)
- Email: Required, EmailAddress, MaxLength(80)
- PhoneNumber: Required, Phone, MaxLength(15)
- Gender: Required, RegularExpression `^[MFO]$`
- DOB: Required, **MinimumAge(18)** (custom attribute)
- DepartmentId, RoleId: Required

**CreateLeaveDto** (DataAnnotations + IValidatableObject)
- EmpId: Required
- LeaveType: Required, RegularExpression `^(Sick|Casual|Earned)$`
- FromDate, ToDate: Required; **ToDate ≥ FromDate** (cross-field validation)

**RegisterRequestDto** — Username Required/MaxLength(50), Password Required/MinLength(6), RoleId Required

`[ApiController]` auto-returns `400 ValidationProblemDetails` when these fail.

---

## 6. Authentication & Authorization Design

- **JwtTokenService.GenerateToken(user, roleName)** builds claims:
  `ClaimTypes.Name = username`, `ClaimTypes.Role = roleName`, `UserId = user.UserId`.
- Signed with `Jwt:Key` (HMAC-SHA256), 8-hour expiry.
- **AuthService.LoginAsync**: load user by username → hash input password (SHA-256)
  → compare → update `LastLogin` → issue token.
- **PasswordHasher.Hash**: SHA-256 → Base64.
- Controllers enforce roles via `[Authorize(Roles="...")]`; the Angular client
  mirrors this with route guards and role-based UI gating.

---

## 7. Sequence — Login

```
Client            AuthController       AuthService        AuthRepository     DB
  │ POST /login        │                   │                   │             │
  │───────────────────▶│ LoginAsync(dto)   │                   │             │
  │                    │──────────────────▶│ GetByUsername     │             │
  │                    │                   │──────────────────▶│ SELECT user │
  │                    │                   │  hash & compare   │             │
  │                    │                   │  UpdateLastLogin  │────────────▶│
  │                    │   JWT token       │                   │             │
  │◀───────────────────│◀──────────────────│                   │             │
  │ { token, role }    │                   │                   │             │
```

## 8. Sequence — Leave Approval

```
Manager → PUT /api/Leave/{id}/approve
  LeaveController.Approve(id)  → reads UserId from JWT claim
    → LeaveService.ApproveAsync(id, approverId)
        → repo.GetById → set Status='Approved', ApprovedBy, ApprovedOn=now
        → repo.UpdateAsync → SaveChanges
  ← 200 "Leave approved"
```

---

## 9. Cross-Cutting Concerns

| Concern | Implementation |
|---------|----------------|
| Exception handling | `ExceptionMiddleware` → JSON `{ message }`, HTTP 500 |
| Validation | DataAnnotations + `IValidatableObject` + `[ApiController]` |
| Mapping | AutoMapper profiles per module |
| CORS | `Cors:AllowedOrigins` config → policy `AllowAngular` |
| Logging | ASP.NET Core logging + Application Insights (Azure) |
| Auditing | `AuditLogs` table + `AuditLogsController` (read) |

---

## 10. Configuration Keys

| Key | Purpose |
|-----|---------|
| `ConnectionStrings:DefaultConnection` | SQL/Azure SQL connection |
| `Jwt:Key` / `Jwt:Issuer` / `Jwt:Audience` | JWT signing & validation |
| `Cors:AllowedOrigins:0..n` | Allowed frontend origins |

In Azure, these map to App Service settings, e.g.
`Cors__AllowedOrigins__0`, `ConnectionStrings__DefaultConnection`.

---

## 11. Frontend (Angular) Design

```
src/app
├── core/services      one HttpClient service per module + auth
├── core/models        typed models
├── shared/guards      authGuard (token check)
├── shared/interceptors auth.interceptor (attaches JWT)
├── shared/layouts     layout shell (sidebar + navbar + router-outlet)
└── features/*         auth, dashboard, employees, departments, roles,
                       clients, projects, allocation, attendance, leaves,
                       announcements, auditlogs
```

- **State/UX:** RxJS subscriptions, Reactive Forms, ngx-toastr notifications,
  Chart.js dashboard, role-based UI gating via `AuthService.canManage()`.
- **Routing:** authenticated routes are children of the layout shell; `authGuard`
  protects them; `**` redirects to login.
