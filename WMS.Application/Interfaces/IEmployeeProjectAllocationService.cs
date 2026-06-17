using WMS.Application.DTOs.EmployeeProjectAllocation;

namespace WMS.Application.Interfaces;

public interface IEmployeeProjectAllocationService
{
    Task<
        IEnumerable<EmployeeProjectAllocationResponseDto>>
        GetAllAsync();

    Task<EmployeeProjectAllocationResponseDto?>
        GetByIdAsync(int id);

    Task<EmployeeProjectAllocationResponseDto>
        CreateAsync(
            CreateEmployeeProjectAllocationDto dto);

    Task UpdateAsync(
        int id,
        UpdateEmployeeProjectAllocationDto dto);

    Task DeleteAsync(int id);
}