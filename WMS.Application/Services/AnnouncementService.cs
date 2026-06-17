using AutoMapper;
using WMS.Application.DTOs.Announcement;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class AnnouncementService
    : IAnnouncementService
{
    private readonly
        IAnnouncementRepository _repository;

    private readonly IMapper _mapper;

    public AnnouncementService(
        IAnnouncementRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<
        IEnumerable<AnnouncementResponseDto>>
        GetAllAsync()
    {
        var announcements =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<AnnouncementResponseDto>>
            (announcements);
    }

    public async Task<AnnouncementResponseDto?>
        GetByIdAsync(int id)
    {
        var announcement =
            await _repository.GetByIdAsync(id);

        if (announcement == null)
            return null;

        return _mapper.Map<
            AnnouncementResponseDto>
            (announcement);
    }

    public async Task<
        AnnouncementResponseDto>
        CreateAsync(
            CreateAnnouncementDto dto)
    {
        var announcement =
            _mapper.Map<Announcement>(dto);

        announcement.CreatedOn =
            DateTime.UtcNow;

        announcement.IsActive = true;

        await _repository.AddAsync(
            announcement);

        return _mapper.Map<
            AnnouncementResponseDto>
            (announcement);
    }

    public async Task UpdateAsync(
        int id,
        UpdateAnnouncementDto dto)
    {
        var announcement =
            await _repository.GetByIdAsync(id);

        if (announcement == null)
            throw new Exception(
                "Announcement not found");

        _mapper.Map(
            dto,
            announcement);

        await _repository.UpdateAsync(
            announcement);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}