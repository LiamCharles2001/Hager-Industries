using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class City
    {
        public City()
        {
        }

        public int ID { get; set; }

        [Display(Name = "City Name")]
        [Required(ErrorMessage = "You cannot leave Name blank.")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters long.")]
        public string cityName { get; set; }

        [Display(Name = "Province")]
        public int ProvinceID { get; set; }
        public Province Province { get; set; }
    }
}
