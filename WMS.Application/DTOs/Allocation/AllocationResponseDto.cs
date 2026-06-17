namespace WMS.Application.DTOs.EmployeeProjectAllocation;

public class EmployeeProjectAllocationResponseDto
{
    public int AllocationId { get; set; }

    public int EmpId { get; set; }

    public string EmployeeName { get; set; }
        = string.Empty;

    public int ProjectId { get; set; }

    public string ProjectName { get; set; }
        = string.Empty;

    public DateOnly AssignedOn { get; set; }

    public bool Status { get; set; }
}