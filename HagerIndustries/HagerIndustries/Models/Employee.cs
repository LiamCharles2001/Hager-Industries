using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class Employee
    {

        public Employee()
        {            
            EmployeeSkills = new HashSet<EmployeeSkill>();
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

        [Display(Name = "Main Address")]
        [StringLength(100, ErrorMessage = "Address cannot be more than 100 characters long.")]
        public string AddressOne { get; set; }

        [Display(Name = "Secondary Address")]        
        [StringLength(100, ErrorMessage = "Address cannot be more than 100 characters long.")]
        public string AddressTwo { get; set; }

        [Display(Name = "Postal Code")]
        [StringLength(20, ErrorMessage = "Postal code cannot be more than 20 characters long.")]
        public string Postal { get; set; }

        [Display(Name = "Cell Number")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64? CellPhone { get; set; }

        [Display(Name = "Home Phone")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64? HomePhone { get; set; }

        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }

        [Display(Name = "Company Join Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateJoined { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(9,2)")]//So only 5 bytes to store in SQL Server
        [Range(0, 9999999.99, ErrorMessage = "Invalid wage.")]
        public decimal? Wage { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(9,2)")]//So only 5 bytes to store in SQL Server
        [Range(0, 9999999.99, ErrorMessage = "Invalid expense.")]
        public decimal? Expense { get; set; }

        [Display(Name = "Keyfob Number")]
        [RegularExpression("^\\d{7}$", ErrorMessage = "Please enter a valid 7-digit keyfob number (no spaces or special characters).")]
        [DisplayFormat(DataFormatString ="{0:###:####}", ApplyFormatInEditMode = true)]
        public string? KeyFobNumber { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "CRM User")]
        public bool IsUser { get; set; }

        [Display(Name = "Emergency Contact")]
        [StringLength(100, ErrorMessage = "Emergency contact name cannot be more than 100 characters long.")]
        public string EmergencyContactName { get; set; }

        [Display(Name = "Emergency Contact Phone")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64? EmergencyContactPhone { get; set; }

        [Display(Name = "Note")]
        [DataType(DataType.MultilineText)]
        [StringLength(255, ErrorMessage = "Note cannot exceed 255 characters.")]
        public string Note { get; set; }

        [Display(Name = "Position")]
        public int? PositionID { get; set; }

        public Position Position { get; set; }

        [Display(Name = "Type of Employment")]
        public int? EmploymentID { get; set; }

        public Employment Employment { get; set; }

        [Display(Name = "Country")]
        public int? CountryID { get; set; }

        public Country Country { get; set; }

        [Display(Name = "Province")]
        public int? ProvinceID { get; set; }

        public Province Province { get; set; }
        [Display(Name = "Skills")]
        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DOB>DateTime.Today)
            {
                yield return new ValidationResult("Date of Birth cannot be in the future.", new[] { "DOB" });
            }
        }
    }
}
