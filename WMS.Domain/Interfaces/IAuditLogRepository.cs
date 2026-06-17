using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces;

public interface IAuditLogRepository
{
    Task<IEnumerable<AuditLog>>
        GetAllAsync();

    Task AddAsync(AuditLog auditLog);
}