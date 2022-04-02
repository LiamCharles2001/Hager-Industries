using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class Customer
    {
        public Customer()
        {
            CompanyCustomers = new HashSet<CompanyCustomer>();
        }
        public int ID { get; set; }

        [Display(Name = "Customer Name")]
        //[Required(ErrorMessage = "You cannot leave the name of the Contract blank.")]
        [StringLength(100, ErrorMessage = "Contract name is too long, the longest allowed name is 100 characters long")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Description")]
        //[Required(ErrorMessage = "You cannot leave the Description of the Contract blank.")]
        [StringLength(500, ErrorMessage = "Contract Description is too long, the longest allowed description is 500 characters long")]
        public string CustomerDescription { get; set; }

        public int SortIndex { get; set; }
        public ICollection<CompanyCustomer> CompanyCustomers { get; set; }
    }
}
