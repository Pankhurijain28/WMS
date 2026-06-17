using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.Project;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _service;

    public ProjectsController(
        IProjectService service)
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
        var project =
            await _service.GetByIdAsync(id);

        if (project == null)
            return NotFound();

        return Ok(project);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProjectDto dto)
    {
        var result =
            await _service.CreateAsync(dto);

        return Ok(result);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateProjectDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}