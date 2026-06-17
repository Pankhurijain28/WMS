using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories;

public class AnnouncementRepository
    : IAnnouncementRepository
{
    private readonly ApplicationDbContext _context;

    public AnnouncementRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Announcement>>
        GetAllAsync()
    {
        return await _context.Announcements
            .OrderByDescending(x => x.CreatedOn)
            .ToListAsync();
    }

    public async Task<Announcement?>
        GetByIdAsync(int id)
    {
        return await _context.Announcements
            .FirstOrDefaultAsync(
                x => x.AnnouncementId == id);
    }

    public async Task AddAsync(
        Announcement announcement)
    {
        await _context.Announcements
            .AddAsync(announcement);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(
        Announcement announcement)
    {
        _context.Announcements
            .Update(announcement);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var announcement =
            await GetByIdAsync(id);

        if (announcement == null)
            return;

        _context.Announcements
            .Remove(announcement);

        await _context.SaveChangesAsync();
    }
}