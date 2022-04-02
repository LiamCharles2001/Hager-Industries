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
    public class ContactCustomersController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public ContactCustomersController(HagerIndustriesContext context)
        {
            _context = context;
        }

        // GET: ContactCustomers
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Contacts", new { _companyTypeEnum = "Customer" });
        }

        // GET: ContactCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactCustomer = await _context.ContactCustomers
                .Include(c => c.Contact)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.ContactID == id);
            if (contactCustomer == null)
            {
                return NotFound();
            }

            return View(contactCustomer);
        }

        // GET: ContactCustomers/Create
        public IActionResult Create()
        {
            ViewData["ContactID"] = new SelectList(_context.Contacts, "ID", "Email");
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID");
            return View();
        }

        // POST: ContactCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,ContactID")] ContactCustomer contactCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactID"] = new SelectList(_context.Contacts, "ID", "Email", contactCustomer.ContactID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", contactCustomer.CustomerID);
            return View(contactCustomer);
        }

        // GET: ContactCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactCustomer = await _context.ContactCustomers.FindAsync(id);
            if (contactCustomer == null)
            {
                return NotFound();
            }
            ViewData["ContactID"] = new SelectList(_context.Contacts, "ID", "Email", contactCustomer.ContactID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", contactCustomer.CustomerID);
            return View(contactCustomer);
        }

        // POST: ContactCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,ContactID")] ContactCustomer contactCustomer)
        {
            if (id != contactCustomer.ContactID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactCustomerExists(contactCustomer.ContactID))
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
            ViewData["ContactID"] = new SelectList(_context.Contacts, "ID", "Email", contactCustomer.ContactID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", contactCustomer.CustomerID);
            return View(contactCustomer);
        }

        // GET: ContactCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactCustomer = await _context.ContactCustomers
                .Include(c => c.Contact)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.ContactID == id);
            if (contactCustomer == null)
            {
                return NotFound();
            }

            return View(contactCustomer);
        }

        // POST: ContactCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactCustomer = await _context.ContactCustomers.FindAsync(id);
            _context.ContactCustomers.Remove(contactCustomer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactCustomerExists(int id)
        {
            return _context.ContactCustomers.Any(e => e.ContactID == id);
        }
    }
}
