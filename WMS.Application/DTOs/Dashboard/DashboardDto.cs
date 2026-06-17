namespace WMS.Application.DTOs.Dashboard;

public class DashboardDto
{
    public int TotalEmployees { get; set; }

    public int TotalDepartments { get; set; }

    public int TotalProjects { get; set; }

    public int TotalClients { get; set; }

    public int ActiveEmployees { get; set; }

    public int PendingLeaves { get; set; }
}