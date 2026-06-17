using AutoMapper;
using WMS.Application.DTOs.Client;
using WMS.Domain.Entities;

namespace WMS.Application.Mapping;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<Client, ClientResponseDto>();

        CreateMap<CreateClientDto, Client>();

        CreateMap<UpdateClientDto, Client>();
    }
}