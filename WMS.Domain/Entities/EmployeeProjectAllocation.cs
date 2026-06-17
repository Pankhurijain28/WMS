using System;
using System.Collections.Generic;
using System.Text;

namespace WMS.Domain.Entities
{
    public class EmployeeProjectAllocation
    {
        public int AllocationId { get; set; }

        public int EmpId { get; set; }

        public int ProjectId { get; set; }

        public DateOnly AssignedOn { get; set; }

        public DateOnly CreateDate { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public bool Status { get; set; } = true;

        public string? UpdatedBy { get; set; }

        public DateOnly? UpdatedDate { get; set; }

        public Employee? Employee { get; set; }

        public Project? Project { get; set; }
    }
}
