using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.Employee;
using WMS.Application.Interfaces;
namespace WMS.API.Controllers;



[Authorize]

[ApiController]

[Route("api/[controller]")]
public class EmployeesController : ControllerBase

{

    private readonly IEmployeeService _service;



    public EmployeesController(

        IEmployeeService service)

    {

        _service = service;

    }



    [HttpGet]

    public async Task<IActionResult> GetAll()

    {

        return Ok(await _service.GetAllAsync());

    }



    [HttpGet("{id}")]

    public async Task<IActionResult> GetById(int id)

    {

        var employee =

            await _service.GetByIdAsync(id);



        if (employee == null)

            return NotFound();



        return Ok(employee);

    }



    [HttpPost]

    public async Task<IActionResult> Create(

        CreateEmployeeDto dto)

    {

        var result =

            await _service.CreateAsync(dto);



        return CreatedAtAction(

            nameof(GetById),

            new { id = result.EmployeeId },

            result);

    }



    [HttpPut("{id}")]

    public async Task<IActionResult> Update(

        int id,

        UpdateEmployeeDto dto)

    {

        await _service.UpdateAsync(id, dto);



        return NoContent();

    }

    [HttpGet("search/name/{name}")]
    public async Task<IActionResult> SearchByName(
    string name)
    {
        var result =
            await _service.SearchByNameAsync(name);

        return Ok(result);
    }

    [HttpGet("search/department/{departmentId}")]
    public async Task<IActionResult> SearchByDepartment(
        int departmentId)
    {
        var result =
            await _service.SearchByDepartmentAsync(
                departmentId);

        return Ok(result);
    }

    [HttpGet("search/role/{roleId}")]
    public async Task<IActionResult> SearchByRole(
        int roleId)
    {
        var result =
            await _service.SearchByRoleAsync(
                roleId);

        return Ok(result);
    }



    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(

        int id)

    {

        await _service.DeleteAsync(id);



        return NoContent();

    }

}