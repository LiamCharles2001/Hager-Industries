using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class CompanyContractor
    {
        public int ContractorID { get; set; }
        public Contractor Contractor { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; }
    }
}
