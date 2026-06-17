using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.Role;

public class CreateRoleDto
{
    [Required]
    public string RoleName { get; set; }
        = string.Empty;

    public string? Description { get; set; }
}