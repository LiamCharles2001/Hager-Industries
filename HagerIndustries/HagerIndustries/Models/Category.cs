using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class Category : Auditable
    {
        public Category()
        {
            ContactCategories = new HashSet<ContactCategory>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "You cannot leave Name of the Category blank.")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters long.")]
        public string Name { get; set; }
        public int SortIndex { get; set; }
        public ICollection<ContactCategory> ContactCategories { get; set; }

    }
}
