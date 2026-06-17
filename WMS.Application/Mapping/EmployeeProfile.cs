using AutoMapper;
using WMS.Application.DTOs.Announcement;
using WMS.Application.DTOs.AuditLog;
using WMS.Application.DTOs.Employee;
using WMS.Domain.Entities;
namespace WMS.Application.Mapping;

public class EmployeeProfile : Profile

{

    public EmployeeProfile()

    {

        CreateMap<Employee, EmployeeResponseDto>()
    .ForMember(
        dest => dest.DepartmentName,
        opt => opt.MapFrom(
            src => src.Department!.DepartmentName))
    .ForMember(
        dest => dest.RoleName,
        opt => opt.MapFrom(
            src => src.Role!.RoleName));



        CreateMap<CreateEmployeeDto, Employee>();



        CreateMap<UpdateEmployeeDto, Employee>();

        CreateMap<

    Announcement,

    AnnouncementResponseDto>();



        CreateMap<

            CreateAnnouncementDto,

            Announcement>();



        CreateMap<

            UpdateAnnouncementDto,

            Announcement>();



        CreateMap<

    AuditLog,

    AuditLogResponseDto>();

    }

}