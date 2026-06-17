using AutoMapper;
using WMS.Application.DTOs.EmployeeProjectAllocation;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class EmployeeProjectAllocationService
    : IEmployeeProjectAllocationService
{
    private readonly
        IEmployeeProjectAllocationRepository
        _repository;

    private readonly IMapper _mapper;

    public EmployeeProjectAllocationService(
        IEmployeeProjectAllocationRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<
        IEnumerable<EmployeeProjectAllocationResponseDto>>
        GetAllAsync()
    {
        var data =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<EmployeeProjectAllocationResponseDto>>
            (data);
    }

    public async Task<
        EmployeeProjectAllocationResponseDto?>
        GetByIdAsync(int id)
    {
        var data =
            await _repository.GetByIdAsync(id);

        if (data == null)
            return null;

        return _mapper.Map<
            EmployeeProjectAllocationResponseDto>
            (data);
    }

    public async Task<
        EmployeeProjectAllocationResponseDto>
        CreateAsync(
            CreateEmployeeProjectAllocationDto dto)
    {
        var entity =
            _mapper.Map<
                EmployeeProjectAllocation>(dto);

        entity.CreateDate =
            DateOnly.FromDateTime(DateTime.UtcNow);

        entity.Status = true;

        await _repository.AddAsync(entity);

        return _mapper.Map<
            EmployeeProjectAllocationResponseDto>
            (entity);
    }

    public async Task UpdateAsync(
        int id,
        UpdateEmployeeProjectAllocationDto dto)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            throw new Exception(
                "Allocation not found");

        _mapper.Map(dto, entity);

        entity.UpdatedDate =
            DateOnly.FromDateTime(DateTime.UtcNow);

        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}