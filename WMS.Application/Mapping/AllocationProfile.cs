using AutoMapper;
using WMS.Application.DTOs.EmployeeProjectAllocation;
using WMS.Domain.Entities;

namespace WMS.Application.Mapping;

public class EmployeeProjectAllocationProfile
    : Profile
{
    public EmployeeProjectAllocationProfile()
    {
        CreateMap<
            EmployeeProjectAllocation,
            EmployeeProjectAllocationResponseDto>()

            .ForMember(
                d => d.EmployeeName,
                o => o.MapFrom(
                    s => s.Employee!.FirstName + " "
                       + s.Employee.LastName))

            .ForMember(
                d => d.ProjectName,
                o => o.MapFrom(
                    s => s.Project!.ProjectName));

        CreateMap<
            CreateEmployeeProjectAllocationDto,
            EmployeeProjectAllocation>();

        CreateMap<
            UpdateEmployeeProjectAllocationDto,
            EmployeeProjectAllocation>();
    }
}