using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.Department;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentService _service;

    public DepartmentsController(
        IDepartmentService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(
            await _service.GetAllAsync());
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        int id)
    {
        var department =
            await _service.GetByIdAsync(id);

        if (department == null)
            return NotFound();

        return Ok(department);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateDepartmentDto dto)
    {
        var result =
            await _service.CreateAsync(dto);

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateDepartmentDto dto)
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