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
    [Authorize(Roles = "Admin,Supervisor")]
    public class BillingTermsController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public BillingTermsController(HagerIndustriesContext context)
        {
            _context = context;
        }

        // GET: BillingTerms
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Lookups", new { Tab = "BillingTermsTab" });
        }

        // GET: BillingTerms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingTerm = await _context.BillingTerms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (billingTerm == null)
            {
                return NotFound();
            }

            return View(billingTerm);
        }

        // GET: BillingTerms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillingTerms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TermName,TermDetails")] BillingTerm billingTerm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(billingTerm);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains(""))
                {
                    ModelState.AddModelError("", "Unable to save changes. Remember, you cannot have duplicate Billing Terms.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }

            return View(billingTerm);
        }

        // GET: BillingTerms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingTerm = await _context.BillingTerms.FindAsync(id);
            if (billingTerm == null)
            {
                return NotFound();
            }
            return View(billingTerm);
        }

        // POST: BillingTerms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var billingTermsToUpdate = await _context.BillingTerms.SingleOrDefaultAsync(p => p.ID == id);
            if (id != billingTermsToUpdate.ID)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<BillingTerm>(billingTermsToUpdate, "", p => p.TermName, p => p.TermDetails))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillingTermExists(billingTermsToUpdate.ID))
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
                    if (dex.GetBaseException().Message.Contains(""))
                    {
                        ModelState.AddModelError("", "Unable to save changes. Remember, you cannot have duplicate Billing Term Name.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }
                }
            }
            return View(billingTermsToUpdate);
        }

        // GET: BillingTerms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingTerm = await _context.BillingTerms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (billingTerm == null)
            {
                return NotFound();
            }

            return View(billingTerm);
        }

        // POST: BillingTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billingTerm = await _context.BillingTerms.FindAsync(id);
            try
            {
                _context.BillingTerms.Remove(billingTerm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains(""))
                {
                    ModelState.AddModelError("", "Unable to Delete Billing Term. Remember, you cannot delete a Billing Term that has employees assigned.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(billingTerm);

        }

        private bool BillingTermExists(int id)
        {
            return _context.BillingTerms.Any(e => e.ID == id);
        }
    }
}
