using WMS.Application.DTOs.Department;

namespace WMS.Application.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentResponseDto>>
        GetAllAsync();

    Task<DepartmentResponseDto?>
        GetByIdAsync(int id);

    Task<DepartmentResponseDto>
        CreateAsync(CreateDepartmentDto dto);

    Task UpdateAsync(
        int id,
        UpdateDepartmentDto dto);

    Task DeleteAsync(int id);
}