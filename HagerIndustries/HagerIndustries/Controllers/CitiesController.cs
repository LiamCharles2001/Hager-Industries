using HagerIndustries.Data;
using HagerIndustries.Models;
using HagerIndustries.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Controllers
{
    [Authorize(Roles = "Admin,Supervisor")]
    public class CitiesController : Controller
    {
        private readonly HagerIndustriesContext _context;

        public CitiesController(HagerIndustriesContext context)
        {
            _context = context;
        }
        public JsonResult getCitiesByProvince(int id)
        {
            List<City> cities = new List<City>();
            cities = _context.Cities.Where(c => c.ProvinceID == id).ToList();
            return Json(new SelectList(cities, "ID", "cityName"));

        }
        // GET: Cities
        public IActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var cities = from s in _context.Cities
                            select s;
            switch (sortOrder)
            {
                case "name_desc":
                    cities = cities.OrderByDescending(s => s.cityName);
                    break;
                default:
                    cities = cities.OrderBy(s => s.cityName);
                    break;
            }

            var hagerDbContext = _context.Cities.Include(p => p.Province);
            return RedirectToAction("Index", "Lookups", new { Tab = "CitiesTab" });
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .Include(p => p.Province)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,cityName,ProvinceID")] City city)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(city);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "CitiesTab" });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            PopulateDropDownLists(city);
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities.Include(p => p.Province).ThenInclude(p=>p.Country).FirstOrDefaultAsync(m => m.ID == id); ;
            if (city == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(city);
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var cityToUpdate = await _context.Cities.SingleOrDefaultAsync(p => p.ID == id);

            if (cityToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<City>(cityToUpdate, "",
                p => p.cityName, p => p.ProvinceID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "CitiesTab" });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(cityToUpdate.ID))
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
            PopulateDropDownLists(cityToUpdate);
            return View(cityToUpdate);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .Include(p => p.Province)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            try
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Lookups", new { Tab = "CitiesTab" });
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
            return View(city);
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.ID == id);
        }

       private void PopulateDropDownLists(City city = null)
        {
            //ViewData["ProvinceID"] = new SelectList(_context.Provinces, "ID", "provName", city?.ProvinceID);
            ViewData["CountryID"] = new
                SelectList(_context.Countries
                .OrderBy(a => a.countryName), "ID", "countryName",city?.Province.CountryID);
            ViewData["ProvinceID"] = _context.ProvinceSelectList(city?.ProvinceID);
        }
    }
}
