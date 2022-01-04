using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Experience { get; set; }
        public DateTime HireDate { get; set; }
        public ICollection<Manager> Managers { get; set; }
        public ICollection<EmplyeesForClient> EmplyeesForClient { get; set; }
        public Technology Technology { get; set; }

    }
}
