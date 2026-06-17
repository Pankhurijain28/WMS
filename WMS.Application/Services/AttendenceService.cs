using AutoMapper;
using WMS.Application.DTOs.Attendance;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class AttendanceService
    : IAttendanceService
{
    private readonly IAttendanceRepository _repository;
    private readonly IMapper _mapper;

    public AttendanceService(
        IAttendanceRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<
        IEnumerable<AttendanceResponseDto>>
        GetAllAsync()
    {
        var data =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<AttendanceResponseDto>>
            (data);
    }

    public async Task<AttendanceResponseDto?>
        GetByIdAsync(int id)
    {
        var data =
            await _repository.GetByIdAsync(id);

        if (data == null)
            return null;

        return _mapper.Map<
            AttendanceResponseDto>(data);
    }

    public async Task<AttendanceResponseDto>
        CreateAsync(CreateAttendanceDto dto)
    {
        var attendance =
            _mapper.Map<Attendance>(dto);

        attendance.AttendanceDate =
            DateOnly.FromDateTime(DateTime.UtcNow);

        attendance.TotalHours = 0;

        if (attendance.CheckOut != null)
        {
            attendance.TotalHours =
                (attendance.CheckOut.Value
                - attendance.CheckIn)
                .TotalHours;
        }

        await _repository.AddAsync(attendance);

        return _mapper.Map<
            AttendanceResponseDto>(attendance);
    }

    public async Task CheckOutAsync(
    int employeeId)
    {
        var today =
            DateOnly.FromDateTime(
                DateTime.UtcNow);

        var attendance =
            (await _repository.GetAllAsync())
            .FirstOrDefault(x =>
                x.EmpId == employeeId &&
                x.AttendanceDate == today);

        if (attendance == null)
            throw new Exception(
                "Check-In record not found");

        attendance.CheckOut =
            DateTime.UtcNow;

        attendance.TotalHours =
            (attendance.CheckOut.Value
            - attendance.CheckIn)
            .TotalHours;

        await _repository.UpdateAsync(
            attendance);
    }

    public async Task<
    IEnumerable<AttendanceResponseDto>>
    GetMonthlyAttendanceAsync(
        int employeeId,
        int month,
        int year)
    {
        var attendance =
            (await _repository.GetAllAsync())
            .Where(x =>
                x.EmpId == employeeId &&
                x.AttendanceDate.Month == month &&
                x.AttendanceDate.Year == year);

        return _mapper.Map<
            IEnumerable<AttendanceResponseDto>>
            (attendance);
    }

    public async Task<
    IEnumerable<AttendanceResponseDto>>
    GetEmployeeAttendanceAsync(
        int employeeId)
    {
        var attendance =
            (await _repository.GetAllAsync())
            .Where(x =>
                x.EmpId == employeeId);

        return _mapper.Map<
            IEnumerable<AttendanceResponseDto>>
            (attendance);
    }
    public async Task CheckInAsync(
    int employeeId)
    {
        var attendance = new Attendance
        {
            EmpId = employeeId,
            CheckIn = DateTime.UtcNow,
            AttendanceDate =
                DateOnly.FromDateTime(
                    DateTime.UtcNow),

            TotalHours = 0
        };

        await _repository.AddAsync(
            attendance);
    }

    public async Task UpdateAsync(
        int id,
        UpdateAttendanceDto dto)
    {
        var attendance =
            await _repository.GetByIdAsync(id);

        if (attendance == null)
            throw new Exception(
                "Attendance not found");

        _mapper.Map(dto, attendance);

        if (attendance.CheckOut != null)
        {
            attendance.TotalHours =
                (attendance.CheckOut.Value
                - attendance.CheckIn)
                .TotalHours;
        }

        await _repository.UpdateAsync(attendance);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

}