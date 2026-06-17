using AutoMapper;
using WMS.Application.DTOs.Employee;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
namespace WMS.Application.Services;

public class EmployeeService : IEmployeeService

{

    private readonly IEmployeeRepository _repository;

    private readonly IMapper _mapper;



    public EmployeeService(

        IEmployeeRepository repository,

        IMapper mapper)

    {

        _repository = repository;

        _mapper = mapper;

    }



    public async Task<IEnumerable<EmployeeResponseDto>>

        GetAllAsync()

    {

        var employees =

            await _repository.GetAllAsync();



        return _mapper.Map<

            IEnumerable<EmployeeResponseDto>>

            (employees);

    }



    public async Task<EmployeeResponseDto?>

        GetByIdAsync(int id)

    {

        var employee =

            await _repository.GetByIdAsync(id);



        if (employee == null)

            return null;



        return _mapper.Map<

            EmployeeResponseDto>(employee);

    }



    public async Task<EmployeeResponseDto>

        CreateAsync(CreateEmployeeDto dto)

    {

        var employee =

            _mapper.Map<Employee>(dto);



        employee.CreatedOn = DateTime.UtcNow;



        await _repository.AddAsync(employee);



        return _mapper.Map<

            EmployeeResponseDto>(employee);

    }



    public async Task UpdateAsync(

        int id,

        UpdateEmployeeDto dto)

    {

        var employee =

            await _repository.GetByIdAsync(id);



        if (employee == null)

            throw new Exception(

                "Employee not found");



        _mapper.Map(dto, employee);



        employee.UpdatedOn = DateTime.UtcNow;



        await _repository.UpdateAsync(employee);

    }



    public async Task<

    IEnumerable<EmployeeResponseDto>>

    SearchByNameAsync(

        string name)

    {

        var employees =

            await _repository

                .SearchByNameAsync(name);



        return _mapper.Map<

            IEnumerable<EmployeeResponseDto>>

            (employees);

    }



    public async Task<

        IEnumerable<EmployeeResponseDto>>

        SearchByDepartmentAsync(

            int departmentId)

    {

        var employees =

            await _repository

                .SearchByDepartmentAsync(

                    departmentId);



        return _mapper.Map<

            IEnumerable<EmployeeResponseDto>>

            (employees);

    }



    public async Task<

        IEnumerable<EmployeeResponseDto>>

        SearchByRoleAsync(

            int roleId)

    {

        var employees =

            await _repository

                .SearchByRoleAsync(roleId);



        return _mapper.Map<

            IEnumerable<EmployeeResponseDto>>

            (employees);

    }



    public async Task DeleteAsync(int id)

    {

        await _repository.DeleteAsync(id);

    }

}