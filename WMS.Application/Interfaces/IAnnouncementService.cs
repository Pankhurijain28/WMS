using WMS.Application.DTOs.Announcement;

namespace WMS.Application.Interfaces;

public interface IAnnouncementService
{
    Task<IEnumerable<AnnouncementResponseDto>>
        GetAllAsync();

    Task<AnnouncementResponseDto?>
        GetByIdAsync(int id);

    Task<AnnouncementResponseDto>
        CreateAsync(CreateAnnouncementDto dto);

    Task UpdateAsync(
        int id,
        UpdateAnnouncementDto dto);

    Task DeleteAsync(int id);
}