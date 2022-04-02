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
using HagerIndustries.Utilities;

namespace HagerIndustries.Controllers
{
    [Authorize(Roles = "Admin,Supervisor")]
    public class ProvincesController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public ProvincesController(HagerIndustriesContext context)
        {
            _context = context;
        }

        public JsonResult getProvincesByCountry(int id)
        {
            List<Province> provinces = new List<Province>();
            provinces = _context.Provinces.Where(c => c.CountryID == id).ToList();
            //cities.Insert(0, new Province { provName = "--Select Province--", ID = 0 });
            return Json( new SelectList(provinces, "ID", "provName" ));

        }
        // GET: Provinces
        public IActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var provinces = from s in _context.Provinces
                            select s;
            switch (sortOrder)
            {
                case "name_desc":
                    provinces = provinces.OrderByDescending(s => s.provName);
                    break;
                default:
                    provinces = provinces.OrderBy(s => s.provName);
                    break;
            }

            var hagerDbContext = _context.Provinces.Include(p => p.Country);
            return RedirectToAction("Index", "Lookups", new { Tab = "ProvincesTab" });
        }

        // GET: Provinces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _context.Provinces
                .Include(p => p.Country)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        // GET: Provinces/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Provinces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,provName,CountryID")] Province province)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(province);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "ProvincesTab" });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            PopulateDropDownLists(province);
            return View(province);
        }

        // GET: Provinces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _context.Provinces.FindAsync(id);
            if (province == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(province);
            return View(province);
        }

        // POST: Provinces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id/*, [Bind("ID,provName,CountryID")] Province province*/)
        {
            var provinceToUpdate = await _context.Provinces.SingleOrDefaultAsync(p=>p.ID == id);

            if (provinceToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Province>(provinceToUpdate, "",
                p=>p.provName, p=>p.CountryID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "ProvincesTab" });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvinceExists(provinceToUpdate.ID))
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
            PopulateDropDownLists(provinceToUpdate);
            return View(provinceToUpdate);
        }

        // GET: Provinces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _context.Provinces
                .Include(p => p.Country)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        // POST: Provinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var province = await _context.Provinces.FindAsync(id);
            try
            {
                _context.Provinces.Remove(province);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Lookups", new { Tab = "ProvincesTab" });
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to Delete Province. Remember, you cannot delete a Province that has employees assigned.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(province);
        }

        private bool ProvinceExists(int id)
        {
            return _context.Provinces.Any(e => e.ID == id);
        }

        private void PopulateDropDownLists(Province province = null)
        {
            ViewData["CountryID"] = _context.CountrySelectList(province?.CountryID);
        }

        //@Html.ActionLink("Provinces", "Index", new { sortOrder = ViewBag.NameSortParm })
    }
}
