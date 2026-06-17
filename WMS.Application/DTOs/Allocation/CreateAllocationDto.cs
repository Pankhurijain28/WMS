namespace WMS.Application.DTOs.EmployeeProjectAllocation;

public class CreateEmployeeProjectAllocationDto
{
    public int EmpId { get; set; }

    public int ProjectId { get; set; }

    public DateOnly AssignedOn { get; set; }

    public string CreatedBy { get; set; }
        = string.Empty;
}