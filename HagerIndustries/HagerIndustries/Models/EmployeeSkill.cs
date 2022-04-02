using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class EmployeeSkill
    {
        public int SkillID { get; set; }
        public Skill Skill { get; set; }
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        
    }
}
