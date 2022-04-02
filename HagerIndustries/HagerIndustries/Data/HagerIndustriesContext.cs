using HagerIndustries.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Data
{
    public class HagerIndustriesContext : DbContext
    {
        public HagerIndustriesContext(DbContextOptions<HagerIndustriesContext> options)
            : base(options)
        {
        }

        public DbSet<Position> Positions { get; set; }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<BillingTerm> BillingTerms { get; set; }
        public DbSet<CompanyContractor> CompanyContractors { get; set; }
        public DbSet<ContactContractor> ContactContractors { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<CompanyCustomer> CompanyCustomers { get; set; }
        public DbSet<ContactCustomer> ContactCustomers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CompanyVendor> CompanyVendors { get; set; }
        public DbSet<ContactVendor> ContactVendors { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactCategory> ContactCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Event> Events { get; set; }


        //public DbSet<CompanyType> CompanyTypes { get; set; }
        //public DbSet<TypeDetail> TypeDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("HI");
            modelBuilder.Entity<Category>()
               .HasIndex(p => p.Name)
               .IsUnique();

            //Many to Many Primary Keys
            modelBuilder.Entity<ContactCategory>()
              .HasKey(a => new { a.ContactID, a.CategoryID });

            
            modelBuilder.Entity<EmployeeSkill>()
            .HasKey(t => new { t.EmployeeID, t.SkillID });

            modelBuilder.Entity<CompanyContractor>()
            .HasKey(t => new { t.CompanyID, t.ContractorID });

            modelBuilder.Entity<CompanyVendor>()
            .HasKey(t => new { t.CompanyID, t.VendorID });

            modelBuilder.Entity<CompanyCustomer>()
            .HasKey(t => new { t.CompanyID, t.CustomerID });

            modelBuilder.Entity<ContactContractor>()
           .HasKey(t => new { t.ContactID, t.ContractorID });

            modelBuilder.Entity<ContactVendor>()
            .HasKey(t => new { t.ContactID, t.VendorID });

            modelBuilder.Entity<ContactCustomer>()
            .HasKey(t => new { t.ContactID, t.CustomerID });

            //Province cascade Delete Constraints
            modelBuilder.Entity<Province>()
                .HasMany<Employee>(d => d.Employees)
                .WithOne(p => p.Province)
                .HasForeignKey(p => p.ProvinceID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Province>()
               .HasMany<Company>(d => d.ShippingCompanies)
               .WithOne(p => p.ShippingProvince)
               .HasForeignKey(p => p.ShippingProvinceID)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Province>()
               .HasMany<Company>(d => d.BillingCompanies)
               .WithOne(p => p.BillingProvince)
               .HasForeignKey(p => p.BillingProvinceID)
               .OnDelete(DeleteBehavior.Restrict);
            // Ends Here

            // Country Cascade Delete Constraints
            modelBuilder.Entity<Country>()
                .HasMany<Employee>(d => d.Employees)
                .WithOne(p => p.Country)
                .HasForeignKey(p => p.CountryID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Country>()
                .HasMany<Company>(d => d.BillingCompanies)
                .WithOne(p => p.BillingCountry)
                .HasForeignKey(p => p.BillingCountryID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Country>()
                .HasMany<Company>(d => d.ShippingCompanies)
                .WithOne(p => p.ShippingCountry)
                .HasForeignKey(p => p.ShippingCountryID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Country>()
                .HasMany<Province>(d => d.Provinces)
                .WithOne(p => p.Country)
                .HasForeignKey(p => p.CountryID)
                .OnDelete(DeleteBehavior.Restrict);
            // Ends Here

            // Other cascade deletes
            modelBuilder.Entity<Employment>()
                .HasMany<Employee>(d => d.Employees)
                .WithOne(p => p.Employment)
                .HasForeignKey(p => p.EmploymentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Position>()
                .HasMany<Employee>(d => d.Employees)
                .WithOne(p => p.Position)
                .HasForeignKey(p => p.PositionID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Currency>()
               .HasMany<Company>(d => d.Companies)
               .WithOne(p => p.Currency)
               .HasForeignKey(p => p.CurrencyID)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BillingTerm>()
               .HasMany<Company>(d => d.Companies)
               .WithOne(p => p.BillingTerm)
               .HasForeignKey(p => p.BillingTermID)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Skill>()
                .HasMany<EmployeeSkill>(p => p.EmployeeSkills)
                .WithOne(c => c.Skill)
                .HasForeignKey(c => c.SkillID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contractor>()
                .HasMany<CompanyContractor>(p => p.CompanyContractors)
                .WithOne(c => c.Contractor)
                .HasForeignKey(c => c.ContractorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vendor>()
                .HasMany<CompanyVendor>(p => p.CompanyVendors)
                .WithOne(c => c.Vendor)
                .HasForeignKey(c => c.VendorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasMany<CompanyCustomer>(p => p.CompanyCustomers)
                .WithOne(c => c.Customer)
                .HasForeignKey(c => c.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Company>()
                .HasMany<Contact>(p => p.Contacts)
                .WithOne(c => c.Company)
                .HasForeignKey(c => c.CompanyID)
                .OnDelete(DeleteBehavior.Restrict);

            //Unique Position Name
            modelBuilder.Entity<Position>()
            .HasIndex(p => p.PosName)
            .IsUnique();

            //Unique Skill Name
            modelBuilder.Entity<Skill>()
               .HasIndex(p => p.SkillName)
               .IsUnique();

            //Unique Employment type
            modelBuilder.Entity<Employment>()
            .HasIndex(p => p.EmplType)
            .IsUnique();

            //Unique Employee Email
            modelBuilder.Entity<Employee>()
            .HasIndex(p => p.Email)
            .IsUnique();

            //Unique Keyfob
            modelBuilder.Entity<Employee>()
            .HasIndex(p => p.KeyFobNumber)
            .IsUnique();

            //Unique Currency Name
            modelBuilder.Entity<Currency>()
            .HasIndex(p => p.CurrencyName)
            .IsUnique();

            //Unique Billing Term Name
            modelBuilder.Entity<BillingTerm>()
            .HasIndex(p => p.TermName)
            .IsUnique();

            //Unique Type Name
            //modelBuilder.Entity<CompanyType>()
            //.HasIndex(p => p.CompanyTypeName)
            //.IsUnique();

            //Unique Country Name
            modelBuilder.Entity<Country>()
                .HasIndex(p => p.countryName)
                .IsUnique();

            modelBuilder.Entity<Province>()
               .HasIndex(p => new { p.CountryID, p.provName })
               .IsUnique();

            modelBuilder.Entity<City>()
               .HasIndex(p => new { p.ProvinceID, p.cityName })
               .IsUnique();

            //Unique Company Name
            modelBuilder.Entity<Company>()
              .HasIndex(p => new { p.CompName})
              .IsUnique();

            //Unique Contact Name
            modelBuilder.Entity<Contact>()
             .HasIndex(p => new { p.Email })
             .IsUnique();

            modelBuilder.Entity<Category>()
               .HasMany<ContactCategory>(d => d.ContactCategories)
               .WithOne(p => p.Category)
               .HasForeignKey(p => p.CategoryID)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contact>()
              .HasIndex(p => new { p.FirstName, p.LastName })
              .IsUnique();

            modelBuilder.Entity<Employee>()
              .HasIndex(p => new { p.FirstName, p.LastName })
              .IsUnique();

        }
        //public DbSet<CompanyType> CompanyTypes { get; set; }
        //public DbSet<TypeDetail> TypeDetails { get; set; }


        //public DbSet<HagerIndustries.Models.ContactCategory> ContactCategory { get; set; }
    }
}
