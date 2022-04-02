using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class Skill
    {
        public Skill()
        {
            EmployeeSkills = new HashSet<EmployeeSkill>();

        }
        public int ID { get; set; }

        [Display(Name = "Employee Skill")]
        [Required(ErrorMessage = "You cannot leave the name of the Skill blank.")]
        [StringLength(100, ErrorMessage = "Skill name is too long, the longest allowed name is 100 characters long")]
        public string SkillName { get; set; }
        public int SortIndex { get; set; }
        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
