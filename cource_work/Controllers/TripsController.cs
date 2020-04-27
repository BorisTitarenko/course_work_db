using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cource_work.Models.Entity;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace cource_work.Controllers
{
    [Authorize(Policy = "DispatcherPolicy")]
    public class TripsController : Controller
    {
        private readonly buz_stationContext _context;

        public TripsController(buz_stationContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var buz_stationContext = _context.Trip
                .Include(t => t.Journey)
                .Include(t => t.Journey.Bus)
                .Include(t => t.Journey.Route)
                .Where(j => j.DeportingDate.Value >= (DateTime.Today.Date));
            return View(await buz_stationContext.ToListAsync());
        }

        // GET: Trips/Search
        public async Task<IActionResult> Search(string ss = "PLANNED")
        {
            var buz_stationContext = _context.Trip
                .Include(t => t.Journey)
                .Include(t => t.Journey.Bus)
                .Include(t => t.Journey.Route)
                .Where(j => j.DeportingDate.Value.Equals(DateTime.Today.Date))
                .Where(j => j.Journey.Route.StartPoint.ToLower().Contains(ss)
                || j.Journey.Route.EndPoint.ToLower().Contains(ss)
                || j.PassangersCount.ToString().Contains(ss)
                || j.DeportingStat.ToLower().Contains(ss));
            return View("Index", await buz_stationContext.ToListAsync());
        }

        public async Task<IActionResult> Date(DateTime date)
        {
            var buz_stationContext = _context.Trip
                .Include(t => t.Journey)
                .Include(t => t.Journey.Bus)
                .Include(t => t.Journey.Route)
                .Where(j => j.DeportingDate.Value.Equals(date.Date));
            return View("Index", await buz_stationContext.ToListAsync());
        }

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TripId,PassangersCount,JourneyId,DeportingStat,DeportingDate")] Trip trip)
        {
            if (id != trip.TripId){
                return NotFound();
            }

            if (ModelState.IsValid){
                try{
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }catch (DbUpdateConcurrencyException){
                    if (!TripExists(trip.TripId)){
                        return NotFound();
                    }else{
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip
                .Include(t => t.Journey)
                .FirstOrDefaultAsync(m => m.TripId == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trip = await _context.Trip.FindAsync(id);
            _context.Trip.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(int id)
        {
            return _context.Trip.Any(e => e.TripId == id);
        }
    }
}
