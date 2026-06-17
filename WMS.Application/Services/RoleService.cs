using AutoMapper;
using WMS.Application.DTOs.Role;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;
    private readonly IMapper _mapper;

    public RoleService(
        IRoleRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<
        IEnumerable<RoleResponseDto>>
        GetAllAsync()
    {
        var roles =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<RoleResponseDto>>
            (roles);
    }

    public async Task<RoleResponseDto?>
        GetByIdAsync(int id)
    {
        var role =
            await _repository.GetByIdAsync(id);

        if (role == null)
            return null;

        return _mapper.Map<
            RoleResponseDto>(role);
    }

    public async Task<RoleResponseDto>
        CreateAsync(CreateRoleDto dto)
    {
        var role =
            _mapper.Map<Role>(dto);

        await _repository.AddAsync(role);

        return _mapper.Map<
            RoleResponseDto>(role);
    }

    public async Task UpdateAsync(
        int id,
        UpdateRoleDto dto)
    {
        var role =
            await _repository.GetByIdAsync(id);

        if (role == null)
            throw new Exception(
                "Role not found");

        _mapper.Map(dto, role);

        await _repository.UpdateAsync(role);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}