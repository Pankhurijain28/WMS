using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.Announcement;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AnnouncementController : ControllerBase
{
    private readonly IAnnouncementService _service;


public AnnouncementController(
    IAnnouncementService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(
            await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        int id)
    {
        var announcement =
            await _service.GetByIdAsync(id);

        if (announcement == null)
            return NotFound();

        return Ok(announcement);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateAnnouncementDto dto)
    {
        var result =
            await _service.CreateAsync(dto);

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateAnnouncementDto dto)
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
