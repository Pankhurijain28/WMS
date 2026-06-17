using Microsoft.EntityFrameworkCore;
using WMS.Application.DTOs.Dashboard;
using WMS.Application.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.API.Services;

public class DashboardService : IDashboardService
{
    private readonly ApplicationDbContext _context;

    public DashboardService(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto>
        GetDashboardAsync()
    {
        return new DashboardDto
        {
            TotalEmployees =
                await _context.Employees.CountAsync(),

            TotalDepartments =
                await _context.Departments.CountAsync(),

            TotalProjects =
                await _context.Projects.CountAsync(),

            TotalClients =
                await _context.Clients.CountAsync(),

            ActiveEmployees =
                await _context.Employees
                    .CountAsync(x =>
                        x.Status == "Active"),

            PendingLeaves =
                await _context.Leaves
                    .CountAsync(x =>
                        x.Status == "Pending")
        };
    }
}