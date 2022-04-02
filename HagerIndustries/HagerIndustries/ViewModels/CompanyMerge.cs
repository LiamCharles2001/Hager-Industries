using HagerIndustries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.ViewModels
{
    public class CompanyMerge
    {
        public Company Primary { get; set; }
        public Company Merger { get; set; }

    }
    public class MergeData {
        public string field { get; set; }
        public string value { get; set; }
    }
}
