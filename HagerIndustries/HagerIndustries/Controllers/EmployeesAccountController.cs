using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HagerIndustries.Data;
using HagerIndustries.Models;
using Microsoft.AspNetCore.Authorization;
using HagerIndustries.Utilities;

namespace HagerIndustries.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeesAccountController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public EmployeesAccountController(HagerIndustriesContext context)
        {
            _context = context;
        }

        // GET: EmployeesAccount
        public IActionResult Index()
        {
            var hagerIndustriesContext = _context.Employees.Include(e => e.Employment).Include(e => e.Position);
            return RedirectToAction(nameof(Details));
        }

        // GET: EmployeesAccount/Details/5
        public async Task<IActionResult> Details()
        {


            var employee = await _context.Employees
               .Where(c => c.Email == User.Identity.Name)
               .FirstOrDefaultAsync();
            if (employee == null)
            {
                return RedirectToAction(nameof(Create));
            }

            return View(employee);
        }

        // GET: EmployeesAccount/Create
        public IActionResult Create()
        {
            ViewData["EmploymentID"] = new SelectList(_context.Employments, "ID", "EmplType");
            ViewData["PositionID"] = new SelectList(_context.Positions, "ID", "PosName");
            return View();
        }

        // POST: EmployeesAccount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,AddressOne,AddressTwo,Postal,CellPhone,HomePhone,Email,DOB,DateJoined,Wage,Expense,KeyFobNumber,IsUser,EmergencyContactName,EmergencyContactPhone,Note,PositionID,EmploymentID")] Employee employee)
        {
            employee.Email = User.Identity.Name;
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    UpdateUserNameCookie(employee.FullName);
                    return RedirectToAction(nameof(Details));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewData["EmploymentID"] = new SelectList(_context.Employments, "ID", "EmplType", employee.EmploymentID);
            ViewData["PositionID"] = new SelectList(_context.Positions, "ID", "PosName", employee.PositionID);
            return View(employee);
        }

        // GET: EmployeesAccount/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var employee = await _context.Employees
                .Where(c => c.Email == User.Identity.Name)
                .FirstOrDefaultAsync();
            if (employee == null)
            {
                return RedirectToAction(nameof(Create));
            }
            ViewData["EmploymentID"] = new SelectList(_context.Employments, "ID", "EmplType", employee.EmploymentID);
            ViewData["PositionID"] = new SelectList(_context.Positions, "ID", "PosName", employee.PositionID);
            return View(employee);
        }

        // POST: EmployeesAccount/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var employeeToUpdate = await _context.Employees
                .FirstOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<Employee>(employeeToUpdate, "",
                c => c.FirstName, c => c.LastName, c => c.AddressOne, c => c.CellPhone))
            {
                try
                {
                    _context.Update(employeeToUpdate);
                    await _context.SaveChangesAsync();
                    UpdateUserNameCookie(employeeToUpdate.FullName);
                    return RedirectToAction(nameof(Details));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employeeToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. The record you attempted to edit "
                                + "was modified by another user after you received your values.  You need to go back and try your edit again.");
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Something went wrong in the database.");
                }
            }
            return View(employeeToUpdate);
        }
        private void UpdateUserNameCookie(string userName)
        {
            CookieHelper.CookieSet(HttpContext, "userName", userName, 960);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }

    }   
}
