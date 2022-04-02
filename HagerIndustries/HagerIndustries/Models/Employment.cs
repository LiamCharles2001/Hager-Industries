using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class Employment
    {
        public Employment()
        {
            this.Employees = new HashSet<Employee>();
        }
        public int ID { get; set; }

        [Display(Name = "Emplyment Type")]
        [Required(ErrorMessage = "You cannot leave the employment type blank.")]
        [StringLength(50, ErrorMessage = "Employment type cannot be more than 50 characters long.")]
        public string EmplType { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
