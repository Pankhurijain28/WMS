using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.Department;

public class CreateDepartmentDto
{
    [Required]
    public string DepartmentName { get; set; }
        = string.Empty;

    public string? Description { get; set; }
}