using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class Manager
    {
        public int ManagerID { get; set; }
        public int DepartmentID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime ManagerSinceDate { get; set; }

        public Department Department { get; set; }
        public Employee Employee { get; set; }
    }
}
