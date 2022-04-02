using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class ContactCustomer
    {
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public int ContactID { get; set; }
        public Contact Contact { get; set; }
    }
}
