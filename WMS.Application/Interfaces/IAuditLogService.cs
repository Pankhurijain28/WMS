using WMS.Application.DTOs.AuditLog;

namespace WMS.Application.Interfaces;

public interface IAuditLogService
{
    Task<IEnumerable<AuditLogResponseDto>>
        GetAllAsync();

    Task LogAsync(
        string entityName,
        int recordId,
        string action,
        string createdBy);
}