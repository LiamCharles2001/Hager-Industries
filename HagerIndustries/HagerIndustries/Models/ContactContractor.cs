using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class ContactContractor
    {
        public int ContractorID { get; set; }
        public Contractor Contractor { get; set; }
        public int ContactID { get; set; }
        public Contact Contact { get; set; }
    }
}
