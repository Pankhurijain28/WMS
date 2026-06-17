using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces;

public interface ILeaveRepository
{
    Task<IEnumerable<Leave>> GetAllAsync();

    Task<Leave?> GetByIdAsync(int id);

    Task<Leave> AddAsync(Leave leave);

    Task UpdateAsync(Leave leave);

    Task DeleteAsync(int id);
}