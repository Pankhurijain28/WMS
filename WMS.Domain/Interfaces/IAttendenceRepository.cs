using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces;

public interface IAttendanceRepository
{
    Task<IEnumerable<Attendance>> GetAllAsync();

    Task<Attendance?> GetByIdAsync(int id);

    Task<Attendance> AddAsync(Attendance attendance);

    Task UpdateAsync(Attendance attendance);

    Task DeleteAsync(int id);
}