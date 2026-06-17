namespace WMS.Application.DTOs.Leave;

public class CreateLeaveDto
{
    public int EmpId { get; set; }

    public string LeaveType { get; set; }
        = string.Empty;

    public string? Reason { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }
}