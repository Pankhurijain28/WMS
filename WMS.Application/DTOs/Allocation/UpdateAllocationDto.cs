namespace WMS.Application.DTOs.EmployeeProjectAllocation;

public class UpdateEmployeeProjectAllocationDto
{
    public int EmpId { get; set; }

    public int ProjectId { get; set; }

    public bool Status { get; set; }

    public string UpdatedBy { get; set; }
        = string.Empty;
}