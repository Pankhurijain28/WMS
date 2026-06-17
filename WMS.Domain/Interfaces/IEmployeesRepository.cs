using WMS.Domain.Entities;
namespace WMS.Domain.Interfaces;

public interface IEmployeeRepository

{

    Task<IEnumerable<Employee>> GetAllAsync();



    Task<Employee?> GetByIdAsync(int id);



    Task<Employee> AddAsync(Employee employee);



    Task UpdateAsync(Employee employee);

    Task<IEnumerable<Employee>>

    SearchByNameAsync(string name);



    Task<IEnumerable<Employee>>

        SearchByDepartmentAsync(int departmentId);



    Task<IEnumerable<Employee>>

        SearchByRoleAsync(int roleId);



    Task DeleteAsync(int id);

}