using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class CompanyVendor
    {
        public int VendorID { get; set; }
        public Vendor Vendor { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; }
    }
}
