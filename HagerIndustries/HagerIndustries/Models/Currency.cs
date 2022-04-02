using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class Currency
    {

        public Currency()
        {
            this.Companies = new HashSet<Company>();
        }

        public int ID { get; set; }

        [Display(Name = "Type of Currency")]
        [Required(ErrorMessage = "You cannot leave the currency type blank.")]
        [StringLength(50, ErrorMessage = "Currency type cannot be more than 50 characters long.")]
        public string CurrencyName { get; set; }

        [Display(Name = "Currency Symbol")]
        [StringLength(10, ErrorMessage = "Currency symbol cannot be more than 10 characters long.")]
        public string CurrencySymbol { get; set; }
        public int SortIndex { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
