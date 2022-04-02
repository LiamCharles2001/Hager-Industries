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
    public class CompanyContractorsController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public CompanyContractorsController(HagerIndustriesContext context)
        {
            _context = context;
        }

        // GET: CompanyContractors
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Companies", new { _companyTypeEnum = "Contractor" });
        }

        // GET: CompanyContractors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyContractor = await _context.CompanyContractors
                .Include(c => c.Company)
                .Include(c => c.Contractor)
                .FirstOrDefaultAsync(m => m.CompanyID == id);
            if (companyContractor == null)
            {
                return NotFound();
            }

            return View(companyContractor);
        }

        // GET: CompanyContractors/Create
        public IActionResult Create()
        {
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName");
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ID", "ID");
            return View();
        }

        // POST: CompanyContractors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractorID,CompanyID")] CompanyContractor companyContractor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyContractor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName", companyContractor.CompanyID);
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ID", "ID", companyContractor.ContractorID);
            return View(companyContractor);
        }

        // GET: CompanyContractors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyContractor = await _context.CompanyContractors.FindAsync(id);
            if (companyContractor == null)
            {
                return NotFound();
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName", companyContractor.CompanyID);
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ID", "ID", companyContractor.ContractorID);
            return View(companyContractor);
        }

        // POST: CompanyContractors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractorID,CompanyID")] CompanyContractor companyContractor)
        {
            if (id != companyContractor.CompanyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyContractor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyContractorExists(companyContractor.CompanyID))
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
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "CompName", companyContractor.CompanyID);
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ID", "ID", companyContractor.ContractorID);
            return View(companyContractor);
        }

        // GET: CompanyContractors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyContractor = await _context.CompanyContractors
                .Include(c => c.Company)
                .Include(c => c.Contractor)
                .FirstOrDefaultAsync(m => m.CompanyID == id);
            if (companyContractor == null)
            {
                return NotFound();
            }

            return View(companyContractor);
        }

        // POST: CompanyContractors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyContractor = await _context.CompanyContractors.FindAsync(id);
            _context.CompanyContractors.Remove(companyContractor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyContractorExists(int id)
        {
            return _context.CompanyContractors.Any(e => e.CompanyID == id);
        }
    }
}
