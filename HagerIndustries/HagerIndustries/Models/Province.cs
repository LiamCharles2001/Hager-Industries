using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class Province
    {

        public Province()
        {
            this.Employees = new HashSet<Employee>();
            this.ShippingCompanies = new HashSet<Company>();
            this.BillingCompanies = new HashSet<Company>();
            this.Cities = new HashSet<City>();
        }

        public int ID { get; set; }

        [Display(Name = "Province Name")]
        [Required(ErrorMessage = "You cannot leave Name blank.")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters long.")]
        public string provName { get; set; }

        [Display(Name = "Country")]
        public int CountryID { get; set; }
        public Country Country { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<Company> ShippingCompanies { get; set; }
        public ICollection<Company> BillingCompanies { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
