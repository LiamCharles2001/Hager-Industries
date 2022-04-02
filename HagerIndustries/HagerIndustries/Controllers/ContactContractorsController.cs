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
    public class ContactContractorsController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public ContactContractorsController(HagerIndustriesContext context)
        {
            _context = context;
        }

        // GET: ContactContractors
        //public async Task<IActionResult> Index()
        //{
        //    var hagerIndustriesContext = _context.ContactContractors.Include(c => c.Contact).Include(c => c.Contractor);
        //    return View(await hagerIndustriesContext.ToListAsync());
        //}
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Contacts", new { _companyTypeEnum = "Contractor" });
        }
        // GET: ContactContractors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactContractor = await _context.ContactContractors
                .Include(c => c.Contact)
                .Include(c => c.Contractor)
                .FirstOrDefaultAsync(m => m.ContactID == id);
            if (contactContractor == null)
            {
                return NotFound();
            }

            return View(contactContractor);
        }

        // GET: ContactContractors/Create
        public IActionResult Create()
        {
            ViewData["ContactID"] = new SelectList(_context.Contacts, "ID", "Email");
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ID", "ID");
            return View();
        }

        // POST: ContactContractors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractorID,ContactID")] ContactContractor contactContractor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactContractor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactID"] = new SelectList(_context.Contacts, "ID", "Email", contactContractor.ContactID);
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ID", "ID", contactContractor.ContractorID);
            return View(contactContractor);
        }

        // GET: ContactContractors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactContractor = await _context.ContactContractors.FindAsync(id);
            if (contactContractor == null)
            {
                return NotFound();
            }
            ViewData["ContactID"] = new SelectList(_context.Contacts, "ID", "Email", contactContractor.ContactID);
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ID", "ID", contactContractor.ContractorID);
            return View(contactContractor);
        }

        // POST: ContactContractors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractorID,ContactID")] ContactContractor contactContractor)
        {
            if (id != contactContractor.ContactID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactContractor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactContractorExists(contactContractor.ContactID))
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
            ViewData["ContactID"] = new SelectList(_context.Contacts, "ID", "Email", contactContractor.ContactID);
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ID", "ID", contactContractor.ContractorID);
            return View(contactContractor);
        }

        // GET: ContactContractors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactContractor = await _context.ContactContractors
                .Include(c => c.Contact)
                .Include(c => c.Contractor)
                .FirstOrDefaultAsync(m => m.ContactID == id);
            if (contactContractor == null)
            {
                return NotFound();
            }

            return View(contactContractor);
        }

        // POST: ContactContractors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactContractor = await _context.ContactContractors.FindAsync(id);
            _context.ContactContractors.Remove(contactContractor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactContractorExists(int id)
        {
            return _context.ContactContractors.Any(e => e.ContactID == id);
        }
    }
}
