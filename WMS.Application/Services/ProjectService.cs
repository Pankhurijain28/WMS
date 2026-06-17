using AutoMapper;
using WMS.Application.DTOs.Project;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class ProjectService
    : IProjectService
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public ProjectService(
        IProjectRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<
        IEnumerable<ProjectResponseDto>>
        GetAllAsync()
    {
        var projects =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<ProjectResponseDto>>
            (projects);
    }

    public async Task<ProjectResponseDto?>
        GetByIdAsync(int id)
    {
        var project =
            await _repository.GetByIdAsync(id);

        if (project == null)
            return null;

        return _mapper.Map<
            ProjectResponseDto>(project);
    }

    public async Task<ProjectResponseDto>
        CreateAsync(CreateProjectDto dto)
    {
        var project =
            _mapper.Map<Project>(dto);

        await _repository.AddAsync(project);

        return _mapper.Map<
            ProjectResponseDto>(project);
    }

    public async Task UpdateAsync(
        int id,
        UpdateProjectDto dto)
    {
        var project =
            await _repository.GetByIdAsync(id);

        if (project == null)
            throw new Exception(
                "Project not found");

        _mapper.Map(dto, project);

        await _repository.UpdateAsync(project);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}