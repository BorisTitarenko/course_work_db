using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cource_work.Models.Entity;
using Microsoft.AspNetCore.Authorization;

namespace cource_work.Controllers
{
    public class JourneysController : Controller
    {
        private readonly buz_stationContext _context;

        public JourneysController(buz_stationContext context)
        {
            _context = context;
        }


        // GET: Journeys
        [Authorize(Policy = "DispatcherPolicy")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var buz_stationContext = _context.Journey.Include(j => j.Bus).Include(j => j.Route);
            return View(await buz_stationContext.ToListAsync());
        }

        [Authorize(Policy = "DispatcherPolicy")]
        // GET: Journeys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journey = await _context.Journey
                .Include(j => j.Bus)
                .Include(j => j.Route)
                .FirstOrDefaultAsync(m => m.JourneyId == id);
            if (journey == null)
            {
                return NotFound();
            }

            return View(journey);
        }

        
        // GET: Journeys/Create
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["BusId"] = new SelectList(_context.Bus, "BusId", "BusModel");
            ViewData["RouteId"] = new SelectList(_context.Broute, "RouteId", "EndPoint");
            return View();
        }

        // POST: Journeys/Create
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JourneyId,BusId,RouteId,DeportingTime,AffectDistance,CarrierCompanyCosts,StartTime,EndTime")] Journey journey)
        {
            if (ModelState.IsValid)
            {
                _context.Add(journey);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusId"] = new SelectList(_context.Bus, "BusId", "BusModel", journey.BusId);
            ViewData["RouteId"] = new SelectList(_context.Broute, "RouteId", "EndPoint", journey.RouteId);
            return View(journey);
        }

        // GET: Journeys/Edit/5
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journey = await _context.Journey.FindAsync(id);
            if (journey == null)
            {
                return NotFound();
            }
            ViewData["BusId"] = new SelectList(_context.Bus, "BusId", "BusModel", journey.BusId);
            ViewData["RouteId"] = new SelectList(_context.Broute, "RouteId", "EndPoint", journey.RouteId);
            return View(journey);
        }


        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JourneyId,BusId,RouteId,DeportingTime,AffectDistance,CarrierCompanyCosts,StartTime,EndTime")] Journey journey)
        {
            if (id != journey.JourneyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(journey);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JourneyExists(journey.JourneyId))
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
            ViewData["BusId"] = new SelectList(_context.Bus, "BusId", "BusModel", journey.BusId);
            ViewData["RouteId"] = new SelectList(_context.Broute, "RouteId", "EndPoint", journey.RouteId);
            return View(journey);
        }

        // GET: Journeys/Delete/5
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journey = await _context.Journey
                .Include(j => j.Bus)
                .Include(j => j.Route)
                .FirstOrDefaultAsync(m => m.JourneyId == id);
            if (journey == null)
            {
                return NotFound();
            }

            return View(journey);
        }

        // POST: Journeys/Delete/5
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var journey = await _context.Journey.FindAsync(id);
            _context.Journey.Remove(journey);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JourneyExists(int id)
        {
            return _context.Journey.Any(e => e.JourneyId == id);
        }
    }
}
