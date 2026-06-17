namespace WMS.Application.DTOs.Leave;

public class LeaveDto
{
    public int LeaveId { get; set; }

    public int EmpId { get; set; }

    public string LeaveType { get; set; } = string.Empty;

    public string? Reason { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime AppliedOn { get; set; }
}