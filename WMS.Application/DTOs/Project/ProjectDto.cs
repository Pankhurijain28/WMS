namespace WMS.Application.DTOs.Project;

public class ProjectDto
{
    public int ProjectId { get; set; }

    public string ProjectName { get; set; } = string.Empty;

    public int ClientId { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
}