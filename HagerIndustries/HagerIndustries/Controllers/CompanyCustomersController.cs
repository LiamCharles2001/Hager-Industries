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

namespace HagerIndustries.Controllers
{
    [Authorize(Roles = "Admin,Supervisor,Employee")]
    public class CompanyCustomersController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public CompanyCustomersController(HagerIndustriesContext context)
        {
            _context = context;
        }

        // GET: CompanyCustomers
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Companies", new { _companyTypeEnum = "Customer" });
        }

        // GET: CompanyCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyCustomer = await _context.CompanyCustomers
                .Include(c => c.Company)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CompanyID == id);
            if (companyCustomer == null)
            {
                return NotFound();
            }

            return View(companyCustomer);
        }

        // GET: CompanyCustomers/Create
        public IActionResult Create()
        {
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName");
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID");
            return View();
        }

        // POST: CompanyCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,CompanyID")] CompanyCustomer companyCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName", companyCustomer.CompanyID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", companyCustomer.CustomerID);
            return View(companyCustomer);
        }

        // GET: CompanyCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyCustomer = await _context.CompanyCustomers.FindAsync(id);
            if (companyCustomer == null)
            {
                return NotFound();
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName", companyCustomer.CompanyID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", companyCustomer.CustomerID);
            return View(companyCustomer);
        }

        // POST: CompanyCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,CompanyID")] CompanyCustomer companyCustomer)
        {
            if (id != companyCustomer.CompanyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyCustomerExists(companyCustomer.CompanyID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName", companyCustomer.CompanyID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", companyCustomer.CustomerID);
            return View(companyCustomer);
        }

        // GET: CompanyCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyCustomer = await _context.CompanyCustomers
                .Include(c => c.Company)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CompanyID == id);
            if (companyCustomer == null)
            {
                return NotFound();
            }

            return View(companyCustomer);
        }

        // POST: CompanyCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyCustomer = await _context.CompanyCustomers.FindAsync(id);
            _context.CompanyCustomers.Remove(companyCustomer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyCustomerExists(int id)
        {
            return _context.CompanyCustomers.Any(e => e.CompanyID == id);
        }
    }
}
