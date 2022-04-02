using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class BillingTerm
    {
        public BillingTerm()
        {
            this.Companies = new HashSet<Company>();
        }

        public int ID { get; set; }

        [Display(Name = "Billing Term")]
        [Required(ErrorMessage = "You cannot leave the name of the Billing Term blank.")]
        [StringLength(50, ErrorMessage = "Billing Term cannot be more than 50 characters long.")]
        public string TermName { get; set; }

        [Display(Name = "Billing Term Details")]
        [StringLength(100, ErrorMessage = "Billing Term Details cannot be more than 100 characters long.")]
        public string TermDetails { get; set; }
        public int SortIndex { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
