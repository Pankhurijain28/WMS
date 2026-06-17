using System;
using System.Collections.Generic;
using System.Text;

namespace WMS.Domain.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; } = string.Empty;

        public int ClientId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string Status { get; set; } = "Active";

        public Client? Client { get; set; }

        public ICollection<EmployeeProjectAllocation>
            EmployeeAllocations
        { get; set; }
            = new List<EmployeeProjectAllocation>();
    }
}
