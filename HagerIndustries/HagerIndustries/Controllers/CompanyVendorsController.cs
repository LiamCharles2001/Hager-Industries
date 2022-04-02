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
    public class CompanyVendorsController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public CompanyVendorsController(HagerIndustriesContext context)
        {
            _context = context;
        }

        // GET: CompanyVendors
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Companies", new { _companyTypeEnum = "Vendor" });

        }

        // GET: CompanyVendors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyVendor = await _context.CompanyVendors
                .Include(c => c.Company)
                .Include(c => c.Vendor)
                .FirstOrDefaultAsync(m => m.CompanyID == id);
            if (companyVendor == null)
            {
                return NotFound();
            }

            return View(companyVendor);
        }

        // GET: CompanyVendors/Create
        public IActionResult Create()
        {
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName");
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "ID");
            return View();
        }

        // POST: CompanyVendors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorID,CompanyID")] CompanyVendor companyVendor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyVendor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName", companyVendor.CompanyID);
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "ID", companyVendor.VendorID);
            return View(companyVendor);
        }

        // GET: CompanyVendors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyVendor = await _context.CompanyVendors.FindAsync(id);
            if (companyVendor == null)
            {
                return NotFound();
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName", companyVendor.CompanyID);
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "ID", companyVendor.VendorID);
            return View(companyVendor);
        }

        // POST: CompanyVendors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendorID,CompanyID")] CompanyVendor companyVendor)
        {
            if (id != companyVendor.CompanyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyVendor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyVendorExists(companyVendor.CompanyID))
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
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName", companyVendor.CompanyID);
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "ID", companyVendor.VendorID);
            return View(companyVendor);
        }

        // GET: CompanyVendors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyVendor = await _context.CompanyVendors
                .Include(c => c.Company)
                .Include(c => c.Vendor)
                .FirstOrDefaultAsync(m => m.CompanyID == id);
            if (companyVendor == null)
            {
                return NotFound();
            }

            return View(companyVendor);
        }

        // POST: CompanyVendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyVendor = await _context.CompanyVendors.FindAsync(id);
            _context.CompanyVendors.Remove(companyVendor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyVendorExists(int id)
        {
            return _context.CompanyVendors.Any(e => e.CompanyID == id);
        }
    }
}
