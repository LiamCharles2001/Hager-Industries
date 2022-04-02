using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class Contact
    {
        public Contact()
        {
            ContactCategories = new HashSet<ContactCategory>();
            IsActive = true;
        }
        public int ID { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave the first name blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave the last name blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string LastName { get; set; }

        [Display(Name = "Job Title")]
        [StringLength(100, ErrorMessage = "Address cannot be more than 100 characters long.")]
        public string JobTitle { get; set; }

        [Display(Name = "Cell Number")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64? CellPhone { get; set; }

        [Display(Name = "Work Phone")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64? WorkPhone { get; set; }

        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Contact Categories")]
        public ICollection<ContactCategory> ContactCategories { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        
        [Display(Name = "Note")]
        [DataType(DataType.MultilineText)]
        [StringLength(255, ErrorMessage = "Note cannot exceed 255 characters.")]
        public string Note { get; set; }

        [Display(Name = "Company")]
        public int CompanyID { get; set; }
        public Company Company { get; set; }

       

    }
}
