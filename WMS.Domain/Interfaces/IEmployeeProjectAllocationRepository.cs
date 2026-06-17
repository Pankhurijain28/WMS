using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces;

public interface IEmployeeProjectAllocationRepository
{
    Task<IEnumerable<EmployeeProjectAllocation>>
        GetAllAsync();

    Task<EmployeeProjectAllocation?>
        GetByIdAsync(int id);

    Task<EmployeeProjectAllocation>
        AddAsync(EmployeeProjectAllocation allocation);

    Task UpdateAsync(
        EmployeeProjectAllocation allocation);

    Task DeleteAsync(int id);
}