using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class Announcement: IValidatableObject
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        

        [Display(Name = "Expire Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpireDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ExpireDate < DateTime.Today.AddDays(1))
            {
                yield return new ValidationResult("Date of Expire must be in the future.", new[] { "ExpireDate" });
            }
        }
    }
}
