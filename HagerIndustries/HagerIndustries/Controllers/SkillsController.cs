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
    public class SkillsController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public SkillsController(HagerIndustriesContext context)
        {
            _context = context;
        }

        // GET: Skills
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Lookups", new { Tab = "SkillsTab" });
        }

        // GET: Skills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills
                .FirstOrDefaultAsync(m => m.ID == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // GET: Skills/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SkillName")] Skill skill)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    _context.Add(skill);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "SkillsTab" });
                }
            }
            catch (DbUpdateException dex)
            {
                {
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Skill.SkillName"))
                    {
                        ModelState.AddModelError("SkillName", "Unable to save changes. Remember, you cannot have duplicate Skill names.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }
                }
            }

            return View(skill);
        }

        // GET: Skills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            return View(skill);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var skillToUpdate = await _context.Skills.SingleOrDefaultAsync(p => p.ID == id);
            
            if (skillToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Skill>(skillToUpdate, "",
                d => d.SkillName))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "SkillsTab" });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillExists(skillToUpdate.ID))
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
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Skill.SkillName"))
                    {
                        ModelState.AddModelError("SkillName", "Unable to save changes. Remember, you cannot have duplicate Skill names.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }
                }
            }
            return View(skillToUpdate);
        }

        // GET: Skills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills
                .FirstOrDefaultAsync(m => m.ID == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            try
            {
                _context.Skills.Remove(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Lookups", new { Tab = "SkillsTab" });
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to delete Skill. Remember, you cannot delete a Skill noted for any Employees.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(skill);
        }

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.ID == id);
        }
    }
}
