using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class ContactVendor
    {
        public int VendorID { get; set; }
        public Vendor Vendor { get; set; }
        public int ContactID { get; set; }
        public Contact Contact { get; set; }
    }
}
