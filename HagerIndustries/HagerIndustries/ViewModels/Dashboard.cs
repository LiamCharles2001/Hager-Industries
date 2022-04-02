using HagerIndustries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.ViewModels
{
    public class Dashboard
    {
        public List<Announcement> Announcements { get; set; }
        public Counter AllCounters { get; set; }

        public List<Event> Events { get; set; }
    }

    public class Counter {
        public int CompanyCustomers { get; set; }

        public int CompanyVendors { get; set; }
        public int CompanyContractors { get; set; }
        public int ContactCustomers { get; set; }
        public int ContactVendors { get; set; }
        public int ContactContractors { get; set; }

    }
}
