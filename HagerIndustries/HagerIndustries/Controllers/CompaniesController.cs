using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HagerIndustries.Data;
using HagerIndustries.Models;
using HagerIndustries.Utilities;
using HagerIndustries.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Authorization;

namespace HagerIndustries.Controllers
{
    [Authorize(Roles = "Admin,Supervisor,Employee")]
    public class CompaniesController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public CompaniesController(HagerIndustriesContext context)
        {
            _context = context;
        }

        // GET: Companies
        public async Task<IActionResult> Index(int? ActiveState, string SearchString, string typeSearchString, string actionButton, int? page, int? pageSizeID,
            string sortDirection = "desc", string sortField = "Active", CompanyTypeEnum _companyTypeEnum = CompanyTypeEnum.All)
        {
            string[] sortOptions = new[] { "Company Name", "Location", "Billing Term" };

            List<ListOptionVM> lists = new List<ListOptionVM>() {
            new ListOptionVM { ID=0,DisplayText="All Companies"},
            new ListOptionVM{ ID=1,DisplayText="Active"},
            new ListOptionVM{ ID=2,DisplayText="Inactive"}
            };


            PopulateDropDownList();
            ViewData["Filtering"] = "btn-outline-secondary";

            //Clear the sort/filter/paging URL Cookie
            CookieHelper.CookieSet(HttpContext, "CompaniesURL", "", -1);

            var companies = from c in _context.Companies
                .Include(c => c.BillingCountry)
                .Include(c => c.BillingProvince)
                .Include(c => c.BillingCity)
                .Include(c => c.BillingTerm)
                .Include(c => c.Currency)
                .Include(c => c.ShippingCountry)
                .Include(c => c.ShippingProvince)
                .Include(c => c.ShippingCity)
                .Include(c => c.CompanyContractors).ThenInclude(c => c.Contractor)
                .Include(c => c.CompanyVendors).ThenInclude(c => c.Vendor)
                .Include(c => c.CompanyCustomers).ThenInclude(c => c.Customer)
                                //.Where(c => c.PrimaryCompID == 0)
                            select c;

            var newComList = companies.Where(a => a.IsActive).OrderBy(a => a.CompName)
                .Select(grp => new OptionVM
                {
                    ID = grp.ID,
                    DisplayText = grp.CompName + "-" + (string.IsNullOrEmpty(grp.CompLocation) ? "" : grp.CompLocation)
                });
            ViewBag.newComList = newComList;

            // Hides the inActive by default
            //companies = companies.Where(c => c.IsActive == true);
            //ViewData["Filtering"] = " show";

            //This is somehow not conntected to the checkbox

            //if (!String.IsNullOrEmpty(showInactive))
            //{
            //companies = companies.Where(c => c.IsActive == false);
            //ViewData["Filtering"] = " show";
            //}
            //else
            //{
            //companies = companies.Where(c => c.IsActive == true);
            //ViewData["Filtering"] = " show";
            //}



            if (!String.IsNullOrEmpty(SearchString))
            {
                companies = companies.Where(c => c.CompName.ToUpper().Contains(SearchString.ToUpper()));
                ViewData["Filtering"] = "btn-danger";
            }

            if (!String.IsNullOrEmpty(typeSearchString))
            {
                companies = companies.Where(c => c.CompanyContractors.Any(c => c.Contractor.ContractName.ToUpper().Contains(typeSearchString.ToUpper())) ||  /*== typeSearchString.ToUpper())*/
                c.CompanyVendors.Any(c => c.Vendor.VendorName.ToUpper().Contains(typeSearchString.ToUpper())) ||  /*== typeSearchString.ToUpper())*/
                c.CompanyCustomers.Any(c => c.Customer.CustomerName.ToUpper().Contains(typeSearchString.ToUpper()))); /*== typeSearchString.ToUpper())*/
                ViewData["Filtering"] = "btn-danger";
            }

            if (ActiveState == 2)
            {
                companies = companies.Where(c => c.IsActive == false);
                ViewData["Filtering"] = "btn-danger";
                CookieHelper.CookieSet(HttpContext, "ActiveState", Convert.ToString(ActiveState), 30);
            }
            else if (ActiveState == 1)
            {
                companies = companies.Where(c => c.IsActive == true);
                ViewData["Filtering"] = "btn-danger";
                CookieHelper.CookieSet(HttpContext, "ActiveState", Convert.ToString(ActiveState), 30);
            }
            //string  TempActiveState = Convert.ToString(HttpContext.Request.Cookies["ActiveState"]);

            //int cookieActiveState = Convert.ToInt32(TempActiveState==""?"0": TempActiveState);
            ////if (!(cookieActiveState))
            ////{
            //    ActiveState = cookieActiveState;
            ViewData["ActiveState"] = new SelectList(lists, "ID", "DisplayText", ActiveState);

            // }
            //else
            //{
            //    ActiveState = Convert.ToString(HttpContext.Request.Cookies["ActiveState"]);
            //}

            //    //Value selected from DDL so use and save it to Cookie
            //    pageSize = pageSizeID.GetValueOrDefault();
            //    CookieHelper.CookieSet(HttpContext, "pageSizeValue", pageSize.ToString(), 30);
            //}
            //    else
            //    {
            //        //Not selected so see if it is in Cookie
            //        pageSize = Convert.ToInt32(HttpContext.Request.Cookies["pageSizeValue"]);

            switch (_companyTypeEnum)
            {
                case CompanyTypeEnum.Customer:
                    companies = companies.Where(p => p.IsCustomer);
                    break;
                case CompanyTypeEnum.Vendor:
                    companies = companies.Where(p => p.IsVendor);

                    break;
                case CompanyTypeEnum.Contractor:
                    companies = companies.Where(p => p.IsContractor);
                    break;
                default:
                    break;
            }

            if (!String.IsNullOrEmpty(actionButton)) //Form Submitted so lets sort!
            {
                if (actionButton != "Filter")//Change of sort is requested
                {
                    if (actionButton == sortField) //Reverse order on same field
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                        //showInactive = showInactive == "showInactive" ? "dontShowInactive" : "showInactive";
                    }
                    sortField = actionButton;//Sort by the button clicked
                }
            }

            if (sortField == "Location")
            {
                if (sortDirection == "desc") companies = companies.OrderByDescending(c => c.CompLocation);
                else companies = companies.OrderBy(c => c.CompLocation);
            }
            else if (sortField == "Active")
            {
                if (sortDirection == "desc") companies = companies.OrderByDescending(c => c.IsActive);
                else companies = companies.OrderBy(c => c.IsActive);
            }
            else if (sortField == "Billing Term")
            {
                if (sortDirection == "desc") companies = companies.OrderByDescending(c => c.BillingTerm);
                else companies = companies.OrderBy(c => c.BillingTerm);
            }
            else if (sortField == "Contractor Status")
            {
                if (sortDirection == "desc") companies = companies.OrderByDescending(c => c.IsContractor);
                else companies = companies.OrderBy(c => c.IsContractor);
            }
            else if (sortField == "Vendor Status")
            {
                if (sortDirection == "desc") companies = companies.OrderByDescending(c => c.IsVendor);
                else companies = companies.OrderBy(c => c.IsVendor);
            }
            else if (sortField == "Customer Status")
            {
                if (sortDirection == "desc") companies = companies.OrderByDescending(c => c.IsCustomer);
                else companies = companies.OrderBy(c => c.IsCustomer);
            }
            else
            {
                if (sortDirection == "desc") companies = companies.OrderByDescending(p => p.CompName);
                else companies = companies.OrderBy(p => p.CompName);
            }
            //Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            int pageSize;//This is the value we will pass to PaginatedList
            if (pageSizeID.HasValue)
            {
                //Value selected from DDL so use and save it to Cookie
                pageSize = pageSizeID.GetValueOrDefault();
                CookieHelper.CookieSet(HttpContext, "pageSizeValue", pageSize.ToString(), 30);
            }
            else
            {
                //Not selected so see if it is in Cookie
                pageSize = Convert.ToInt32(HttpContext.Request.Cookies["pageSizeValue"]);
            }
            pageSize = (pageSize == 0) ? 10 : pageSize;//Neither Selected or in Cookie so go with default
            ViewData["pageSizeID"] =
                new SelectList(new[] { "3", "5", "10", "20", "30", "40", "50", "100", "500" }, pageSize.ToString());
            var pagedData = await PaginatedList<Company>.CreateAsync(companies.AsNoTracking(), page ?? 1, pageSize);
            return View(pagedData);

            //return View(await companies.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Companies");

            var company = await _context.Companies
                .Include(c => c.BillingCountry)
                .Include(c => c.BillingProvince)
                .Include(c => c.BillingCity)
                .Include(c => c.BillingTerm)
                .Include(c => c.Currency)
                .Include(c => c.ShippingCountry)
                .Include(c => c.ShippingProvince)
                .Include(c => c.ShippingCity)
                .Include(c => c.Contacts)
                .Include(c => c.CompanyContractors).ThenInclude(c => c.Contractor)
                .Include(c => c.CompanyVendors).ThenInclude(c => c.Vendor)
                .Include(c => c.CompanyCustomers).ThenInclude(c => c.Customer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (company == null)
            {
                return NotFound();
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName", company.ID);

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Companies");

            Company company = new Company();
            PopulateAssignedContractorData(company);
            PopulateAssignedVendorData(company);
            PopulateAssignedCustomerData(company);
            PopulateDropDownList();
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CompName,CompLocation,CreditCheck,CreditCheckDate,CompPhone,CompWebsite,BillingAddressOne,BillingAddressTwo,BillingPostal,ShippingAddressOne,ShippingAddressTwo,ShippingPostal,IsActive,Note,BillingCountryID,BillingProvinceID,BillingCityID,ShippingCountryID,ShippingProvinceID,ShippingCityID,CurrencyID,BillingTermID,IsContractor,IsVendor,IsCustomer")]
        Company company, string[] selectedContractorOptions, string[] selectedVendorOptions, string[] selectedCustomerOptions)
        {
            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Companies");

            try
            {
                UpdateCompanyContractors(selectedContractorOptions, company);
                UpdateCompanyVendors(selectedVendorOptions, company);
                UpdateCompanyCustomers(selectedCustomerOptions, company);

                if (ModelState.IsValid)
                {
                    _context.Add(company);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { company.ID });

                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Companies.CompName"))
                {
                    ModelState.AddModelError("", "Unable to save changes. Remember, you cannot have duplicate Company Name.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            PopulateAssignedContractorData(company);
            PopulateAssignedVendorData(company);
            PopulateAssignedCustomerData(company);
            PopulateDropDownList(company);
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Companies");

            var company = await _context.Companies
                .Include(c => c.CompanyContractors).ThenInclude(c => c.Contractor)
                .Include(c => c.CompanyVendors).ThenInclude(c => c.Vendor)
                .Include(c => c.CompanyCustomers).ThenInclude(c => c.Customer)
                .Include(c => c.Contacts)
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.ID == id);

            if (company == null)
            {
                return NotFound();
            }
            PopulateAssignedContractorData(company);
            PopulateAssignedVendorData(company);
            PopulateAssignedCustomerData(company);
            PopulateDropDownList(company);
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName", company.ID);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string[] selectedContractorOptions, string[] selectedVendorOptions, string[] selectedCustomerOptions)
        {
            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Companies");

            var companyToUpdate = await _context.Companies
                .Include(c => c.CompanyContractors).ThenInclude(c => c.Contractor)
                .Include(c => c.CompanyVendors).ThenInclude(c => c.Vendor)
                .Include(c => c.CompanyCustomers).ThenInclude(c => c.Customer)
                .Include(c => c.Contacts)

                .SingleOrDefaultAsync(c => c.ID == id);

            if (companyToUpdate == null)
            {
                return NotFound();
            }

            UpdateCompanyContractors(selectedContractorOptions, companyToUpdate);
            UpdateCompanyVendors(selectedVendorOptions, companyToUpdate);
            UpdateCompanyCustomers(selectedCustomerOptions, companyToUpdate);

            if (await TryUpdateModelAsync<Company>
                (companyToUpdate, "", c => c.CompName, c => c.CompLocation, c => c.CreditCheck, c => c.CreditCheckDate, c => c.CompPhone, c => c.CompWebsite,
                c => c.BillingAddressOne, c => c.BillingAddressTwo, c => c.BillingPostal,
                c => c.ShippingAddressOne, c => c.ShippingAddressTwo, c => c.ShippingPostal,
                c => c.IsActive, c => c.Note,
                c => c.BillingCountryID, c => c.BillingProvinceID, c => c.BillingCityID, c => c.ShippingCountryID, c => c.ShippingProvinceID, c => c.ShippingCityID,
                c => c.CurrencyID, c => c.BillingTermID,
                c => c.IsContractor, c => c.IsVendor, c => c.IsCustomer))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { companyToUpdate.ID });
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(companyToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException dex)
                {
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Companies.CompName"))
                    {
                        ModelState.AddModelError("CompName", "Unable to save changes. Remember, you cannot have duplicate Company Name.");
                    }
                    else 
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }}
            }
            PopulateAssignedContractorData(companyToUpdate);
            PopulateAssignedVendorData(companyToUpdate);
            PopulateAssignedCustomerData(companyToUpdate);
            PopulateDropDownList(companyToUpdate);
            return View(companyToUpdate);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Companies");

            var company = await _context.Companies
                .Include(c => c.BillingCountry)
                .Include(c => c.BillingProvince)
                .Include(c => c.BillingCity)
                .Include(c => c.BillingTerm)
                .Include(c => c.Currency)
                .Include(c => c.ShippingCountry)
                .Include(c => c.ShippingProvince)
                .Include(c => c.ShippingCity)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Companies");

            var company = await _context.Companies.FindAsync(id);
            try
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
                return Redirect(ViewData["returnURL"].ToString());
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to Delete Company. Remember, you cannot delete a Company that has Contacts assigned.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(company);
        }
        private SelectList BillingCountrySelectList(int? selectedId)
        {
            return new SelectList(_context.Countries
                .OrderBy(d => d.countryName), "ID", "countryName", selectedId);
        }

        private SelectList BillingProvinceSelectList(int? selectedId)
        {
            return new SelectList(_context
                .Provinces
                .OrderBy(m => m.provName), "ID", "provName", selectedId);
        }
        private SelectList BillingCitySelectList(int? selectedId)
        {
            return new SelectList(_context.Cities
                .OrderBy(d => d.cityName), "ID", "cityName", selectedId);
        }

        private SelectList CurrencySelectList(int? selectedId)
        {
            return new SelectList(_context
                .Currencies
                .OrderBy(m => m.CurrencyName), "ID", "CurrencyName", selectedId);
        }
        private SelectList ShippingCountrySelectList(int? selectedId)
        {
            return new SelectList(_context
                .Countries
                .OrderBy(m => m.countryName), "ID", "countryName", selectedId);
        }
        private SelectList ShippingProvinceSelectList(int? selectedId)
        {
            return new SelectList(_context.Provinces
                .OrderBy(d => d.provName), "ID", "provName", selectedId);
        }

        private SelectList ShippingCitySelectList(int? selectedId)
        {
            return new SelectList(_context
                .Cities
                .OrderBy(m => m.cityName), "ID", "cityName", selectedId);
        }

        public void PopulateDropDownList(Company company = null)
        {
            ViewData["BillingCountryID"] = BillingCountrySelectList(company?.BillingCountryID);
            ViewData["BillingProvinceID"] = _context.ProvinceList(company?.BillingCountryID, company?.BillingProvinceID);
            ViewData["BillingCityID"] = _context.CityList(company?.BillingProvinceID, company?.BillingCityID);
            ViewData["BillingTermID"] = new SelectList(_context.BillingTerms, "ID", "TermName", company?.BillingTermID);
            ViewData["CurrencyID"] = CurrencySelectList(company?.CurrencyID);
            ViewData["ShippingCountryID"] = ShippingCountrySelectList(company?.ShippingCountryID);
            ViewData["ShippingProvinceID"] = _context.ProvinceList(company?.ShippingCountryID, company?.ShippingProvinceID);
            ViewData["ShippingCityID"] = _context.CityList(company?.ShippingProvinceID, company?.ShippingCityID);
        }
        public JsonResult GetBillingCountries(int? id)
        {
            return Json(BillingCountrySelectList(id));
        }
        public JsonResult GetBillingProvinces(int? id)
        {
            return Json(BillingProvinceSelectList(id));
        }
        public JsonResult GetBillingCities(int? id)
        {
            return Json(BillingCitySelectList(id));
        }
        public JsonResult GetCurrencies(int? id)
        {
            return Json(CurrencySelectList(id));
        }
        public JsonResult GetShippingCountries(int? id)
        {
            return Json(ShippingCountrySelectList(id));
        }
        public JsonResult GetShippingProvinces(int? id)
        {
            return Json(ShippingProvinceSelectList(id));
        }
        public JsonResult GetShippingCities(int? id)
        {
            return Json(ShippingCitySelectList(id));
        }
        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.ID == id);
        }

        private void PopulateAssignedContractorData(Company company)
        {
            var allOptions = _context.Contractors;
            var currentOptionsHS = new HashSet<int>(company.CompanyContractors.Select(b => b.ContractorID));
            var selected = new List<ListOptionVM>();
            var available = new List<ListOptionVM>();
            foreach (var s in allOptions)
            {
                if (currentOptionsHS.Contains(s.ID))
                {
                    selected.Add(new ListOptionVM
                    {
                        ID = s.ID,
                        DisplayText = s.ContractName
                    });
                }
                else
                {
                    available.Add(new ListOptionVM
                    {
                        ID = s.ID,
                        DisplayText = s.ContractName
                    });
                }
            }

            ViewData["selContractorOpts"] = new MultiSelectList(selected.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availContractorOpts"] = new MultiSelectList(available.OrderBy(s => s.DisplayText), "ID", "DisplayText");
        }
        private void PopulateAssignedVendorData(Company company)
        {
            var allOptions = _context.Vendors;
            var currentOptionsHS = new HashSet<int>(company.CompanyVendors.Select(b => b.VendorID));
            var selected = new List<ListOptionVM>();
            var available = new List<ListOptionVM>();
            foreach (var s in allOptions)
            {
                if (currentOptionsHS.Contains(s.ID))
                {
                    selected.Add(new ListOptionVM
                    {
                        ID = s.ID,
                        DisplayText = s.VendorName
                    });
                }
                else
                {
                    available.Add(new ListOptionVM
                    {
                        ID = s.ID,
                        DisplayText = s.VendorName
                    });
                }
            }

            ViewData["selVendorOpts"] = new MultiSelectList(selected.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availVendorOpts"] = new MultiSelectList(available.OrderBy(s => s.DisplayText), "ID", "DisplayText");
        }

        private void PopulateAssignedCustomerData(Company company)
        {
            var allOptions = _context.Customers;
            var currentOptionsHS = new HashSet<int>(company.CompanyCustomers.Select(b => b.CustomerID));
            var selected = new List<ListOptionVM>();
            var available = new List<ListOptionVM>();
            foreach (var s in allOptions)
            {
                if (currentOptionsHS.Contains(s.ID))
                {
                    selected.Add(new ListOptionVM
                    {
                        ID = s.ID,
                        DisplayText = s.CustomerName
                    });
                }
                else
                {
                    available.Add(new ListOptionVM
                    {
                        ID = s.ID,
                        DisplayText = s.CustomerName
                    });
                }
            }

            ViewData["selCustomerOpts"] = new MultiSelectList(selected.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availCustomerOpts"] = new MultiSelectList(available.OrderBy(s => s.DisplayText), "ID", "DisplayText");
        }

        private void UpdateCompanyContractors(string[] selectedOptions, Company companyToUpdate)
        {
            if (selectedOptions == null)
            {
                companyToUpdate.CompanyContractors = new List<CompanyContractor>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var currentOptionsHS = new HashSet<int>(companyToUpdate.CompanyContractors.Select(b => b.ContractorID));
            foreach (var s in _context.Contractors)
            {
                if (selectedOptionsHS.Contains(s.ID.ToString()))
                {
                    if (!currentOptionsHS.Contains(s.ID))
                    {
                        companyToUpdate.CompanyContractors.Add(new CompanyContractor
                        {
                            ContractorID = s.ID,
                            CompanyID = companyToUpdate.ID
                        });
                    }
                }
                else
                {
                    if (currentOptionsHS.Contains(s.ID))
                    {
                        CompanyContractor contractToRemove = companyToUpdate.CompanyContractors.SingleOrDefault(d => d.ContractorID == s.ID);
                        _context.Remove(contractToRemove);
                    }
                }
            }
        }

        private void UpdateCompanyCustomers(string[] selectedOptions, Company companyToUpdate)
        {
            if (selectedOptions == null)
            {
                companyToUpdate.CompanyCustomers = new List<CompanyCustomer>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var currentOptionsHS = new HashSet<int>(companyToUpdate.CompanyCustomers.Select(b => b.CustomerID));
            foreach (var s in _context.Customers)
            {
                if (selectedOptionsHS.Contains(s.ID.ToString()))
                {
                    if (!currentOptionsHS.Contains(s.ID))
                    {
                        companyToUpdate.CompanyCustomers.Add(new CompanyCustomer
                        {
                            CustomerID = s.ID,
                            CompanyID = companyToUpdate.ID
                        });
                    }
                }
                else
                {
                    if (currentOptionsHS.Contains(s.ID))
                    {
                        CompanyCustomer customerToRemove = companyToUpdate.CompanyCustomers.SingleOrDefault(d => d.CustomerID == s.ID);
                        _context.Remove(customerToRemove);
                    }
                }
            }
        }

        private void UpdateCompanyVendors(string[] selectedOptions, Company companyToUpdate)
        {
            if (selectedOptions == null)
            {
                companyToUpdate.CompanyVendors = new List<CompanyVendor>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var currentOptionsHS = new HashSet<int>(companyToUpdate.CompanyVendors.Select(b => b.VendorID));
            foreach (var s in _context.Vendors)
            {
                if (selectedOptionsHS.Contains(s.ID.ToString()))
                {
                    if (!currentOptionsHS.Contains(s.ID))
                    {
                        companyToUpdate.CompanyVendors.Add(new CompanyVendor
                        {
                            VendorID = s.ID,
                            CompanyID = companyToUpdate.ID
                        });
                    }
                }
                else
                {
                    if (currentOptionsHS.Contains(s.ID))
                    {
                        CompanyVendor vendorToRemove = companyToUpdate.CompanyVendors.SingleOrDefault(d => d.VendorID == s.ID);
                        _context.Remove(vendorToRemove);
                    }
                }
            }
        }
        [HttpGet]
        public ActionResult LoadMergeData(int Primary, int Merger)
        {



            //Creating List    
            int[] list = new int[] { Primary, Merger };
            CompanyMerge companyMerge = new CompanyMerge();
            var companies = from c in _context.Companies
                .Include(c => c.BillingCountry)
                .Include(c => c.BillingProvince)
                .Include(c => c.BillingCity)
                .Include(c => c.BillingTerm)
                .Include(c => c.Currency)
                .Include(c => c.ShippingCountry)
                .Include(c => c.ShippingProvince)
                .Include(c => c.ShippingCity)
                .Include(c => c.Contacts)
                .Include(c => c.CompanyContractors).ThenInclude(c => c.Contractor)
                .Include(c => c.CompanyVendors).ThenInclude(c => c.Vendor)
                .Include(c => c.CompanyCustomers).ThenInclude(c => c.Customer)
                            select c;

            companies = companies.Where(c => list.Contains(c.ID));
            companyMerge.Primary = companies.Where(c => c.ID == Primary).FirstOrDefault();
            companyMerge.Merger = companies.Where(c => c.ID == Merger).FirstOrDefault();


            //Adding records to list    
            return PartialView("_MergeCompany", companyMerge);

        }
        [HttpPost]
        public async Task<int> MergeCompanies(List<MergeData> final, int Primary, int Merger)
        {
            int[] list = new int[] { Primary, Merger };
            CompanyMerge companyMerge = new CompanyMerge();
            var companies = from c in _context.Companies
                .Include(c => c.BillingCountry)
                .Include(c => c.BillingProvince)
                .Include(c => c.BillingCity)
                .Include(c => c.BillingTerm)
                .Include(c => c.Currency)
                .Include(c => c.ShippingCountry)
                .Include(c => c.ShippingProvince)
                .Include(c => c.ShippingCity)
                .Include(c => c.Contacts)
                .Include(c => c.CompanyContractors).ThenInclude(c => c.Contractor)
                .Include(c => c.CompanyVendors).ThenInclude(c => c.Vendor)
                .Include(c => c.CompanyCustomers).ThenInclude(c => c.Customer)
                            select c;

            companies = companies.Where(c => list.Contains(c.ID));
            companyMerge.Primary = companies.Where(c => c.ID == Primary).FirstOrDefault();
            companyMerge.Merger = companies.Where(c => c.ID == Merger).FirstOrDefault();

            foreach (var item in final)
            {
                if (item.field == "Contacts")
                    companyMerge.Primary.Contacts.Add(companyMerge.Merger.Contacts.Where(c => c.ID == (Convert.ToInt32(item.value))).FirstOrDefault());
                if (item.value == "merger")
                {
                    switch (item.field)
                    {
                        case "CompLocation":
                            companyMerge.Primary.CompLocation = companyMerge.Merger.CompLocation;
                            break;
                        case "IsActive":
                            companyMerge.Primary.IsActive = companyMerge.Merger.IsActive;
                            break;
                        case "IsContractor":
                            companyMerge.Primary.IsContractor = companyMerge.Merger.IsContractor;
                            break;
                        case "BillingData":

                            // case "BillingAddressOne":
                            companyMerge.Primary.BillingAddressOne = companyMerge.Merger.BillingAddressOne;
                            // break;
                            // case "BillingAddressTwo":
                            companyMerge.Primary.BillingAddressTwo = companyMerge.Merger.BillingAddressTwo;
                            //break;
                            // case "BillingCity":
                            companyMerge.Primary.BillingCity = companyMerge.Merger.BillingCity;
                            companyMerge.Primary.BillingCityID = companyMerge.Merger.BillingCityID;
                            //  break;
                            // case "BillingCountry":
                            companyMerge.Primary.BillingCountry = companyMerge.Merger.BillingCountry;
                            companyMerge.Primary.BillingCountryID = companyMerge.Merger.BillingCountryID;
                            //  break;
                            //case "BillingPostal":
                            companyMerge.Primary.BillingPostal = companyMerge.Merger.BillingPostal;
                            //  break;
                            //case "BillingProvince":
                            companyMerge.Primary.BillingProvince = companyMerge.Merger.BillingProvince;
                            companyMerge.Primary.BillingProvinceID = companyMerge.Merger.BillingProvinceID;
                            break;
                        case "BillingTerm":
                            companyMerge.Primary.BillingTerm = companyMerge.Merger.BillingTerm;
                            companyMerge.Primary.BillingTermID = companyMerge.Merger.BillingTermID;
                            break;
                        case "CompanyContractors":
                            companyMerge.Primary.CompanyContractors = companyMerge.Merger.CompanyContractors;
                            break;
                        case "CompanyCustomers":
                            companyMerge.Primary.CompanyCustomers = companyMerge.Merger.CompanyCustomers;
                            break;
                        case "CompanyVendors":
                            companyMerge.Primary.CompanyVendors = companyMerge.Merger.CompanyVendors;
                            break;
                        case "CompPhone":
                            companyMerge.Primary.CompPhone = companyMerge.Merger.CompPhone;
                            break;
                        case "CompWebsite":
                            companyMerge.Primary.CompWebsite = companyMerge.Merger.CompWebsite;
                            break;
                        case "CreditCheck":
                            companyMerge.Primary.CreditCheck = companyMerge.Merger.CreditCheck;
                            break;
                        case "CreditCheckDate":
                            companyMerge.Primary.CreditCheckDate = companyMerge.Merger.CreditCheckDate;
                            break;
                        case "Currency":
                            companyMerge.Primary.Currency = companyMerge.Merger.Currency;
                            companyMerge.Primary.CurrencyID = companyMerge.Merger.CurrencyID;
                            break;

                        case "IsCustomer":
                            companyMerge.Primary.IsCustomer = companyMerge.Merger.IsCustomer;
                            break;
                        case "IsVendor":
                            companyMerge.Primary.IsVendor = companyMerge.Merger.IsVendor;
                            break;
                        case "Note":
                            companyMerge.Primary.Note = companyMerge.Merger.Note;
                            break;
                        case "ShippingData":

                            // case "ShippingAddressOne":
                            companyMerge.Primary.ShippingAddressOne = companyMerge.Merger.ShippingAddressOne;
                            // break;

                            // case "ShippingAddressTwo":
                            companyMerge.Primary.ShippingAddressTwo = companyMerge.Merger.ShippingAddressTwo;
                            // break;
                            //case "ShippingCity":
                            companyMerge.Primary.ShippingCity = companyMerge.Merger.ShippingCity;
                            companyMerge.Primary.ShippingCityID = companyMerge.Merger.ShippingCityID;
                            // break;
                            //case "ShippingCountry":
                            companyMerge.Primary.ShippingCountry = companyMerge.Merger.ShippingCountry;
                            companyMerge.Primary.ShippingCountryID = companyMerge.Merger.ShippingCountryID;
                            //break;
                            //case "ShippingPostal":
                            companyMerge.Primary.ShippingPostal = companyMerge.Merger.ShippingPostal;
                            //break;
                            //case "ShippingProvince":
                            companyMerge.Primary.ShippingProvince = companyMerge.Merger.ShippingProvince;
                            companyMerge.Primary.ShippingProvinceID = companyMerge.Merger.ShippingProvinceID;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (await TryUpdateModelAsync<Company>
                (companyMerge.Primary, "", c => c.CompLocation
                ,
                c => c.CompLocation, c => c.CreditCheck, c => c.CreditCheckDate, c => c.CompPhone, c => c.CompWebsite,
                c => c.BillingAddressOne, c => c.BillingAddressTwo, c => c.BillingPostal,
                c => c.ShippingAddressOne, c => c.ShippingAddressTwo, c => c.ShippingPostal,
                c => c.IsActive, c => c.Note,
                c => c.BillingCountryID, c => c.BillingProvinceID, c => c.BillingCityID, c => c.ShippingCountryID, c => c.ShippingProvinceID, c => c.ShippingCityID,
                c => c.CurrencyID, c => c.BillingTermID,
                c => c.IsContractor, c => c.IsVendor, c => c.IsCustomer
                ))
            {
                try
                {
                    companyMerge.Merger.PrimaryCompID = companyMerge.Primary.ID;
                    companyMerge.Merger.IsActive = false;

                    await _context.SaveChangesAsync();



                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(companyMerge.Primary.ID))
                    {
                        //  return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    return 0;
                }
            }
            return 1;
        }
        //public ActionResult ShowInactive(bool visible)
        //{
        //    if (showInactive)
        //    {
        //        companies = companies.Where(c => c.IsActive == false);
        //        ViewData["Filtering"] = " show";
        //    }
        //}
    }
}
