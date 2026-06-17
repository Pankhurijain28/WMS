using System.ComponentModel.DataAnnotations;
using WMS.Application.Validation;

namespace WMS.Application.DTOs.Employee;

public class CreateEmployeeDto
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(80)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    [MaxLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [RegularExpression("^[MFO]$",
        ErrorMessage = "Gender must be M, F or O.")]
    public char Gender { get; set; }

    [Required]
    [MinimumAge(18,
        ErrorMessage = "Employee must be at least 18 years old.")]
    public DateOnly DOB { get; set; }

    [Required]
    public DateOnly DOJ { get; set; }

    [Required]
    public int DepartmentId { get; set; }

    [Required]
    public int RoleId { get; set; }
}
