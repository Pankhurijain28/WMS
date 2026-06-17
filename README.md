# Workforce Management System (WMS)

A full‑stack Workforce Management System for HR and operational workflows —
employee management, attendance, leave approval, departments, projects,
dashboards and auditing.

**Stack:** ASP.NET Core Web API (.NET 10) · Angular 21 · SQL Server (Azure SQL) ·
JWT auth · EF Core (Code‑First) · Azure App Service + Static Web Apps · Azure DevOps CI/CD.

---

## 1. Prerequisites

Install these once:

| Tool | Version | Notes |
|------|---------|-------|
| **.NET SDK** | **10.0** | https://dotnet.microsoft.com/download/dotnet/10.0 |
| **Node.js** | 20.19+ / 22 / 24 | https://nodejs.org (LTS) |
| **npm** | comes with Node | |
| **SQL Server** | Azure SQL (already provisioned) | or local SQL Server / Express |
| **Git** | latest | |

Check they're installed:

```powershell
dotnet --version      # 10.0.x
node --version        # v20+/v22+/v24
npm --version
```

---

## 2. Project structure

```
WMS/
├── WMS.API/                 ASP.NET Core Web API (controllers, JWT, Swagger)
├── WMS.Application/         Services, DTOs, AutoMapper, validation
├── WMS.Domain/             Entities + repository interfaces
├── WMS.Infrastructure/     EF Core DbContext, repositories, migrations
├── WMS.Tests/              xUnit unit tests
├── Frontend/WMS.Frontend/  Angular 21 client app
├── azure-pipelines.yml     Azure DevOps CI/CD pipeline
├── seed-data.sql           Sample data seed script
└── WMS.slnx                Solution file
```

---

## 3. Run the BACKEND (API)

From the repo root (`WMS/`):

```powershell
dotnet restore
dotnet build
dotnet run --project WMS.API
```

- API runs at **http://localhost:5190**
- Swagger UI: **http://localhost:5190/swagger**

> The database connection string and JWT key live in
> `WMS.API/appsettings.json`. By default it points at the Azure SQL database.

### Apply database migrations (only if using a fresh/local DB)

```powershell
dotnet tool install --global dotnet-ef          # one-time
dotnet ef database update --project WMS.Infrastructure --startup-project WMS.API
```

### Seed sample data (optional)

Run `seed-data.sql` against the database (SSMS / Azure Data Studio), or it is
already seeded on the shared Azure SQL database.

---

## 4. Run the FRONTEND (Angular)

In a **second terminal**:

```powershell
cd Frontend/WMS.Frontend
npm install          # first time only
npm start            # = ng serve
```

- App runs at **http://localhost:4200**
- It calls the API at `http://localhost:5190/api` (see
  `src/environments/environment.ts`).

---

## 5. Login credentials

| Role | Username | Password |
|------|----------|----------|
| Admin | `admin` | `Admin@123` |
| Manager | `manager` | `Manager@123` |
| Employee | `employee` | `Employee@123` |

Role behaviour:
- **Admin** – full access (all modules, audit logs, delete).
- **Manager** – manage employees, approve/reject leave, view dashboard.
- **Employee** – view‑only lists, can **apply for leave**.

New accounts can be created by an Admin via **POST `/api/Auth/register`**.

---

## 6. Run the tests

```powershell
dotnet test
```

---

## 7. Production build (manual)

**API:**
```powershell
dotnet publish WMS.API/WMS.API.csproj -c Release -o ./publish/api
```

**Angular:**
```powershell
cd Frontend/WMS.Frontend
npm run build -- --configuration production
# output: dist/wms-frontend/browser
```

---

## 8. Deployment (Azure + Azure DevOps CI/CD)

Deployed architecture:

```
Angular  ->  Azure Static Web Apps
.NET API ->  Azure App Service (Linux, .NET 10)
Database ->  Azure SQL
CI/CD    ->  Azure DevOps pipeline (azure-pipelines.yml)
```

**Live URLs:**
- Frontend: https://icy-ground-0c4762f00.7.azurestaticapps.net
- API: https://wms-api-pankhuri-hpcabjdahtanhge8.southeastasia-01.azurewebsites.net
- API Swagger: …/swagger

### How CI/CD works
A push to `main` triggers `azure-pipelines.yml`, which:
1. Builds & tests the .NET API, publishes it, deploys to **App Service**.
2. Builds the Angular app (production), deploys to **Static Web Apps**.

### One-time Azure configuration (already done)
- **Azure SQL → Networking:** "Allow Azure services and resources to access this server" enabled.
- **App Service → Environment variables:**
  `Cors__AllowedOrigins__0 = https://icy-ground-0c4762f00.7.azurestaticapps.net`
- **Pipeline secret variable:** `swaDeployToken` (Static Web App deployment token).
- **Service connection:** `wms-azure-connection` (Azure Resource Manager).

To change the deployed API URL the frontend talks to, edit
`Frontend/WMS.Frontend/src/environments/environment.prod.ts`.

---

## 9. Troubleshooting

| Symptom | Cause / Fix |
|---------|-------------|
| `dotnet` not found | Install **.NET 10 SDK**, reopen terminal. |
| DB error: *client IP not allowed* | Add your IP in Azure SQL → Networking → firewall rules. |
| Login works but **tables empty / dashboard spinner stuck** | Ensure Zone.js is present (already configured); hard refresh `Ctrl+Shift+R`. |
| **Whole blue / blank page** | `@angular/animations` must be installed (already in `package.json`); run `npm install`. |
| Frontend can't reach API (CORS) | Add the frontend origin to `Cors:AllowedOrigins` (App Service setting) or `Program.cs`. |
| Port 5190 already in use | Stop the other `dotnet` process, or change the port in `Properties/launchSettings.json`. |

---

## 10. Quick start (TL;DR)

```powershell
# Terminal 1 — API
dotnet run --project WMS.API

# Terminal 2 — Angular
cd Frontend/WMS.Frontend
npm install
npm start
```

Open **http://localhost:4200** and log in as `admin` / `Admin@123`.
