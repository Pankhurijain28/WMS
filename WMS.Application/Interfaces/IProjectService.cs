using WMS.Application.DTOs.Project;

namespace WMS.Application.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>>
        GetAllAsync();

    Task<ProjectResponseDto?>
        GetByIdAsync(int id);

    Task<ProjectResponseDto>
        CreateAsync(CreateProjectDto dto);

    Task UpdateAsync(
        int id,
        UpdateProjectDto dto);

    Task DeleteAsync(int id);
}