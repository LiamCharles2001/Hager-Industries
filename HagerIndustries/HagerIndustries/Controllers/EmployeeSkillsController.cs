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
    public class EmployeeSkillsController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public EmployeeSkillsController(HagerIndustriesContext context)
        {
            _context = context;
        }

        // GET: EmployeeSkills
        public async Task<IActionResult> Index()
        {
            var hagerIndustriesContext = _context.EmployeeSkills.Include(e => e.Employee).Include(e => e.Skill);
            return View(await hagerIndustriesContext.ToListAsync());
        }

        // GET: EmployeeSkills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSkill = await _context.EmployeeSkills
                .Include(e => e.Employee)
                .Include(e => e.Skill)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employeeSkill == null)
            {
                return NotFound();
            }

            return View(employeeSkill);
        }

        // GET: EmployeeSkills/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "AddressOne");
            ViewData["SkillID"] = new SelectList(_context.Skills, "ID", "SkillName");
            return View();
        }

        // POST: EmployeeSkills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,SkillID")] EmployeeSkill employeeSkill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeSkill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "AddressOne", employeeSkill.EmployeeID);
            ViewData["SkillID"] = new SelectList(_context.Skills, "ID", "SkillName", employeeSkill.SkillID);
            return View(employeeSkill);
        }

        // GET: EmployeeSkills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSkill = await _context.EmployeeSkills.FindAsync(id);
            if (employeeSkill == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "AddressOne", employeeSkill.EmployeeID);
            ViewData["SkillID"] = new SelectList(_context.Skills, "ID", "SkillName", employeeSkill.SkillID);
            return View(employeeSkill);
        }

        // POST: EmployeeSkills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,SkillID")] EmployeeSkill employeeSkill)
        {
            if (id != employeeSkill.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeSkill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeSkillExists(employeeSkill.EmployeeID))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "AddressOne", employeeSkill.EmployeeID);
            ViewData["SkillID"] = new SelectList(_context.Skills, "ID", "SkillName", employeeSkill.SkillID);
            return View(employeeSkill);
        }

        // GET: EmployeeSkills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSkill = await _context.EmployeeSkills
                .Include(e => e.Employee)
                .Include(e => e.Skill)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employeeSkill == null)
            {
                return NotFound();
            }

            return View(employeeSkill);
        }

        // POST: EmployeeSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeSkill = await _context.EmployeeSkills.FindAsync(id);
            _context.EmployeeSkills.Remove(employeeSkill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeSkillExists(int id)
        {
            return _context.EmployeeSkills.Any(e => e.EmployeeID == id);
        }
    }
}
