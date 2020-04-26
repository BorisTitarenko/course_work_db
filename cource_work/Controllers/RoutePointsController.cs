using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cource_work.Models.Entity;
using cource_work.ViewModels;

namespace cource_work.Controllers
{
    public class RoutePointsController : Controller
    {
        private readonly bus_stationContext _context;

        public RoutePointsController(bus_stationContext context)
        {
            _context = context;
        }

        // GET: RoutePoints
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoutePoint.ToListAsync());
        }

        // GET: RoutePoints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routePoint = await _context.RoutePoint
                .FirstOrDefaultAsync(m => m.RpId == id);
            if (routePoint == null)
            {
                return NotFound();
            }

            return View(routePoint);
        }

        // GET: RoutePoints/Create
        public IActionResult Create()
        {
            ViewData["Routes"] = new SelectList((from r in _context.Broute.ToList()
                                                select new
                                                {
                                                    Value = r.RouteId,
                                                    Text = r.StartPoint + " " + r.EndPoint
                                                }), "Value", "Text");
            return View();
        }



        public JsonResult getJourneys(int id)
        {
            var journeys = _context.Broute.Include(r => r.Journey).FirstOrDefault(r => r.RouteId == id).Journey;
            var result = from j in journeys
                                select new
                                {
                                    Value = j.JourneyId,
                                    Text = j.JourneyId + " " + j.DeportingTime
                                };
            return Json(result);
        }



        // POST: RoutePoints/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoutePointViewModel routePoint)
        {
            if (ModelState.IsValid)
            {
                RoutePoint rp = new RoutePoint
                {
                    CityName = routePoint.CityName,
                    StopName = routePoint.StopName
                };

                _context.Add(rp);
                await _context.SaveChangesAsync();

                int lastRpId = _context.RoutePoint.ToList().Last().RpId;

                RouteRoutePoint rrp = new RouteRoutePoint
                {
                    RouteId = routePoint.RouteId,
                    RpId = lastRpId
                };
                JourneyRoutePoint jrp = new JourneyRoutePoint
                {
                    RpId = lastRpId,
                    JourneyId = routePoint.JourneyId,
                    TicketPrice = routePoint.TicketPrice,
                    ArrivalTime = routePoint.ArrivalTime,
                    Pdv = routePoint.TicketPrice * (decimal)0.18
                };
                _context.Add(rrp);
                _context.Add(jrp);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            return View(routePoint);
        }



        // GET: RoutePoints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routePoint = await _context.RoutePoint.FindAsync(id);
            if (routePoint == null)
            {
                return NotFound();
            }
            return View(routePoint);
        }

        // POST: RoutePoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RpId,CityName,StopName")] RoutePoint routePoint)
        {
            if (id != routePoint.RpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routePoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoutePointExists(routePoint.RpId))
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
            return View(routePoint);
        }

        // GET: RoutePoints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routePoint = await _context.RoutePoint
                .FirstOrDefaultAsync(m => m.RpId == id);
            if (routePoint == null)
            {
                return NotFound();
            }

            return View(routePoint);
        }

        // POST: RoutePoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var routePoint = await _context.RoutePoint.FindAsync(id);
            _context.RoutePoint.Remove(routePoint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoutePointExists(int id)
        {
            return _context.RoutePoint.Any(e => e.RpId == id);
        }
    }
}
