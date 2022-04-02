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
    public class ContractorsController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public ContractorsController(HagerIndustriesContext context)
        {
            _context = context;
        }

        // GET: Contractors
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Lookups", new { Tab = "ContractorsTab" });
        }

        // GET: Contractors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }

        // GET: Contractors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contractors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ContractName,ContractDescription")] Contractor contractor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(contractor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "ContractorsTab" });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(contractor);
        }

        // GET: Contractors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractors.FindAsync(id);
            if (contractor == null)
            {
                return NotFound();
            }
            return View(contractor);
        }

        // POST: Contractors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var contractorToUpdate = await _context.Contractors.FirstOrDefaultAsync(m => m.ID == id);

            if (contractorToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Contractor>(contractorToUpdate, "",
                d => d.ContractName, d=>d.ContractDescription))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "ContractorsTab" });
                }                
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractorExists(contractorToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }                
            }
            return View(contractorToUpdate);
        }

        // GET: Contractors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }

        // POST: Contractors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var contractor = await _context.Contractors.FindAsync(id);
            //_context.Contractors.Remove(contractor);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            var contractor = await _context.Contractors.FindAsync(id);

            try
            {
                _context.Contractors.Remove(contractor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Lookups", new { Tab = "ContractorssTab" });
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to delete Contractor Type. Remember, you cannot delete a Contractor that any Company has assigned.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(contractor);
        }

        private bool ContractorExists(int id)
        {
            return _context.Contractors.Any(e => e.ID == id);
        }
    }
}
