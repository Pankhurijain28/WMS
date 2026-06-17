using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories;

public class AttendanceRepository
    : IAttendanceRepository
{
    private readonly ApplicationDbContext _context;

    public AttendanceRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Attendance>>
        GetAllAsync()
    {
        return await _context.Attendances
            .Include(x => x.Employee)
            .ToListAsync();
    }

    public async Task<Attendance?>
        GetByIdAsync(int id)
    {
        return await _context.Attendances
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(
                x => x.AttendanceId == id);
    }

    public async Task<Attendance>
        AddAsync(Attendance attendance)
    {
        _context.Attendances.Add(attendance);

        await _context.SaveChangesAsync();

        return attendance;
    }

    public async Task UpdateAsync(
        Attendance attendance)
    {
        _context.Attendances.Update(attendance);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var attendance =
            await _context.Attendances
                .FindAsync(id);

        if (attendance == null)
            return;

        _context.Attendances.Remove(attendance);

        await _context.SaveChangesAsync();
    }
}