using AutoMapper;
using WMS.Application.DTOs.Department;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class DepartmentService
    : IDepartmentService
{
    private readonly IDepartmentRepository _repository;
    private readonly IMapper _mapper;

    public DepartmentService(
        IDepartmentRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<
        IEnumerable<DepartmentResponseDto>>
        GetAllAsync()
    {
        var departments =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<DepartmentResponseDto>>
            (departments);
    }

    public async Task<DepartmentResponseDto?>
        GetByIdAsync(int id)
    {
        var department =
            await _repository.GetByIdAsync(id);

        if (department == null)
            return null;

        return _mapper.Map<
            DepartmentResponseDto>(department);
    }

    public async Task<DepartmentResponseDto>
        CreateAsync(CreateDepartmentDto dto)
    {
        var department =
            _mapper.Map<Department>(dto);

        await _repository.AddAsync(department);

        return _mapper.Map<
            DepartmentResponseDto>(department);
    }

    public async Task UpdateAsync(
        int id,
        UpdateDepartmentDto dto)
    {
        var department =
            await _repository.GetByIdAsync(id);

        if (department == null)
            throw new Exception(
                "Department not found");

        _mapper.Map(dto, department);

        await _repository.UpdateAsync(
            department);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}