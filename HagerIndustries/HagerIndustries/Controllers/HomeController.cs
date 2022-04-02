using HagerIndustries.Data;
using HagerIndustries.Models;
using HagerIndustries.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HagerIndustriesContext _context;

        public HomeController(ILogger<HomeController> logger, HagerIndustriesContext context)

        {
            _logger = logger;
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Dashboard dashboard = new Dashboard();
            var announcement = await _context.Announcements.OrderBy(p => p.ExpireDate).ToListAsync();
            var calendar = await _context.Events.ToListAsync();

            dashboard.Events = calendar.ToList();
            dashboard.Announcements = announcement.Where(p => p.ExpireDate >= DateTime.Today).ToList();
            return View(dashboard);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetEvents()
        {
            var events = from e in _context.Events
                         select e;
            return Json(events);
        }

        // Not really nessecary, since I've opted to just pull from
        // the page event info instead of hitting the server
        public async Task<IActionResult> GetEvent(Event ev)
        {
            if (ev.ID == null)
            {
                return NotFound();
            }

            var evnt = await _context.Events
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.ID == ev.ID);

            if (evnt == null)
            {
                return NotFound();
            }

            return Json(evnt);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> SaveEvent(Event e)
        {
            _context.Add(e);

            await _context.SaveChangesAsync();

            return new EmptyResult();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> UpdateEvent(Event ev)
        {
            var eventToUpdate = await _context.Events
                .SingleOrDefaultAsync(e => e.ID == ev.ID);

            if (await TryUpdateModelAsync<Event>(eventToUpdate, "",
                e => e.Title, e => e.Start, e => e.End, e => e.AllDay))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return new EmptyResult();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(eventToUpdate.ID))
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
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Event.ID"))
                    {
                        ModelState.AddModelError("ID", "Unable to save changes. A duplicate Event ID already existing in the database is likely the issue.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }
                }
            }
            
            return new EmptyResult();
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> DeleteEvent(Event ev)
        {
            var eventToDelete = await _context.Events.FindAsync(ev.ID);

            try
            {
                _context.Events.Remove(eventToDelete);
                await _context.SaveChangesAsync();
                return new EmptyResult();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to delete event. Try again, and if the problem persists see your system administrator.");
            }

            return new EmptyResult();
        }

        private bool EventExists(string id)
        {
            return _context.Events.Any(e => e.ID == id);
        }

    }
}
