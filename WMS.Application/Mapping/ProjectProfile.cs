using AutoMapper;
using WMS.Application.DTOs.Project;
using WMS.Domain.Entities;

namespace WMS.Application.Mapping;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectResponseDto>()
            .ForMember(
                dest => dest.ClientName,
                opt => opt.MapFrom(
                    src => src.Client!.ClientName));

        CreateMap<CreateProjectDto, Project>();

        CreateMap<UpdateProjectDto, Project>();
    }
}