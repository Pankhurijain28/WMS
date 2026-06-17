using System.ComponentModel.DataAnnotations;

namespace WMS.Application.DTOs.Project;

public class CreateProjectDto
{
    [Required]
    public string ProjectName { get; set; }
        = string.Empty;

    [Required]
    public int ClientId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
}