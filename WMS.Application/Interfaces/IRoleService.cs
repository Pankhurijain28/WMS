using WMS.Application.DTOs.Role;

namespace WMS.Application.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleResponseDto>>
        GetAllAsync();

    Task<RoleResponseDto?>
        GetByIdAsync(int id);

    Task<RoleResponseDto>
        CreateAsync(CreateRoleDto dto);

    Task UpdateAsync(
        int id,
        UpdateRoleDto dto);

    Task DeleteAsync(int id);
}