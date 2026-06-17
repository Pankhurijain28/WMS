using System.ComponentModel.DataAnnotations;
namespace WMS.Application.DTOs.Employee;

public class CreateEmployeeDto

{

    [Required]

    public string FirstName { get; set; } = string.Empty;



    [Required]

    public string LastName { get; set; } = string.Empty;



    [Required]

    [EmailAddress]

    public string Email { get; set; } = string.Empty;



    [Required]

    public string PhoneNumber { get; set; } = string.Empty;



    public char Gender { get; set; }



    public DateOnly DOB { get; set; }



    public DateOnly DOJ { get; set; }



    public int DepartmentId { get; set; }



    public int RoleId { get; set; }

}