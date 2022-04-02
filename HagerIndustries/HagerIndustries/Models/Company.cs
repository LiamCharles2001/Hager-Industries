using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Models
{
    public class Company
    {
        public Company()
        {
            //this.Contacts = new HashSet<Contact>();
            //this.CompanyTypes = new HashSet<CompanyType>();
            CompanyContractors = new HashSet<CompanyContractor>();
            CompanyVendors = new HashSet<CompanyVendor>();
            CompanyCustomers = new HashSet<CompanyCustomer>();
            IsActive = true;
        }

        public int ID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "You cannot leave the first name blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string CompName { get; set; }

        [Display(Name = "Main Location")]
        [StringLength(100, ErrorMessage = "Location cannot be more than 100 characters long.")]
        public string CompLocation { get; set; }

        [Display(Name = "Credit Check")]
        public bool CreditCheck { get; set; }

        [Display(Name = "Date of Credit Check")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CreditCheckDate { get; set; }

        [Display(Name = "Phone")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64? CompPhone { get; set; }

        [Display(Name = "Website")]
        [StringLength(100, ErrorMessage = "Web Address cannot be more than 100 characters long.")]
        public string CompWebsite { get; set; }

        [Display(Name = "Primary Billing Address")]
        [StringLength(100, ErrorMessage = "Address cannot be more than 100 characters long.")]
        public string BillingAddressOne { get; set; }

        [Display(Name = "Secondary Billing Address")]
        [StringLength(100, ErrorMessage = "Address cannot be more than 100 characters long.")]
        public string BillingAddressTwo { get; set; }

        [Display(Name = "Billing Postal Code")]
        [StringLength(20, ErrorMessage = "Postal code cannot be more than 20 characters long.")]
        public string BillingPostal { get; set; }

        [Display(Name = "Primary Shipping Address")]
        [StringLength(100, ErrorMessage = "Address cannot be more than 100 characters long.")]
        public string ShippingAddressOne { get; set; }

        [Display(Name = "Secondary Shipping Address")]
        [StringLength(100, ErrorMessage = "Address cannot be more than 100 characters long.")]
        public string ShippingAddressTwo { get; set; }

        [Display(Name = "Shipping Postal Code")]
        [StringLength(20, ErrorMessage = "Postal code cannot be more than 20 characters long.")]
        public string ShippingPostal { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Note")]
        [DataType(DataType.MultilineText)]
        [StringLength(255, ErrorMessage = "Note cannot exceed 255 characters.")]
        public string Note { get; set; }

        [Display(Name = "Billing Country")]
        //[Required(ErrorMessage = "You must select a Country for your Billing Address.")]
        public int? BillingCountryID { get; set; }

        [Display(Name = "Billing Country")]
        public Country BillingCountry { get; set; }

        [Display(Name = "Billing Province")]
        //[Required(ErrorMessage = "You must select a Province.")]
        public int? BillingProvinceID { get; set; }

        [Display(Name = "Billing Province")]
        public Province BillingProvince { get; set; }

        [Display(Name = "Billing City")]
        //[Required(ErrorMessage = "You must select a City.")]
        public int? BillingCityID { get; set; }

        [Display(Name = "Billing City")]
        public City BillingCity { get; set; }

        [Display(Name = "Shipping Country")]
        //[Required(ErrorMessage = "You must select a Country.")]
        public int? ShippingCountryID { get; set; }

        [Display(Name = "Shipping Country")]
        public Country ShippingCountry { get; set; }

        [Display(Name = "Shipping Province")]
        //[Required(ErrorMessage = "You must select a Province.")]
        public int? ShippingProvinceID { get; set; }

        [Display(Name = "Shipping Province")]
        public Province ShippingProvince { get; set; }

        [Display(Name = "Shipping City")]
        //[Required(ErrorMessage = "You must select a City.")]
        public int? ShippingCityID { get; set; }

        [Display(Name = "Shipping City")]
        public City ShippingCity { get; set; }

        [Display(Name = "Currency")]
        public int? CurrencyID { get; set; }
        public Currency Currency { get; set; }

        [Display(Name = "Billing Terms")]
        public int? BillingTermID { get; set; }

        [Display(Name = "Billing Terms")]
        public BillingTerm BillingTerm { get; set; }


        [Display(Name = "Contractor")]
        public bool IsContractor { get; set; }
        public ICollection<CompanyContractor> CompanyContractors { get; set; }

        [Display(Name = "Vendor")]
        public bool IsVendor { get; set; }
        public ICollection<CompanyVendor> CompanyVendors { get; set; }

        [Display(Name = "Customer")]
        public bool IsCustomer { get; set; }
        public ICollection<CompanyCustomer> CompanyCustomers { get; set; }

        //public ICollection<CompanyType> CompanyTypes { get; set; }
        //public int ContactID { get; set; }
        //public Contact Contact { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public int PrimaryCompID { get; set; }
    }


}