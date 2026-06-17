using AutoMapper;
using WMS.Application.DTOs.AuditLog;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class AuditLogService
    : IAuditLogService
{
    private readonly IAuditLogRepository _repository;
    private readonly IMapper _mapper;

    public AuditLogService(
        IAuditLogRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<
        IEnumerable<AuditLogResponseDto>>
        GetAllAsync()
    {
        var logs =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<AuditLogResponseDto>>
            (logs);
    }

    public async Task LogAsync(
        string entityName,
        int recordId,
        string action,
        string createdBy)
    {
        var log = new AuditLog
        {
            EntityName = entityName,
            RecordId = recordId,
            Action = action,
            CreatedBy = createdBy,
            CreatedOn = DateTime.UtcNow
        };

        await _repository.AddAsync(log);
    }
}