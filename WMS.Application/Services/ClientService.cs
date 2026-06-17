using AutoMapper;
using WMS.Application.DTOs.Client;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;
    private readonly IMapper _mapper;

    public ClientService(
        IClientRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<
        IEnumerable<ClientResponseDto>>
        GetAllAsync()
    {
        var clients =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<ClientResponseDto>>
            (clients);
    }

    public async Task<ClientResponseDto?>
        GetByIdAsync(int id)
    {
        var client =
            await _repository.GetByIdAsync(id);

        if (client == null)
            return null;

        return _mapper.Map<
            ClientResponseDto>(client);
    }

    public async Task<ClientResponseDto>
        CreateAsync(CreateClientDto dto)
    {
        var client =
            _mapper.Map<Client>(dto);

        await _repository.AddAsync(client);

        return _mapper.Map<
            ClientResponseDto>(client);
    }

    public async Task UpdateAsync(
        int id,
        UpdateClientDto dto)
    {
        var client =
            await _repository.GetByIdAsync(id);

        if (client == null)
            throw new Exception(
                "Client not found");

        _mapper.Map(dto, client);

        await _repository.UpdateAsync(client);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}