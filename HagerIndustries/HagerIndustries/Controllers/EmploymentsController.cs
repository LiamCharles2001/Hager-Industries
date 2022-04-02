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
    [Authorize(Roles = "Employee,Admin,Supervisor")]
    public class EmploymentsController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public EmploymentsController(HagerIndustriesContext context)
        {
            _context = context;
        }

        // GET: Employments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employments.ToListAsync());
        }

        // GET: Employments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employment = await _context.Employments
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employment == null)
            {
                return NotFound();
            }

            return View(employment);
        }

        // GET: Employments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EmplType")] Employment employment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(employment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }            
            return View(employment);
        }

        // GET: Employments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employment = await _context.Employments.FindAsync(id);
            if (employment == null)
            {
                return NotFound();
            }
            return View(employment);
        }

        // POST: Employments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id/*, [Bind("ID,EmplType")] Employment employment*/)
        {
            var employmentToUpdate = await _context.Employments.SingleOrDefaultAsync(p=>p.ID == id);

            if (employmentToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Employment>(employmentToUpdate, "",
                p=>p.EmplType))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmploymentExists(employmentToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch(DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(employmentToUpdate);            
        }

        // GET: Employments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employment = await _context.Employments
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employment == null)
            {
                return NotFound();
            }

            return View(employment);
        }

        // POST: Employments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employment = await _context.Employments.FindAsync(id);
            try
            {
                _context.Employments.Remove(employment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if ((dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed")))
                {
                    ModelState.AddModelError("", "Unable to Delete Employment. Remember, you cannot delete an Employment that has Employees assigned.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(employment);
        }

        private bool EmploymentExists(int id)
        {
            return _context.Employments.Any(e => e.ID == id);
        }
    }
}
