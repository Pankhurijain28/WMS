using WMS.Application.DTOs.Employee;
namespace WMS.Application.Interfaces;

public interface IEmployeeService

{

    Task<IEnumerable<EmployeeResponseDto>>

        GetAllAsync();



    Task<EmployeeResponseDto?>

        GetByIdAsync(int id);



    Task<EmployeeResponseDto>

        CreateAsync(CreateEmployeeDto dto);



    Task UpdateAsync(

        int id,

        UpdateEmployeeDto dto);



    Task<IEnumerable<EmployeeResponseDto>>

    SearchByNameAsync(string name);



    Task<IEnumerable<EmployeeResponseDto>>

        SearchByDepartmentAsync(int departmentId);



    Task<IEnumerable<EmployeeResponseDto>>

        SearchByRoleAsync(int roleId);



    Task DeleteAsync(int id);

}