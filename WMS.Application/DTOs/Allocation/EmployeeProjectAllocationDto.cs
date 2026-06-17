namespace WMS.Application.DTOs.Allocation;

public class EmployeeProjectAllocationDto
{
    public int AllocationId { get; set; }

    public int EmpId { get; set; }

    public int ProjectId { get; set; }

    public DateOnly AssignedOn { get; set; }

    public bool Status { get; set; }
}