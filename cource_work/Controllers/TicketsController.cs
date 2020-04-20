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
        private int ndsPersents = 20;

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



        // GET: Tickets/getRoutePointsFromTrip
        public JsonResult getRoutePointsFromTrip(int id) {


            var rp = _context.JourneyRoutePoint.Include(jrp => jrp.Journey)
                .Include(jpr => jpr.Journey.Trip)
                .Include(jpr => jpr.Rp)
                .Where(jrp => jrp.Journey.Trip.FirstOrDefault(t => t.TripId == id).TripId == id).ToList()
                .Select(r => new
                {
                    rpId = r.RpId,
                    cityName = r.Rp.CityName + " " + r.ArrivalTime,
                    ticketPrice = r.TicketPrice
                });

            return Json(rp.ToList());
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



        //GET: Tickets/getSeatAndPrice
        public JsonResult getSeatRange(int tripId, int rpId) {
            int seat = (int)_context.Trip.First(t => t.TripId == tripId).PassangersCount + 1;
            decimal price = (decimal)_context.JourneyRoutePoint.First(r => r.RpId == rpId).TicketPrice;
            return Json(new {seat, price});
        }




        // GET: Tickets/Create
        public IActionResult Create()
        {
            var trips = _context.Trip.ToList(); 
            var passDic = _context.Passenger.ToDictionary(p => p.PassengerId, p => p.PassengerName);
            ViewData["PassengerId"] = new SelectList(_context.Passenger, "PassengerId", "PassengerName");
            return View();
        }




        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketRoutePointTransactionViewModel trptvm)
        {
            if (ModelState.IsValid){
                CashTransaction cashTransaction = new CashTransaction {
                    PayTime = trptvm.PaimentTime,
                    TotalCash = trptvm.TicketPrice,
                    DcaId = _context.DayCashAmount.FirstOrDefault(dca => dca.WorkDate == DateTime.Now.Date).DcaId,

                };
                _context.Add(cashTransaction);
                await _context.SaveChangesAsync();

                Ticket ticket = new Ticket
                {
                    PassengerId = trptvm.PassengerId,
                    Nds = (trptvm.TicketPrice / 100) * ndsPersents,
                    Seat = trptvm.Seat,
                    CtId = _context.CashTransaction.ToList().Last().CtId,
                    TripId = trptvm.TripId,
                    RpId = trptvm.RPId
                };
                _context.Add(ticket);

                Trip trip = _context.Trip.FirstOrDefault(t => t.TripId == trptvm.TripId);
                trip.PassangersCount += 1;
                _context.Update(trip);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PassengerId"] = new SelectList(_context.Passenger, "PassengerId", "PassengerName", trptvm.PassengerId);
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
