# High-Level Design (HLD) — Workforce Management System (WMS)

**Document version:** 1.0
**Project:** Workforce Management System (Capstone — Intelligent App Dev .NET)
**Architecture style:** N-tier, layered (Clean Architecture), client–server SPA + REST API

---

## 1. Purpose & Scope

The Workforce Management System (WMS) centralizes core HR and operational
workflows for a mid-to-large enterprise: employee data, attendance, leave
approval, departments, projects/clients, announcements, dashboards and audit
logging. It replaces fragmented spreadsheets/email processes with a single,
secure, cloud-deployed platform.

---

## 2. System Architecture Overview

```
                          ┌──────────────────────────────┐
        Browser  ───────▶ │   Angular 21 SPA (frontend)  │
   (Chrome/Edge)          │  Azure Static Web Apps        │
                          └───────────────┬───────────────┘
                                          │ HTTPS / REST + JWT (CORS)
                                          ▼
                          ┌──────────────────────────────┐
                          │  ASP.NET Core Web API (.NET10)│
                          │  Azure App Service (Linux)    │
                          │  ┌───────────────────────────┐│
                          │  │ Controllers (REST)        ││
                          │  │ Application (Services/DTO) ││
                          │  │ Domain (Entities/Interfaces)││
                          │  │ Infrastructure (EF Core)   ││
                          │  └───────────────────────────┘│
                          └───────────────┬───────────────┘
                                          │ EF Core (TDS/1433)
                                          ▼
                          ┌──────────────────────────────┐
                          │   Azure SQL Database (WMSDb)  │
                          └──────────────────────────────┘

   CI/CD:  GitHub (source)  ──▶  Azure DevOps Pipeline  ──▶  App Service + Static Web Apps
```

---

## 3. Technology Stack

| Layer | Technology |
|-------|-----------|
| Frontend | Angular 21, TypeScript, RxJS, Angular Material, Bootstrap, Chart.js (ng2-charts), ngx-toastr |
| Backend | ASP.NET Core Web API, .NET 10, C# |
| ORM / Data | Entity Framework Core 10 (Code-First + Migrations) |
| Database | Microsoft SQL Server / Azure SQL |
| Auth | JWT Bearer tokens, role-based authorization |
| Mapping/Validation | AutoMapper, DataAnnotations |
| Testing | xUnit |
| Hosting | Azure App Service (API), Azure Static Web Apps (UI) |
| CI/CD | Azure DevOps Pipelines (build → test → deploy) |
| Source control | Git / GitHub |

---

## 4. Logical Components (Backend layers)

| Project | Responsibility |
|---------|----------------|
| **WMS.API** | HTTP entry point — controllers, JWT auth, Swagger, CORS, global exception middleware, DI wiring |
| **WMS.Application** | Business logic — services, DTOs, AutoMapper profiles, validation, interfaces |
| **WMS.Domain** | Core entities and repository interfaces (no dependencies) |
| **WMS.Infrastructure** | EF Core `DbContext`, repository implementations, migrations |
| **WMS.Tests** | Unit tests (xUnit) |

Dependency direction: `API → Application → Domain ← Infrastructure`
(Domain has no outward dependencies; Infrastructure & Application depend on Domain.)

---

## 5. Functional Modules

| Module | Key capabilities |
|--------|------------------|
| Authentication | Login (JWT), Register (admin), role guard, last-login tracking |
| Employee | CRUD, search by name/department/role, status management |
| Department | CRUD |
| Role | CRUD (Admin/Manager/Employee) |
| Attendance | Check-in/out, monthly view, total-hours computation, search |
| Leave | Apply, cancel, manager approve/reject workflow |
| Project & Client | CRUD, client management |
| Allocation | Assign employees to projects |
| Announcement | Notice board CRUD |
| Dashboard | KPI summary cards, charts, counts |
| Audit Log | Read trail of actions |

---

## 6. Data Flow (example: Apply & Approve Leave)

```
Employee → [Apply Leave]  → POST /api/Leave        → LeaveService.CreateAsync → DB (status=Pending)
Manager  → [Approve]      → PUT  /api/Leave/{id}/approve → LeaveService.ApproveAsync
                                                        → status=Approved, ApprovedBy=<UserId>, ApprovedOn=now
Dashboard/List ← GET /api/Leave  ← reads updated status
```

---

## 7. Security Design

- **Authentication:** JWT Bearer tokens (HMAC-SHA256), 8-hour expiry, issued on login.
- **Authorization:** Role-based (`[Authorize(Roles=...)]`) — Admin / Manager / Employee.
- **Passwords:** Hashed (SHA-256) before storage; never returned to clients.
- **Transport:** HTTPS for the deployed app; CORS restricted to the known frontend origin (config-driven).
- **Secrets:** Connection string / JWT key supplied via configuration; in Azure can be set as App Service settings / Key Vault.
- **Error handling:** Global exception middleware returns sanitized JSON instead of stack traces.

---

## 8. Deployment Architecture (Cloud)

| Component | Azure resource |
|-----------|----------------|
| Angular SPA | Azure Static Web Apps (Free) |
| Web API | Azure App Service (Linux, .NET 10, Basic B1) |
| Database | Azure SQL Database |
| CI/CD | Azure DevOps pipeline (`azure-pipelines.yml`) |
| Source | GitHub repository |

**CI/CD flow:** push to `main` → Azure DevOps pipeline → *Build & Test* stage
(restore, build, xUnit tests, publish) → *Deploy* stage (API → App Service,
Angular production build → Static Web Apps).

---

## 9. Non-Functional Requirements

| Attribute | Approach |
|-----------|----------|
| Performance | Stateless API, EF Core, indexed keys; target < 200 ms typical responses |
| Scalability | Stateless API scalable on App Service; designed toward ~5000 users |
| Security | JWT, HTTPS, CORS, hashed passwords, role guards |
| Availability | Managed Azure PaaS services |
| Maintainability | Layered architecture, DTOs, repository + service patterns |
| Observability | Application Insights (App Service), structured logging, audit log table |

---

## 10. Assumptions & Constraints

- Single SQL database (Azure SQL) shared by all environments in this capstone.
- Crystal Reports (per spec) is .NET Framework-only and not supported on .NET 10;
  reporting is therefore provided via API/dashboard data (documented deviation).
- One App Service tier (Basic B1) — cold starts possible after idle.
