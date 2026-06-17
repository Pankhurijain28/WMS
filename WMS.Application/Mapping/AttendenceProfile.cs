using AutoMapper;
using WMS.Application.DTOs.Attendance;
using WMS.Domain.Entities;

namespace WMS.Application.Mapping;

public class AttendanceProfile : Profile
{
    public AttendanceProfile()
    {
        CreateMap<
            Attendance,
            AttendanceResponseDto>()

            .ForMember(
                d => d.EmployeeName,
                o => o.MapFrom(
                    s => s.Employee!.FirstName + " "
                       + s.Employee.LastName));

        CreateMap<
            CreateAttendanceDto,
            Attendance>();

        CreateMap<
            UpdateAttendanceDto,
            Attendance>();
    }
}