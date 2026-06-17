namespace WMS.Application.DTOs.Attendance;

public class CreateAttendanceDto
{
    public int EmpId { get; set; }

    public DateTime CheckIn { get; set; }

    public DateTime? CheckOut { get; set; }

    public string? WorkMode { get; set; }
}