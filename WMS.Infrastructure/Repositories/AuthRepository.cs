using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _context;

    public AuthRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserLogin?>
        GetByUsernameAsync(string username)
    {
        return await _context.UserLogins
            .Include(x => x.Role)
            .FirstOrDefaultAsync(
                x => x.Username == username);
    }
}