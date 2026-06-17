using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories;

public class EmployeeProjectAllocationRepository
    : IEmployeeProjectAllocationRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeProjectAllocationRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<
        IEnumerable<EmployeeProjectAllocation>>
        GetAllAsync()
    {
        return await _context.EmployeeProjectAllocations
            .Include(x => x.Employee)
            .Include(x => x.Project)
            .ToListAsync();
    }

    public async Task<EmployeeProjectAllocation?>
        GetByIdAsync(int id)
    {
        return await _context.EmployeeProjectAllocations
            .Include(x => x.Employee)
            .Include(x => x.Project)
            .FirstOrDefaultAsync(
                x => x.AllocationId == id);
    }

    public async Task<EmployeeProjectAllocation>
        AddAsync(EmployeeProjectAllocation allocation)
    {
        _context.EmployeeProjectAllocations
            .Add(allocation);

        await _context.SaveChangesAsync();

        return allocation;
    }

    public async Task UpdateAsync(
        EmployeeProjectAllocation allocation)
    {
        _context.EmployeeProjectAllocations
            .Update(allocation);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var allocation =
            await _context.EmployeeProjectAllocations
                .FindAsync(id);

        if (allocation == null)
            return;

        _context.EmployeeProjectAllocations
            .Remove(allocation);

        await _context.SaveChangesAsync();
    }
}