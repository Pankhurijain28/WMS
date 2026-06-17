namespace WMS.Application.DTOs.Attendence;

public class AttendanceDto
{
    public int AttendanceId { get; set; }

    public int EmpId { get; set; }

    public DateTime CheckIn { get; set; }

    public DateTime? CheckOut { get; set; }

    public double TotalHours { get; set; }

    public string? WorkMode { get; set; }

    public DateOnly AttendanceDate { get; set; }
}