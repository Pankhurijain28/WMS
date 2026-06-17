using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using System.Text;
using WMS.API.Middleware;
using WMS.API.Services;
using WMS.Application.Interfaces;
using WMS.Application.Mapping;
using WMS.Application.Services;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;
using WMS.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ===============================
// Controllers
// ===============================

builder.Services.AddControllers();

// ===============================
// Database
// ===============================

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ===============================
// AutoMapper
// ===============================

builder.Services.AddAutoMapper(typeof(EmployeeProfile).Assembly);

// ===============================
// JWT Authentication
// ===============================

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!))
            };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("===== JWT FAILED =====");
                Console.WriteLine(context.Exception.ToString());

                return Task.CompletedTask;
            },

            OnTokenValidated = context =>
            {
                Console.WriteLine("===== TOKEN VALIDATED =====");
                Console.WriteLine(
                    context.Principal?.Identity?.Name);

                return Task.CompletedTask;
            }
        };
    });



builder.Services.AddAuthorization();

// ===============================
// Swagger
// ===============================

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "WMS API",
            Version = "v1"
        });

    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "Paste JWT token here"
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type =
                                ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });
});

// ===============================
// Repositories
// ===============================

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<ILeaveRepository, LeaveRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

builder.Services.AddScoped<
    IEmployeeProjectAllocationRepository,
    EmployeeProjectAllocationRepository>();

builder.Services.AddScoped<
    IAnnouncementRepository,
    AnnouncementRepository>();

builder.Services.AddScoped<
    IAuditLogRepository,
    AuditLogRepository>();

builder.Services.AddScoped<
    IAuthRepository,
    AuthRepository>();

// ===============================
// Services
// ===============================

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddScoped<
    IEmployeeProjectAllocationService,
    EmployeeProjectAllocationService>();

builder.Services.AddScoped<
    IAnnouncementService,
    AnnouncementService>();

builder.Services.AddScoped<
    IAuditLogService,
    AuditLogService>();

builder.Services.AddScoped<
    IDashboardService,
    DashboardService>();

builder.Services.AddScoped<
    IAuthService,
    AuthService>();

builder.Services.AddScoped<
    IJwtTokenService,
    JwtTokenService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAngular",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// ===============================
// Build App
// ===============================

var app = builder.Build();



// ===============================
// Middleware Pipeline
// ===============================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

// app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();