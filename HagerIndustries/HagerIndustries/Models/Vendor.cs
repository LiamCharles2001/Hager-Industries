using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class Vendor
    {
        public Vendor()
        {
            CompanyVendors = new HashSet<CompanyVendor>();
        }
        public int ID { get; set; }

        [Display(Name = "Vendor Name")]
        //[Required(ErrorMessage = "You cannot leave the name of the Contract blank.")]
        [StringLength(100, ErrorMessage = "Contract name is too long, the longest allowed name is 100 characters long")]
        public string VendorName { get; set; }

        [Display(Name = "Vendor Description")]
        //[Required(ErrorMessage = "You cannot leave the Description of the Contract blank.")]
        [StringLength(500, ErrorMessage = "Contract Description is too long, the longest allowed description is 500 characters long")]
        public string VendorDescription { get; set; }

        public int SortIndex { get; set; }
        public ICollection<CompanyVendor> CompanyVendors { get; set; }
    }
}
