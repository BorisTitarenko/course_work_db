using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cource_work.Models.Entity;

namespace cource_work.Controllers
{
    public class TransportationCostsController : Controller
    {
        private readonly bus_stationContext _context;

        public TransportationCostsController(bus_stationContext context)
        {
            _context = context;
        }

        // GET: TransportationCosts
        public async Task<IActionResult> Index()
        {
            var bus_stationContext = _context.TransportationCosts.Include(t => t.Accounting).Include(t => t.Journey)
                .Where(t => t.AccountingId == _context.Accounting.ToList().Last().AccId);
            return View(await bus_stationContext.ToListAsync());
        }

       

        // GET: TransportationCosts/Create
        public IActionResult Create()
        {
            int lastAcc = _context.Accounting.ToList().Last().AccId;
            var items = _context.TransportationCosts.Where(t => t.AccountingId == lastAcc).ToList();
           /*ViewData["AccountingPeriod"] = new SelectList(from a in _context.Accounting.ToList()
                                                          select new
                                                          {
                                                              Value = a.AccId,
                                                              Text = a.StartPerion.Value.ToShortDateString() + " - " + a.EndPerion.Value.ToShortDateString()
                                                          }, "Value", "Text");

            ViewData["JourneyRoute"] = new SelectList(from j in _context.Journey.Include(j => j.Route).ToList()
                                                   select new { 
                                                       Value = j.JourneyId,
                                                       Text = j.Route.StartPoint + " - " + j.Route.EndPoint + " " + j.DeportingTime
                                                   }
                                                   , "Value", "Text");*/
            return View(items);
        }



       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<TransportationCosts> transportationCosts)
        {
            if (ModelState.IsValid)
            {
                foreach(var tc in transportationCosts) {
                    _context.TransportationCosts.Find(tc.CsId).CarrierComCost = tc.CarrierComCost;
                    _context.TransportationCosts.Find(tc.CsId).FuelCosts = tc.FuelCosts;
                    _context.TransportationCosts.Find(tc.CsId).Inssurance = tc.Inssurance;

                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(transportationCosts);
        }

        // GET: TransportationCosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transportationCosts = await _context.TransportationCosts.FindAsync(id);
            if (transportationCosts == null)
            {
                return NotFound();
            }
            ViewData["AccountingId"] = new SelectList(_context.Accounting, "AccId", "AccId", transportationCosts.AccountingId);
            ViewData["JourneyId"] = new SelectList(_context.Journey, "JourneyId", "JourneyId", transportationCosts.JourneyId);
            return View(transportationCosts);
        }

        // POST: TransportationCosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CsId,AccountingId,JourneyId,CarrierComCost,FuelCosts,MechanicCosts")] TransportationCosts transportationCosts)
        {
            if (id != transportationCosts.CsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transportationCosts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransportationCostsExists(transportationCosts.CsId))
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
            ViewData["AccountingId"] = new SelectList(_context.Accounting, "AccId", "AccId", transportationCosts.AccountingId);
            ViewData["JourneyId"] = new SelectList(_context.Journey, "JourneyId", "JourneyId", transportationCosts.JourneyId);
            return View(transportationCosts);
        }

        // GET: TransportationCosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transportationCosts = await _context.TransportationCosts
                .Include(t => t.Accounting)
                .Include(t => t.Journey)
                .FirstOrDefaultAsync(m => m.CsId == id);
            if (transportationCosts == null)
            {
                return NotFound();
            }

            return View(transportationCosts);
        }

        // POST: TransportationCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transportationCosts = await _context.TransportationCosts.FindAsync(id);
            _context.TransportationCosts.Remove(transportationCosts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransportationCostsExists(int id)
        {
            return _context.TransportationCosts.Any(e => e.CsId == id);
        }
    }
}
