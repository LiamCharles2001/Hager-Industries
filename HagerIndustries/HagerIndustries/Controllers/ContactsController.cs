using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HagerIndustries.Data;
using HagerIndustries.Models;
using HagerIndustries.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;
using HagerIndustries.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace HagerIndustries.Controllers
{
    [Authorize(Roles = "Admin,Supervisor,Employee")]
    public class ContactsController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public ContactsController(HagerIndustriesContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index(int? ActiveState, string SearchString, int? page, int? pageSizeID, 
            int? CompanyID, string actionButton, string sortDirectionCheck, string sortFieldID, string sortDirection = "desc", string sortField = "Active",
            CompanyTypeEnum _companyTypeEnum = CompanyTypeEnum.All)
        {
            PopulateDropDownLists();
            ViewData["Filtering"] = "btn-outline-secondary";
            List<ListOptionVM> lists = new List<ListOptionVM>() {
            new ListOptionVM { ID=0,DisplayText="All"},
            new ListOptionVM{ ID=1,DisplayText="Active"},
            new ListOptionVM{ ID=2,DisplayText="Inactive"}
            };
            // Clear the sort/ filter / paging URL Cookie
            CookieHelper.CookieSet(HttpContext, "ContactsURL", "", -1);

            string[] sortOptions = new[] { "Active", "Name", "Company", "Job Title" };

            var contacts = from p in _context.Contacts
                 .Include(p => p.Company)
                           select p;

            if (CompanyID.HasValue)
            {
                contacts = contacts.Where(p => p.CompanyID == CompanyID);
                ViewData["Filtering"] = "btn-danger";
            }

            if (!String.IsNullOrEmpty(SearchString))
            {
                contacts = contacts.Where(p => p.LastName.ToUpper().Contains(SearchString.ToUpper()) || p.FirstName.ToUpper().Contains(SearchString.ToUpper()));
                ViewData["Filtering"] = "btn-danger";
            }

            if (ActiveState == 2)
            {
                contacts = contacts.Where(c => c.IsActive == false);
                ViewData["Filtering"] = "btn-danger";
                CookieHelper.CookieSet(HttpContext, "ActiveStateC", Convert.ToString(ActiveState), 30);
            }
            else if (ActiveState == 1)
            {
                contacts = contacts.Where(c => c.IsActive == true);
                ViewData["Filtering"] = "btn-danger";
                CookieHelper.CookieSet(HttpContext, "ActiveStateC", Convert.ToString(ActiveState), 30);
            }
            //string  TempActiveState = Convert.ToString(HttpContext.Request.Cookies["ActiveState"]);

            //int cookieActiveState = Convert.ToInt32(TempActiveState==""?"0": TempActiveState);
            ////if (!(cookieActiveState))
            ////{
            //    ActiveState = cookieActiveState;
            ViewData["ActiveState"] = new SelectList(lists, "ID", "DisplayText", ActiveState);


            switch (_companyTypeEnum)
            {
                case CompanyTypeEnum.Customer:
                    contacts = contacts.Include(a=>a.Company).Where(p => p.Company.IsCustomer);
                    break;
                case CompanyTypeEnum.Vendor:
                    contacts = contacts.Include(a => a.Company).Where(p => p.Company.IsVendor);

                    break;
                case CompanyTypeEnum.Contractor:
                    contacts = contacts.Include(a => a.Company).Where(p => p.Company.IsContractor);
                    break;
                default:
                    break;
            }
            if (!String.IsNullOrEmpty(actionButton)) //Form Submitted so lets sort!
            {
                page = 1;//Reset page to start

                if (sortOptions.Contains(actionButton))//Change of sort is requested
                {
                    if (actionButton == sortField) sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    sortField = actionButton; // Sort by the button clicked
                }
                else //Sort by the controls in the filter area
                {
                    sortDirection = String.IsNullOrEmpty(sortDirectionCheck) ? "asc" : "desc";
                    sortField = sortFieldID;
                }
                //Save sort in cookie
                CookieHelper.CookieSet(HttpContext, "cookieSort", sortField, 30);
                CookieHelper.CookieSet(HttpContext, "cookieSortDirection", sortDirection, 30);
            }
            else
            {
                //May be coming back to the page so see if sort is in cookie
                string cookieSort = HttpContext.Request.Cookies["cookieSort"];
                if (!String.IsNullOrEmpty(cookieSort))
                {
                    sortField = cookieSort;
                    sortDirection = HttpContext.Request.Cookies["cookieSortDirection"];
                }
            }

            if (sortField == "Job Title")
            {
                if (sortDirection == "desc") contacts = contacts.OrderByDescending(p => p.JobTitle);
                else contacts = contacts.OrderBy(p => p.JobTitle);
            }
            else if (sortField == "Active")
            {
                if (sortDirection == "desc") contacts = contacts.OrderByDescending(p => p.IsActive);
                else contacts = contacts.OrderBy(p => p.IsActive);
            }

            else if (sortField == "Company")
            {
                if (sortDirection == "desc") contacts = contacts.OrderByDescending(p => p.Company);
                else contacts = contacts.OrderBy(p => p.Company);
            }
            else // Finally, sort by name
            {
                if (sortDirection == "desc") contacts = contacts.OrderByDescending(p => p.LastName).ThenByDescending(p => p.FirstName);
                else contacts = contacts.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);
            }

            //Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;
            ViewBag.sortFieldID = new SelectList(sortOptions, sortField.ToString());

            //Handle Paging
            int pageSize;//This is the value we will pass to PaginatedList
            if (pageSizeID.HasValue)
            {
                // Value selected from DDL so use and save it to Cookie
                pageSize = pageSizeID.GetValueOrDefault();
                CookieHelper.CookieSet(HttpContext, "pageSizeValue", pageSize.ToString(), 30);
            }
            else pageSize = Convert.ToInt32(HttpContext.Request.Cookies["pageSizeValue"]);

            pageSize = (pageSize == 0) ? 10 : pageSize;//Neither Selected or in Cookie so go with default
            ViewData["pageSizeID"] = new SelectList(new[] { "3", "5", "10", "20", "30", "40", "50", "100", "500" }, pageSize.ToString());
            var pagedData = await PaginatedList<Contact>.CreateAsync(contacts.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Contacts");

            var contact = await _context.Contacts
                .Include(c => c.Company)
                .Include(d => d.ContactCategories).ThenInclude(d => d.Category)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (contact == null) return NotFound();

            return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Contacts");

            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName");
            var contact = new Contact();
            PopulateAssignedCategoryData(contact);
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,JobTitle,CellPhone,WorkPhone,Email,IsActive,Note,CompanyID")] Contact contact,string[] selectedOptions)
        {
            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Contacts");

            try
            {
                //Add the selected skills
                if (selectedOptions != null)
                {
                    foreach (var category in selectedOptions)
                    {
                        var categoryToAdd = new ContactCategory { ContactID = contact.ID, CategoryID = int.Parse(category) };
                        contact.ContactCategories.Add(categoryToAdd);
                    }
                }
                if (ModelState.IsValid)
                {
                    _context.Add(contact);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { contact.ID });
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("Email"))
                {
                    ModelState.AddModelError("", "Unable to save changes. Remember, you cannot have duplicate eMail addresses.");
                }
                else if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Contacts.FirstName, Contacts.LastName"))
                {
                    ModelState.AddModelError("", "Unable to save changes. Remember, you cannot have duplicate First and Last name.");
                }
                //else
                //{
                //    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                //}
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName", contact.CompanyID);
            PopulateAssignedCategoryData(contact);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Contacts");

            var contact = await _context.Contacts
                .Include(c => c.Company)
                .Include(a => a.ContactCategories).ThenInclude(d => d.Category)
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.ID == id);
            if (contact == null)
            {
                return NotFound();
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName", contact.CompanyID);
            PopulateAssignedCategoryData(contact);
            
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contact contact, string[] selectedOptions)
        {
            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Contacts");

            var contactToUpdate = await _context.Contacts
                .Include(c => c.Company)
                .Include(a => a.ContactCategories).ThenInclude(d => d.Category)
                .SingleOrDefaultAsync(a => a.ID == id);
            //Check that you got it or exit with a not found error
            if (contactToUpdate == null)
            {
                return NotFound();
            }
            UpdateContactCategories(selectedOptions, contactToUpdate);

            if (await TryUpdateModelAsync<Contact>(contactToUpdate, "",
                 p => p.FirstName, p => p.LastName, p => p.JobTitle, p => p.CellPhone, p => p.WorkPhone,
                 p => p.Email, p => p.IsActive, p => p.Note, p => p.CompanyID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { contactToUpdate.ID });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contactToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateException dex)
                {
                    if (dex.GetBaseException().Message.Contains("Email"))
                    {
                        ModelState.AddModelError("", "Unable to save changes. Remember, you cannot have duplicate eMail addresses.");
                    }
                    else if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Contacts.FirstName, Contacts.LastName"))
                    {
                        ModelState.AddModelError("", "Unable to save changes. Remember, you cannot have duplicate First and Last name.");
                    }
                    //else
                    //{
                    //    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    //}
                }
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName", contact.CompanyID);

            PopulateAssignedCategoryData(contactToUpdate);
            return View(contactToUpdate);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Contacts");

            var contact = await _context.Contacts
                .Include(c => c.Company)
                .Include(a => a.ContactCategories).ThenInclude(d => d.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Contacts");

            var contact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return Redirect(ViewData["returnURL"].ToString());
        }       

        

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ID == id);
        }
        private void PopulateAssignedCategoryData(Contact contact)
        {
            var allOptions = _context.Categories;
            var currentOptionIDs = new HashSet<int>(contact.ContactCategories.Select(b => b.CategoryID));
            var checkBoxes = new List<OptionVM>();
            foreach (var option in allOptions)
            {
                checkBoxes.Add(new OptionVM
                {
                    ID = option.ID,
                    DisplayText = option.Name,
                    Assigned = currentOptionIDs.Contains(option.ID)
                });
            }
            ViewData["CategoriesOptions"] = checkBoxes;
        }
        private void UpdateContactCategories(string[] selectedOptions, Contact contactToUpdate)
        {
            if (selectedOptions == null)
            {
                contactToUpdate.ContactCategories = new List<ContactCategory>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var categoryOptionsHS = new HashSet<int>
                (contactToUpdate.ContactCategories.Select(c => c.CategoryID));//IDs of the currently selected conditions
            foreach (var option in _context.Categories)
            {
                if (selectedOptionsHS.Contains(option.ID.ToString()))
                {
                    if (!categoryOptionsHS.Contains(option.ID))
                    {
                        contactToUpdate.ContactCategories.Add(new ContactCategory { ContactID = contactToUpdate.ID, CategoryID = option.ID });
                    }
                }
                else
                {
                    if (categoryOptionsHS.Contains(option.ID))
                    {
                        ContactCategory conditionToRemove = contactToUpdate.ContactCategories.SingleOrDefault(c => c.CategoryID == option.ID);
                        _context.Remove(conditionToRemove);
                    }
                }
            }
        }
        private SelectList CompanySelectList(int? selectedId)
        {
            return new SelectList(_context
                .Companies
                .OrderBy(m => m.CompName), "ID", "CompName", selectedId);
        }

        //Now we can use the SelectList method to get the data
        //for our Pop-up
        [HttpGet]
        public JsonResult GetCompanies(int? id)
        {
            return Json(CompanySelectList(id));
        }

        private void PopulateDropDownLists(Contact contact = null)
        {
            ViewData["CompanyID"] = CompanySelectList(contact?.CompanyID);

        }
    }
}
