namespace WMS.Application.DTOs.AuditLog;

public class AuditLogResponseDto
{
    public int AuditLogId { get; set; }

    public string EntityName { get; set; }
        = string.Empty;

    public int RecordId { get; set; }

    public string Action { get; set; }
        = string.Empty;

    public string CreatedBy { get; set; }
        = string.Empty;

    public DateTime CreatedOn { get; set; }
}