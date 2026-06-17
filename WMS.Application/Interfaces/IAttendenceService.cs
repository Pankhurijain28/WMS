using WMS.Application.DTOs.Attendance;

namespace WMS.Application.Interfaces;

public interface IAttendanceService
{
    Task<IEnumerable<AttendanceResponseDto>>
        GetAllAsync();

    Task<AttendanceResponseDto?>
        GetByIdAsync(int id);

    Task<AttendanceResponseDto>
        CreateAsync(CreateAttendanceDto dto);

    Task UpdateAsync(
        int id,
        UpdateAttendanceDto dto);

    Task DeleteAsync(int id);
    Task CheckInAsync(int employeeId);

    Task CheckOutAsync(int employeeId);

    Task<IEnumerable<AttendanceResponseDto>>
        GetEmployeeAttendanceAsync(int employeeId);

    Task<IEnumerable<AttendanceResponseDto>>
        GetMonthlyAttendanceAsync(
            int employeeId,
            int month,
            int year);
}