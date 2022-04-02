using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class Position
    {
        public Position()
        {
            this.Employees = new HashSet<Employee>();
        }
        public int ID { get; set; }

        [Display(Name = "Position Name")]
        [Required(ErrorMessage = "You cannot leave the pisition name blank.")]
        [StringLength(50, ErrorMessage = "Position name cannot be more than 50 characters long.")]
        public string PosName { get; set; }
        public int SortIndex { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
