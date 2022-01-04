using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models.LibraryViewModels
{
    public class ClientIndexData
    {
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Manager> Managers { get; set; }
    }
}
