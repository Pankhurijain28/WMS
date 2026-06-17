using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.Leave;

public class ApplyLeaveDto
{
    [Required]
    public int EmpId { get; set; }

    [Required]
    public string LeaveType { get; set; } = string.Empty;

    public string? Reason { get; set; }

    [Required]
    public DateOnly FromDate { get; set; }

    [Required]
    public DateOnly ToDate { get; set; }
}