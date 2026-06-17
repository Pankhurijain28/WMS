using System.ComponentModel.DataAnnotations;
using System.Data;
namespace WMS.Domain.Entities;

public class Employee

{

    public int EmployeeId { get; set; }



    [Required]

    [MaxLength(50)]

    public string FirstName { get; set; } = string.Empty;



    [Required]

    [MaxLength(50)]

    public string LastName { get; set; } = string.Empty;



    [Required]

    [MaxLength(80)]

    public string Email { get; set; } = string.Empty;



    [Required]

    [MaxLength(15)]

    public string PhoneNumber { get; set; } = string.Empty;



    [Required]

    public char Gender { get; set; }



    public DateOnly DOB { get; set; }



    public DateOnly DOJ { get; set; }



    public int DepartmentId { get; set; }



    public int RoleId { get; set; }



    public string Status { get; set; } = "Active";



    public DateTime CreatedOn { get; set; }



    public DateTime? UpdatedOn { get; set; }



    // Navigation Properties



    public Department? Department { get; set; }



    public Role? Role { get; set; }



    public ICollection<Attendance> Attendances { get; set; }

        = new List<Attendance>();



    public ICollection<Leave> Leaves { get; set; }

        = new List<Leave>();

}