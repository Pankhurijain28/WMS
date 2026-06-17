using WMS.Application.DTOs.Client;

namespace WMS.Application.Interfaces;

public interface IClientService
{
    Task<IEnumerable<ClientResponseDto>>
        GetAllAsync();

    Task<ClientResponseDto?>
        GetByIdAsync(int id);

    Task<ClientResponseDto>
        CreateAsync(CreateClientDto dto);

    Task UpdateAsync(
        int id,
        UpdateClientDto dto);

    Task DeleteAsync(int id);
}