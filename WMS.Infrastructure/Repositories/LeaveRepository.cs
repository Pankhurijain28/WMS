using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories;

public class LeaveRepository : ILeaveRepository
{
    private readonly ApplicationDbContext _context;

    public LeaveRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Leave>>
        GetAllAsync()
    {
        return await _context.Leaves
            .Include(x => x.Employee)
            .ToListAsync();
    }

    public async Task<Leave?>
        GetByIdAsync(int id)
    {
        return await _context.Leaves
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(
                x => x.LeaveId == id);
    }

    public async Task<Leave>
        AddAsync(Leave leave)
    {
        _context.Leaves.Add(leave);

        await _context.SaveChangesAsync();

        return leave;
    }

    public async Task UpdateAsync(Leave leave)
    {
        _context.Leaves.Update(leave);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var leave =
            await _context.Leaves.FindAsync(id);

        if (leave == null)
            return;

        _context.Leaves.Remove(leave);

        await _context.SaveChangesAsync();
    }
}