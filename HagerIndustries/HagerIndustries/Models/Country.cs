using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class Country
    {
       public  Country()
        {
            this.Provinces = new HashSet<Province>();
            this.Employees = new HashSet<Employee>();
            this.ShippingCompanies = new HashSet<Company>();
            this.BillingCompanies = new HashSet<Company>();
        }
        public int ID { get; set; }

        [Display(Name = "Country Name")]
        [Required(ErrorMessage = "You cannot leave Name blank.")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters long.")]
        public string countryName { get; set; }

        [Display(Name = "Postal Code Format")]
        [StringLength(50, ErrorMessage = "Postal code cannot be more than 50 characters long.")]
        public string countryPostalFormat { get; set; }

        [Display(Name = "Province/State")]
        public ICollection<Province> Provinces { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<Company> ShippingCompanies { get; set; }
        public ICollection<Company> BillingCompanies { get; set; }

    }
}
