using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class ContactCategory
    {
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public int ContactID { get; set; }
        public Contact Contact { get; set; }
    }
}
