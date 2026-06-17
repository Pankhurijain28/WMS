using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllAsync();

    Task<Role?> GetByIdAsync(int id);

    Task<Role> AddAsync(Role role);

    Task UpdateAsync(Role role);

    Task DeleteAsync(int id);
}