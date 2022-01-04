using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Client
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Client Name")]
        [StringLength(50)]
        public string ClientName { get; set; }
        [Display(Name = "Address of the main building")]
        [StringLength(70)]
        public string Adress { get; set; }
        public ICollection<EmplyeesForClient> EmplyeesForClient { get; set; }
    }
}
