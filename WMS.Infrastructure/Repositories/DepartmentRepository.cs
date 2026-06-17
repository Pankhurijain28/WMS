using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories;

public class DepartmentRepository
    : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Department>>
        GetAllAsync()
    {
        return await _context.Departments
            .ToListAsync();
    }

    public async Task<Department?>
        GetByIdAsync(int id)
    {
        return await _context.Departments
            .FirstOrDefaultAsync(
                d => d.DepartmentId == id);
    }

    public async Task<Department>
        AddAsync(Department department)
    {
        _context.Departments.Add(department);

        await _context.SaveChangesAsync();

        return department;
    }

    public async Task UpdateAsync(
        Department department)
    {
        _context.Departments.Update(department);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var department =
            await _context.Departments
                .FindAsync(id);

        if (department == null)
            return;

        _context.Departments.Remove(department);

        await _context.SaveChangesAsync();
    }
}