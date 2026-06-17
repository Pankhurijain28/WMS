using AutoMapper;
using WMS.Application.DTOs.Role;
using WMS.Domain.Entities;

namespace WMS.Application.Mapping;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleResponseDto>();

        CreateMap<CreateRoleDto, Role>();

        CreateMap<UpdateRoleDto, Role>();
    }
}