using AutoMapper;
using WMS.Application.DTOs.Leave;
using WMS.Domain.Entities;

namespace WMS.Application.Mapping;

public class LeaveProfile : Profile
{
    public LeaveProfile()
    {
        CreateMap<Leave, LeaveResponseDto>()

            .ForMember(
                d => d.EmployeeName,
                o => o.MapFrom(
                    s => s.Employee!.FirstName + " "
                       + s.Employee.LastName));

        CreateMap<CreateLeaveDto, Leave>();

        CreateMap<UpdateLeaveDto, Leave>();
    }
}