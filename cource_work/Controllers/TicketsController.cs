using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cource_work.Models.Entity;
using Microsoft.CodeAnalysis;
using cource_work.ViewModels;

namespace cource_work.Controllers
{
    public class MinMaxSeatAndPrice { 
        public int Min { get; set; }
        public int Max { get; set; }
        public double Price { get; set; }
    }
    public class TicketsController : Controller
    {
        private readonly bus_stationContext _context;

        public TicketsController(bus_stationContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var bus_stationContext = _context.Ticket.Include(t => t.Ct).Include(t => t.Passenger).Include(t => t.Trip);
            return View(await bus_stationContext.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Ct)
                .Include(t => t.Passenger)
                .Include(t => t.Trip)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }



        // GET: Tickets/getRoutePointsFromRoute
        public JsonResult getRoutePointsFromRoute(int id) {
            var routePoints = from trips in _context.Trip.Where(t => t.TripId == id).ToList()
                              join j in _context.Journey.ToList() on trips.JourneyId equals j.JourneyId into table1
                              from jt in table1.ToList()
                              join r in _context.Broute.ToList() on jt.RouteId equals r.RouteId into table2
                              from jtr in table2.ToList()
                              join rp in _context.RoutePoint.ToList() on jtr.RouteId equals rp.RouteId
                              select
                              new {
                                  rpId = rp.RpId,
                                  cityName = rp.CityName
                              };
            return Json(routePoints);
        }




        // GET: Tickets/getTripsOnDate
        public JsonResult getTripsOnDate(DateTime date) {
            var trips = _context.Trip.Include(t => t.Journey).Include(t => t.Journey.Route)
                .Where(t => t.DeportingDate == date).ToList();

            var tripList = from p in trips
                select new{
                    Value = p.TripId,
                    Text = p.Journey.Route.StartPoint + " - " + p.Journey.Route.EndPoint + " " + p.Journey.DeportingTime,
                };
            return Json(tripList);

        }



        //GET: Tickets/getSeatRange
        public JsonResult getSeatRange(int tripId, int rpId) {
            var trip = _context.Trip.Include(t => t.Journey)
                .Include(t => t.Journey.Bus).FirstOrDefault(t => t.TripId == tripId);
            var routePoint = _context.RoutePoint.FirstOrDefault(t => t.RpId == rpId);

            if (trip.PassangersCount >= trip.Journey.Bus.SitingNumber) {
                return null;
            }
            MinMaxSeatAndPrice minMax = new MinMaxSeatAndPrice { Min = (int)trip.PassangersCount + 1, 
                Max = (int)trip.Journey.Bus.SitingNumber,
                //Price = (double)routePoint.};
            return Json(minMax);
        }




        // GET: Tickets/Create
        public IActionResult Create()
        {
            var trips = _context.Trip.ToList(); 
            var passDic = _context.Passenger.ToDictionary(p => p.PassengerId, p => p.PassengerName);
            ViewData["PassengerId"] = new SelectList(passDic, "PassengerId", "PassengerName");
            return View();
        }




        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketRoutePointTransactionViewModel trptvm)
        {
            /*if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CtId"] = new SelectList(_context.CashTransaction, "CtId", "Currency", ticket.CtId);
            ViewData["PassengerId"] = new SelectList(_context.Passenger, "PassengerId", "PassengerName", ticket.PassengerId);
            ViewData["TripId"] = new SelectList(_context.Trip, "TripId", "DeportingStat", ticket.TripId);*/
            return View();
        }



        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            //ViewData["CtId"] = new SelectList(_context.CashTransaction, "CtId", "Currency", ticket.CtId);
            ViewData["PassengerId"] = new SelectList(_context.Passenger, "PassengerId", "PassengerName", ticket.PassengerId);
            ViewData["TripId"] = new SelectList(_context.Trip, "TripId", "DeportingStat", ticket.TripId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,Nds,Seat,PassengerId,CtId,TripId")] Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TicketId))
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
            //ViewData["CtId"] = new SelectList(_context.CashTransaction, "CtId", "Currency", ticket.CtId);
            ViewData["PassengerId"] = new SelectList(_context.Passenger, "PassengerId", "PassengerName", ticket.PassengerId);
            ViewData["TripId"] = new SelectList(_context.Trip, "TripId", "DeportingStat", ticket.TripId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Ct)
                .Include(t => t.Passenger)
                .Include(t => t.Trip)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.TicketId == id);
        }
    }
}
