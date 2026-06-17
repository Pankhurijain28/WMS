using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;
namespace WMS.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository

{

    private readonly ApplicationDbContext _context;



    public EmployeeRepository(

        ApplicationDbContext context)

    {

        _context = context;

    }



    public async Task<IEnumerable<Employee>> GetAllAsync()

    {

        return await _context.Employees

            .Include(x => x.Department)

            .Include(x => x.Role)

            .ToListAsync();

    }



    public async Task<Employee?> GetByIdAsync(int id)

    {

        return await _context.Employees

            .Include(x => x.Department)

            .Include(x => x.Role)

            .FirstOrDefaultAsync(x =>

                x.EmployeeId == id);

    }



    public async Task<Employee> AddAsync(Employee employee)

    {

        _context.Employees.Add(employee);



        await _context.SaveChangesAsync();



        return employee;

    }



    public async Task UpdateAsync(Employee employee)

    {

        _context.Employees.Update(employee);



        await _context.SaveChangesAsync();

    }



    public async Task<IEnumerable<Employee>>

    SearchByNameAsync(string name)

    {

        return await _context.Employees

            .Where(x =>

                x.FirstName.Contains(name) ||

                x.LastName.Contains(name))

            .ToListAsync();

    }



    public async Task<IEnumerable<Employee>>

        SearchByDepartmentAsync(

            int departmentId)

    {

        return await _context.Employees

            .Where(x =>

                x.DepartmentId == departmentId)

            .ToListAsync();

    }



    public async Task<IEnumerable<Employee>>

        SearchByRoleAsync(

            int roleId)

    {

        return await _context.Employees

            .Where(x =>

                x.RoleId == roleId)

            .ToListAsync();

    }



    public async Task DeleteAsync(int id)

    {

        var employee =

            await _context.Employees.FindAsync(id);



        if (employee == null)

            return;



        _context.Employees.Remove(employee);



        await _context.SaveChangesAsync();

    }

}