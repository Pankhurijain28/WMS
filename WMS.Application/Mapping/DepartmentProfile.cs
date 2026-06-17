using AutoMapper;
using WMS.Application.DTOs.Department;
using WMS.Domain.Entities;

namespace WMS.Application.Mapping;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<
            Department,
            DepartmentResponseDto>();

        CreateMap<
            CreateDepartmentDto,
            Department>();

        CreateMap<
            UpdateDepartmentDto,
            Department>();
    }
}