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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using HagerIndustries.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using HagerIndustries.Utilities;

namespace HagerIndustries.Controllers
{
    [Authorize(Roles = "Employee,Admin,Supervisor")]
    public class EmployeesController : Controller
    {
        private readonly HagerIndustriesContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public EmployeesController(HagerIndustriesContext context, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        // GET: Employees
        public async Task<IActionResult> Index(int? ActiveState, string SearchString, int? PositionID,
            int? page, int? pageSizeID, string actionButton, string sortDirectionCheck, string sortFieldID, string sortDirection = "desc", string sortField = "Active")
        {

            string[] sortOptions = new[] { "Active", "Name", "Email", "Position", "Employment Type", "Country" };

            PopulateDropDownLists();
            List<ListOptionVM> lists = new List<ListOptionVM>() {
            new ListOptionVM { ID=0,DisplayText="All"},
            new ListOptionVM{ ID=1,DisplayText="Active"},
            new ListOptionVM{ ID=2,DisplayText="Inactive"}
            };
            // Instead of keeping the filter open, change colour of button
            ViewData["Filtering"] = "btn-outline-secondary";

            //Clear the sort/filter/paging URL Cookie
            CookieHelper.CookieSet(HttpContext, "EmployeesURL", "", -1);

            var employees = from p in _context.Employees
                .Include(e => e.Country)
                .Include(e => e.Employment)
                .Include(e => e.Position)
                .Include(e => e.Province)
                .Include(d => d.EmployeeSkills).ThenInclude(d => d.Skill)
                            select p;           

            if (PositionID.HasValue)
            {
                employees = employees.Where(p => p.PositionID == PositionID);
                ViewData["Filtering"] = "btn-danger";
            }

            if (!String.IsNullOrEmpty(SearchString))
            {
                employees = employees.Where(p => p.FirstName.ToUpper().Contains(SearchString.ToUpper())
                                            || p.LastName.ToUpper().Contains(SearchString.ToUpper()));
                ViewData["Filtering"] = "btn-danger";
            }

            if (ActiveState == 2)
            {
                employees = employees.Where(c => c.IsActive == false);
                ViewData["Filtering"] = "btn-danger";
                CookieHelper.CookieSet(HttpContext, "ActiveStateE", Convert.ToString(ActiveState), 30);
            }
            else if (ActiveState == 1)
            {
                employees = employees.Where(c => c.IsActive == true);
                ViewData["Filtering"] = "btn-danger";
                CookieHelper.CookieSet(HttpContext, "ActiveStateE", Convert.ToString(ActiveState), 30);
            }
            //string  TempActiveState = Convert.ToString(HttpContext.Request.Cookies["ActiveState"]);

            //int cookieActiveState = Convert.ToInt32(TempActiveState==""?"0": TempActiveState);
            ////if (!(cookieActiveState))
            ////{
            //    ActiveState = cookieActiveState;
            ViewData["ActiveState"] = new SelectList(lists, "ID", "DisplayText", ActiveState);

            //if (!String.IsNullOrEmpty(isActiveCheck))
            //{
            //    employees = employees.Where(c => c.IsActive != Convert.ToBoolean(isActiveCheck));
            //    ViewData["Filtering"] = "btn-danger";
            //}

            if (!String.IsNullOrEmpty(actionButton)) // Form submitted - time to sort
            {
                page = 1;

                if (sortOptions.Contains(actionButton))// Change of sort is requested
                {
                    if (actionButton == sortField) sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    sortField = actionButton; // Sort by the button clicked
                }
                else // Sort by the controls in the filter area
                {
                    sortDirection = String.IsNullOrEmpty(sortDirectionCheck) ? "asc" : "desc";
                    sortField = sortFieldID;
                }
                CookieHelper.CookieSet(HttpContext, "cookieSort", sortField, 30);
                CookieHelper.CookieSet(HttpContext, "cookieSortDirection", sortDirection, 30);
            }
            else
            {
                string cookieSort = HttpContext.Request.Cookies["cookieSort"];
                if (!String.IsNullOrEmpty(cookieSort))
                {
                    sortField = cookieSort;
                    sortDirection = HttpContext.Request.Cookies["cookieSortDirection"];
                }

            }

            if (sortField == "Active")
            {
                if (sortDirection == "desc") employees = employees.OrderByDescending(p => p.IsActive);
                else employees = employees.OrderBy(p => p.IsActive);
            }

            else if (sortField == "Position")
            {
                if (sortDirection == "desc") employees = employees.OrderByDescending(p => p.Position);
                else employees = employees.OrderBy(p => p.Position);
            }

            else if (sortField == "Employment Type")
            {
                if (sortDirection == "desc") employees = employees.OrderByDescending(p => p.Employment);
                else employees = employees.OrderBy(p => p.Employment);
            }

            else if (sortField == "Country")
            {
                if (sortDirection == "desc") employees = employees.OrderByDescending(p => p.Country);
                else employees = employees.OrderBy(p => p.Country);
            }

            else //Sorting by Patient Name
            {
                if (sortDirection == "desc") employees = employees.OrderByDescending(p => p.LastName).ThenByDescending(p => p.FirstName);
                else employees = employees.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);
            }
            //Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;
            ViewBag.sortFieldID = new SelectList(sortOptions, sortField.ToString());

            //Handle Paging
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

            var pagedData = await PaginatedList<Employee>.CreateAsync(employees.AsNoTracking(), page ?? 1, pageSize);

            //employees.
            return View(pagedData);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id, string returnURL)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Employees");

            var employee = await _context.Employees
                .Include(e => e.Country)
                .Include(e => e.Employment)
                .Include(e => e.Position)
                .Include(e => e.Province)
                .Include(d => d.EmployeeSkills).ThenInclude(d => d.Skill)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Employees");

            Employee employee = new Employee();
            PopulateAssignedSkillData(employee);
            PopulateDropDownLists();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,AddressOne,AddressTwo,Postal,CellPhone,HomePhone,Email,DOB,DateJoined,Wage,Expense,KeyFobNumber,IsActive,IsUser,EmergencyContactName,EmergencyContactPhone,Note,PositionID,EmploymentID,CountryID,ProvinceID")] Employee employee
            , string[] selectedOptions)
        {
            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Employees");

            try
            {
                UpdateEmployeeSkills(selectedOptions, employee);
                if (ModelState.IsValid)
                {
                    _context.Add(employee);

                    if (employee.IsUser)
                    {
                        var user = new IdentityUser { UserName = employee.Email, Email = employee.Email };
                        var result = await _userManager.CreateAsync(user, "password");
                        await _context.SaveChangesAsync();

                        //Auto Password Reset email on account and employee creation
                        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ResetPassword",
                            pageHandler: null,
                            values: new { area = "Identity", code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(
                            employee.Email,
                            "Reset Password - Hager Industries CRM Application",
                            $"This is an automated message from Hager Industries CRM Application. <br>Please click this link in order to reset your password: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Password Reset Link</a>.");
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { employee.ID });
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Employees.FirstName, Employees.LastName"))
                {
                    ModelState.AddModelError("", "Unable to save changes. Remember, you cannot have duplicate Employee First and Last name.");
                }
                else if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Employee.Email"))
                {
                    ModelState.AddModelError("Email", "Unable to save changes. Remember, you cannot have duplicate Emails.");
                }
                else if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Employee.KeyFobNumber"))
                {
                    ModelState.AddModelError("KeyFobNumber", "Unable to save changes. Remember, you cannot have duplicate Key Fobs.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            PopulateAssignedSkillData(employee);
            PopulateDropDownLists(employee);
            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Employees");

            var employee = await _context.Employees
                .Include(e => e.EmployeeSkills)
                .ThenInclude(e => e.Skill)
                .Include(d => d.EmployeeSkills).ThenInclude(d => d.Skill)
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.ID == id);

            if (employee == null)
            {
                return NotFound();
            }
            PopulateAssignedSkillData(employee);
            PopulateDropDownLists(employee);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string[] selectedOptions)
        {
            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Employees");

            var employeeToUpdate = await _context.Employees
                .Include(e => e.EmployeeSkills)
                .ThenInclude(e => e.Skill)
                //.AsNoTracking()
                .SingleOrDefaultAsync(e => e.ID == id);

            if (employeeToUpdate == null)
            {
                return NotFound();
            }

            UpdateEmployeeSkills(selectedOptions, employeeToUpdate);

            if (await TryUpdateModelAsync<Employee>(employeeToUpdate, "",
                p => p.FirstName, p => p.LastName, p => p.AddressOne, p => p.AddressTwo, p => p.Postal, p => p.CellPhone, p => p.HomePhone, p => p.Email, p => p.DOB, p => p.DateJoined, p => p.Wage, p => p.Expense,
                p => p.KeyFobNumber, p => p.IsActive, p => p.IsUser, p => p.EmergencyContactName, p => p.EmergencyContactPhone, p => p.Note, p => p.EmploymentID, p => p.PositionID, p => p.CountryID, p => p.ProvinceID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { employeeToUpdate.ID });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employeeToUpdate.ID))
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
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Employees.FirstName, Employees.LastName"))
                    {
                        ModelState.AddModelError("", "Unable to save changes. Remember, you cannot have duplicate Employee First and Last name.");
                    }
                    else if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Employee.Email"))
                    {
                        ModelState.AddModelError("Email", "Unable to save changes. Remember, you cannot have duplicate Emails.");
                    }
                    else if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Employee.KeyFobNumber"))
                    {
                        ModelState.AddModelError("KeyFobNumber", "Unable to save changes. Remember, you cannot have duplicate Key Fobs.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }
                }
            }
            PopulateAssignedSkillData(employeeToUpdate);
            PopulateDropDownLists(employeeToUpdate);
            return View(employeeToUpdate);
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Employees");

            var employee = await _context.Employees
                .Include(e => e.Country)
                .Include(e => e.Employment)
                .Include(e => e.Position)
                .Include(e => e.Province)
                .Include(d => d.EmployeeSkills).ThenInclude(d => d.Skill)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string returnURL)
        {
            var employee = await _context.Employees.FindAsync(id);
            //Get the URL with the last filter, sort and page parameters
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Employees");
            try
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return Redirect(ViewData["returnURL"].ToString());
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to delete record. Try again, and if the problem persists see your system administrator.");
            }
            return View(employee);
        }

        private void PopulateAssignedSkillData(Employee employee)
        {
            var allOptions = _context.Skills;
            var currentOptionsHS = new HashSet<int>(employee.EmployeeSkills.Select(b => b.SkillID));
            var selected = new List<ListOptionVM>();
            var available = new List<ListOptionVM>();
            foreach (var s in allOptions)
            {
                if (currentOptionsHS.Contains(s.ID))
                {
                    selected.Add(new ListOptionVM
                    {
                        ID = s.ID,
                        DisplayText = s.SkillName
                    });
                }
                else
                {
                    available.Add(new ListOptionVM
                    {
                        ID = s.ID,
                        DisplayText = s.SkillName
                    });
                }
            }

            ViewData["selOpts"] = new MultiSelectList(selected.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availOpts"] = new MultiSelectList(available.OrderBy(s => s.DisplayText), "ID", "DisplayText");
        }
        private void UpdateEmployeeSkills(string[] selectedOptions, Employee employeeToUpdate)
        {
            if (selectedOptions == null)
            {
                employeeToUpdate.EmployeeSkills = new List<EmployeeSkill>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var currentOptionsHS = new HashSet<int>(employeeToUpdate.EmployeeSkills.Select(b => b.SkillID));
            foreach (var s in _context.Skills)
            {
                if (selectedOptionsHS.Contains(s.ID.ToString()))
                {
                    if (!currentOptionsHS.Contains(s.ID))
                    {
                        employeeToUpdate.EmployeeSkills.Add(new EmployeeSkill
                        {
                            SkillID = s.ID,
                            EmployeeID = employeeToUpdate.ID
                        });
                    }
                }
                else
                {
                    if (currentOptionsHS.Contains(s.ID))
                    {
                        EmployeeSkill skillToRemove = employeeToUpdate.EmployeeSkills.SingleOrDefault(d => d.SkillID == s.ID);
                        _context.Remove(skillToRemove);
                    }
                }
            }
        }
        private SelectList PositionSelectList(int? selectedId)
        {
            return new SelectList(_context
                .Positions
                .OrderBy(m => m.PosName), "ID", "PosName", selectedId);
        }
        //private SelectList CountrySelectList(int? selectedId)
        //{
        //    return new SelectList(_context.Countries
        //        .OrderBy(d => d.countryName), "ID", "countryName", selectedId);
        //}

        //private SelectList ProvinceList(int? selectedId)
        //{
        //    return new SelectList(_context
        //        .Provinces
        //        .OrderBy(m => m.provName)
        //        .ThenBy(m=>m.CountryID), "ID", "provName", selectedId);
        //}
        

        private void PopulateDropDownLists(Employee employee = null)
        {
            ViewData["PositionID"] = PositionSelectList(employee?.PositionID);
            ViewData["EmploymentID"] = new SelectList(_context.Employments, "ID", "EmplType", employee?.EmploymentID);
            ViewData["CountryID"] = _context.CountrySelectList(employee?.CountryID);
            //if (employee != null)
                //ViewData["ProvinceID"] = ProvinceList(employee?.ProvinceID);
            //else
                ViewData["ProvinceID"] = _context.ProvinceList(employee?.CountryID,employee?.ProvinceID);
            
        }
        //Now we can use the SelectList method to get the data
        //for our Pop-up
        [HttpGet]
        public JsonResult GetPositions(int? id)
        {
            return Json(PositionSelectList(id));
        }
        [HttpGet]
        public JsonResult GetCountries(int? id)
        {
            return Json(_context.CountrySelectList(id));
        }
        [HttpGet]
        public JsonResult GetProvinces(int countryid,int? id)
        {
            return Json(_context.ProvinceList(countryid,id));
        }
       
       
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }
    }
}
