using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class Event
    {
        public string ID { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "You cannot leave the title of the event blank.")]
        public string Title { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "You cannot leave the start date blank.")]
        public string Start { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "You cannot leave the end date blank.")]
        public string End { get; set; }

        public bool AllDay { get; set; }
    }
}
