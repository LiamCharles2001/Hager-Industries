using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class CompanyCustomer
    {
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; }
    }
}
