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
using Microsoft.AspNetCore.Authorization;

namespace cource_work.Controllers
{
    public class MinMaxSeatAndPrice { 
        public int Min { get; set; }
        public int Max { get; set; }
        public double Price { get; set; }
    }


    [Authorize(Policy = "CashierPolicy")]
    public class TicketsController : Controller
    {
        private readonly buz_stationContext _context;

        public TicketsController(buz_stationContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var result = _context.Ticket
                .Include(t => t.Rp)
                .Include(t => t.Ct)
                .Include(t => t.Trip)
                .Select(t => new TicketViewModel
                {
                    TicketId = t.TicketId,
                    City = t.Rp.CityName,
                    TicketPrice = (decimal)_context.JourneyRoutePoint.First(r => r.RpId == t.RpId).TicketPrice,
                    DepartureDate = (DateTime)t.Trip.DeportingDate,
                    PaimentTime = t.Ct.PayTime

                }).ToListAsync();

            return View(await result);
        }


        //GET: Tickets/Search
        public async Task<IActionResult> Search(string ss)
        {
            var result = _context.Ticket
                .Include(t => t.Rp)
                .Include(t => t.Ct)
                .Include(t => t.Trip)
                .Where(t => t.Passenger.PassengerName.Contains(ss)
                || t.Rp.CityName.Contains(ss)
                || t.Ct.PayTime.ToString().Contains(ss)).
                Select(t => new TicketViewModel {
                    TicketId = t.TicketId,
                    City = t.Rp.CityName,
                    TicketPrice = (decimal)_context.JourneyRoutePoint.First(r => r.RpId == t.RpId).TicketPrice,
                    DepartureDate = (DateTime)t.Trip.DeportingDate,
                    PaimentTime = t.Ct.PayTime

        }).ToListAsync();
                
            return View("Index", result);
        }


        //GET: Tickets/Date
        public async Task<IActionResult> Date(DateTime date)
        {
            var result = _context.Ticket
                .Include(t => t.Rp)
                .Include(t => t.Ct)
                .Include(t => t.Trip)
                .Where(t => t.Trip.DeportingDate == date)
                .Select(t => new TicketViewModel
                {
                    TicketId = t.TicketId,
                    City = t.Rp.CityName,
                    TicketPrice = (decimal)_context.JourneyRoutePoint.First(r => r.RpId == t.RpId).TicketPrice,
                    DepartureDate = (DateTime)t.Trip.DeportingDate,
                    PaimentTime = t.Ct.PayTime
                }).ToListAsync();

            return View("Index", result);
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
                .Include(t => t.Rp)
                .Include(t => t.Trip)
                .Include(t => t.Passenger)
                .Include(t => t.Trip.Journey)
                .Include(t => t.Trip.Journey.Route)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            var ticketView = new TicketViewModel
            {
                TicketId = ticket.TicketId,
                City = ticket.Rp.CityName,
                TicketPrice = (decimal)_context.JourneyRoutePoint.First(r => r.RpId == ticket.RpId).TicketPrice,
                DepartureDate = (DateTime)ticket.Trip.DeportingDate,
                PaimentTime = ticket.Ct.PayTime,
                RouteName = ticket.Trip.Journey.Route.StartPoint + " - " + ticket.Trip.Journey.Route.EndPoint,
                PassengerName = ticket.Passenger.PassengerName
            };
            ticketView.Pdv = ticketView.TicketPrice * (decimal)0.18;

            return View(ticketView);
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

        

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            var transaction = await _context.CashTransaction.FindAsync(ticket.CtId);
            var dca = await _context.DayCashAmount.FindAsync(transaction.DcaId);
            if (transaction.PayTime < dca.LastTransactionTime)
            {
                dca.TotalDayAmount -= transaction.TotalCash;
                _context.Update(dca);
            }
            _context.Ticket.Remove(ticket);
            _context.CashTransaction.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.TicketId == id);
        }
    }
}
