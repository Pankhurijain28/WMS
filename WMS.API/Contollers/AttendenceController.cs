using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.Attendance;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _service;

    public AttendanceController(
        IAttendanceService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var attendance =
            await _service.GetByIdAsync(id);

        if (attendance == null)
            return NotFound();

        return Ok(attendance);
    }

    [Authorize(Roles = "Employee,Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateAttendanceDto dto)
    {
        var result =
            await _service.CreateAsync(dto);

        return Ok(result);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateAttendanceDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }



    [Authorize(Roles = "Employee")]
    [HttpPost("checkin")]
    public async Task<IActionResult> CheckIn(
    CheckInDto dto)
    {
        await _service.CheckInAsync(
            dto.EmployeeId);

        return Ok("Checked In");
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> CheckOut(
    CheckOutDto dto)
    {
        await _service.CheckOutAsync(
            dto.EmployeeId);

        return Ok("Checked Out");
    }

    [HttpGet("monthly")]
    public async Task<IActionResult>
GetMonthlyAttendance(
    int employeeId,
    int month,
    int year)
    {
        return Ok(
            await _service
            .GetMonthlyAttendanceAsync(
                employeeId,
                month,
                year));
    }
}