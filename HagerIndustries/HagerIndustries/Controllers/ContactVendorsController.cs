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
    public class ContactVendorsController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public ContactVendorsController(HagerIndustriesContext context)
        {
            _context = context;
        }

        // GET: ContactVendors
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Contacts", new { _companyTypeEnum = "Vendor" });

        }

        // GET: ContactVendors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactVendor = await _context.ContactVendors
                .Include(c => c.Contact)
                .Include(c => c.Vendor)
                .FirstOrDefaultAsync(m => m.ContactID == id);
            if (contactVendor == null)
            {
                return NotFound();
            }

            return View(contactVendor);
        }

        // GET: ContactVendors/Create
        public IActionResult Create()
        {
            ViewData["ContactID"] = new SelectList(_context.Contacts, "ID", "Email");
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "ID");
            return View();
        }

        // POST: ContactVendors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorID,ContactID")] ContactVendor contactVendor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactVendor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactID"] = new SelectList(_context.Contacts, "ID", "Email", contactVendor.ContactID);
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "ID", contactVendor.VendorID);
            return View(contactVendor);
        }

        // GET: ContactVendors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactVendor = await _context.ContactVendors.FindAsync(id);
            if (contactVendor == null)
            {
                return NotFound();
            }
            ViewData["ContactID"] = new SelectList(_context.Contacts, "ID", "Email", contactVendor.ContactID);
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "ID", contactVendor.VendorID);
            return View(contactVendor);
        }

        // POST: ContactVendors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendorID,ContactID")] ContactVendor contactVendor)
        {
            if (id != contactVendor.ContactID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactVendor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactVendorExists(contactVendor.ContactID))
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
            ViewData["ContactID"] = new SelectList(_context.Contacts, "ID", "Email", contactVendor.ContactID);
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "ID", contactVendor.VendorID);
            return View(contactVendor);
        }

        // GET: ContactVendors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactVendor = await _context.ContactVendors
                .Include(c => c.Contact)
                .Include(c => c.Vendor)
                .FirstOrDefaultAsync(m => m.ContactID == id);
            if (contactVendor == null)
            {
                return NotFound();
            }

            return View(contactVendor);
        }

        // POST: ContactVendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactVendor = await _context.ContactVendors.FindAsync(id);
            _context.ContactVendors.Remove(contactVendor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactVendorExists(int id)
        {
            return _context.ContactVendors.Any(e => e.ContactID == id);
        }
    }
}
