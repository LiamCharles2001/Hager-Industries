using HagerIndustries.Data;
using HagerIndustries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Controllers
{
    [Authorize(Roles = "Admin,Supervisor")]
    public class LookupsController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public LookupsController(HagerIndustriesContext context)
        {
            _context = context;
        }

        public IActionResult Index(string Tab)
        {
            ViewData["Tab"] = Tab;

            return View();
        }

        public PartialViewResult Countries()
        {
            ViewData["CountriesID"] = new
                SelectList(_context.Countries
                .OrderBy(a => a.countryName), "ID", "countryName");

            return PartialView("_Countries");
        }

        public PartialViewResult Provinces()
        {
            ViewData["CountryID"] = new SelectList(_context.Countries, "ID", "countryName");
            //ViewData["ProvinceID"] = new
            //    SelectList(_context.Provinces
            //    .OrderBy(a => a.provName), "ID", "provName");
            ViewData["ProvincesID"] = new SelectList(string.Empty, "ID", "provName");

            return PartialView("_Provinces");
        }

        public PartialViewResult Cities()
        {
            ViewData["ProvinceID"] = new SelectList(_context.Provinces, "ID", "provName");
            ViewData["CitiesID"] = new SelectList(string.Empty, "ID", "cityName");
            ViewData["CountryID"] = new SelectList(_context.Countries, "ID", "countryName");
            return PartialView("_Cities");
        }

        public PartialViewResult Skills()
        {
            List<Skill> skills = new List<Skill>();
            skills = _context.Skills.ToList();
            for (int i = 0; i < skills.Count; i++)
            {
                if (skills[i].SortIndex == 0)
                    skills[i].SortIndex = 100;
            }
            skills = skills.OrderBy(a => a.SortIndex).ToList();
            ViewBag.LookupSort = skills;
            return PartialView("_Skills");
        }
        [HttpPost]
        public ActionResult UpdateSkills(int[] locationId)
        {
            int preference = 1;
            foreach (int id in locationId)
            {
                var holidayLocation = _context.Skills.Find(id);
                holidayLocation.SortIndex = preference;
                _context.SaveChanges();
                preference += 1;
            }
            return RedirectToAction("Index", "Lookups", new { Tab = "SkillsTab" });
        }
        public PartialViewResult Positions()
        {
            List<Position> skills = new List<Position>();
            skills = _context.Positions.ToList();
            for (int i = 0; i < skills.Count; i++)
            {
                if (skills[i].SortIndex == 0)
                    skills[i].SortIndex = 100;
            }
            skills = skills.OrderBy(a => a.SortIndex).ToList();

            ViewBag.LookupSort = skills;
            return PartialView("_Positions");
        }
        [HttpPost]
        public ActionResult UpdatePositions(int[] locationId)
        {
            int preference = 1;
            foreach (int id in locationId)
            {
                var holidayLocation = _context.Positions.Find(id);
                holidayLocation.SortIndex = preference;
                _context.SaveChanges();
                preference += 1;
            }
            return RedirectToAction("Index", "Lookups", new { Tab = "PositionsTab" });
        }
        public PartialViewResult Currencies()
        {
            List<Currency> currencies = new List<Currency>();
            currencies = _context.Currencies.ToList();
            for (int i = 0; i < currencies.Count; i++)
            {
                if (currencies[i].SortIndex == 0)
                    currencies[i].SortIndex = 100;
            }
            currencies = currencies.OrderBy(a => a.SortIndex).ToList();

            ViewBag.LookupSort = currencies;
            return PartialView("_Currencies");
        }
        [HttpPost]
        public ActionResult UpdateCurrencies(int[] locationId)
        {
            int preference = 1;
            foreach (int id in locationId)
            {
                var holidayLocation = _context.Currencies.Find(id);
                holidayLocation.SortIndex = preference;
                _context.SaveChanges();
                preference += 1;
            }
            return RedirectToAction("Index", "Lookups", new { Tab = "CurrenciesTab" });
        }
        public PartialViewResult BillingTerms()
        {
            List<BillingTerm> billingTerms = new List<BillingTerm>();
            billingTerms = _context.BillingTerms.ToList();
            for (int i = 0; i < billingTerms.Count; i++)
            {
                if (billingTerms[i].SortIndex == 0)
                    billingTerms[i].SortIndex = 100;
            }
            billingTerms = billingTerms.OrderBy(a => a.SortIndex).ToList();

            ViewBag.LookupSort = billingTerms;
            return PartialView("_BillingTerms");
        }
        [HttpPost]
        public ActionResult UpdateBillingTerms(int[] locationId)
        {
            int preference = 1;
            foreach (int id in locationId)
            {
                var holidayLocation = _context.BillingTerms.Find(id);
                holidayLocation.SortIndex = preference;
                _context.SaveChanges();
                preference += 1;
            }
            return RedirectToAction("Index", "Lookups", new { Tab = "BillingTermsTab" });
        }
        public PartialViewResult Categories()
        {
            List<Category> categories = new List<Category>();
            categories = _context.Categories.ToList();
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].SortIndex == 0)
                    categories[i].SortIndex = 100;
            }
            categories = categories.OrderBy(a => a.SortIndex).ToList();

            ViewBag.LookupSort = categories;
            return PartialView("_Categories");
        }
        [HttpPost]
        public ActionResult UpdateCategories(int[] locationId)
        {
            int preference = 1;
            foreach (int id in locationId)
            {
                var holidayLocation = _context.Categories.Find(id);
                holidayLocation.SortIndex = preference;
                _context.SaveChanges();
                preference += 1;
            }
            return RedirectToAction("Index", "Lookups", new { Tab = "CategoriesTab" });
        }

        public PartialViewResult Contractors()
        {
            List<Contractor> contractors = new List<Contractor>();
            contractors = _context.Contractors.ToList();
            for (int i = 0; i < contractors.Count; i++)
            {
                if (contractors[i].SortIndex == 0)
                    contractors[i].SortIndex = 100;
            }
            contractors = contractors.OrderBy(a => a.SortIndex).ToList();

            ViewBag.LookupSort = contractors;
            return PartialView("_Contractors");
        }
        [HttpPost]
        public ActionResult UpdateContractors(int[] locationId)
        {
            int preference = 1;
            foreach (int id in locationId)
            {
                var holidayLocation = _context.Contractors.Find(id);
                holidayLocation.SortIndex = preference;
                _context.SaveChanges();
                preference += 1;
            }
            return RedirectToAction("Index", "Lookups", new { Tab = "ContractorsTab" });
        }

        public PartialViewResult Customers()
        {
            List<Customer> customers = new List<Customer>();
            customers = _context.Customers.ToList();
            for (int i = 0; i <customers.Count; i++)
            {
                if (customers[i].SortIndex == 0)
                    customers[i].SortIndex = 100;
            }
            customers = customers.OrderBy(a => a.SortIndex).ToList();

            ViewBag.LookupSort = customers;
            return PartialView("_Customers");
        }
        [HttpPost]
        public ActionResult UpdateCustomers(int[] locationId)
        {
            int preference = 1;
            foreach (int id in locationId)
            {
                var holidayLocation = _context.Customers.Find(id);
                holidayLocation.SortIndex = preference;
                _context.SaveChanges();
                preference += 1;
            }
            return RedirectToAction("Index", "Lookups", new { Tab = "CustomersTab" });
        }

        public PartialViewResult Vendors()
        {
            List<Vendor> vendors = new List<Vendor>();
            vendors = _context.Vendors.ToList();
            for (int i = 0; i < vendors.Count; i++)
            {
                if (vendors[i].SortIndex == 0)
                    vendors[i].SortIndex = 100;
            }
            vendors = vendors.OrderBy(a => a.SortIndex).ToList();

            ViewBag.LookupSort = vendors;
            return PartialView("_Vendors");
        }
        [HttpPost]
        public ActionResult UpdateVendors(int[] locationId)
        {
            int preference = 1;
            foreach (int id in locationId)
            {
                var holidayLocation = _context.Vendors.Find(id);
                holidayLocation.SortIndex = preference;
                _context.SaveChanges();
                preference += 1;
            }
            return RedirectToAction("Index", "Lookups", new { Tab = "VendorsTab" });
        }
    }
}
