using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class Technology
    {
        public int TechnologyID { get; set; }
        public string Name { get; set; }
        public ICollection<Department> Departments { get; set; }
        public ICollection<Employee> Employees { get; set; }


    }
}
