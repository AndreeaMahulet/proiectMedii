using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace project.Models.LibraryViewModels
{
    public class ManagerGroup
    {
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        public int EmployeeCount { get; set; }
    }
}
