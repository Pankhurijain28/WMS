using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.Leave;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LeaveController : ControllerBase
{
    private readonly ILeaveService _service;

    public LeaveController(
        ILeaveService service)
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
        var leave =
            await _service.GetByIdAsync(id);

        if (leave == null)
            return NotFound();

        return Ok(leave);
    }

    [Authorize(Roles = "Employee,Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateLeaveDto dto)
    {
        var result =
            await _service.CreateAsync(dto);

        return Ok(result);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateLeaveDto dto)
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
}