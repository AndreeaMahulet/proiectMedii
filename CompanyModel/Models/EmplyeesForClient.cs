using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class EmplyeesForClient
    {
        public int ClientID { get; set; }
        public int EmployeeID { get; set; }
        public Client Client { get; set; }
        public Employee Employee { get; set; }
    }
}
