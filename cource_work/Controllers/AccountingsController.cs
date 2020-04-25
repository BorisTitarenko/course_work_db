using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cource_work.Models.Entity;
using cource_work.Services;
using Microsoft.Extensions.Configuration;

namespace cource_work.Controllers
{
    public class AccountingsController : Controller
    {
        private readonly bus_stationContext _context;
        private IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

        public AccountingsController(bus_stationContext context)
        {
            _context = context;
        }

        // GET: Accountings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Accounting.ToListAsync());
        }

        // GET: Accountings/Report/5
        public async Task<IActionResult> Report(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PrepareReport((int)id);
            var accounting = await _context.Accounting
                .FirstOrDefaultAsync(m => m.AccId == id);
            if (accounting == null)
            {
                return NotFound();
            }

            return View(accounting);
        }

        // GET: Accountings/New
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New([Bind("AccId,StartPerion,EndPerion")] Accounting accounting)
        {
            if (ModelState.IsValid)
            {
                Accounting last = _context.Accounting.ToList().Last();
                if (last.EndPerion > accounting.EndPerion) {
                    return View(accounting);
                }
                _context.Add(accounting);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "TransportationCosts");
            }
            return View(accounting);
        }

        public async void PrepareReport(int id) {
            int last = _context.Accounting.ToList().Last().AccId;
            new AccountingCalculator(configuration.GetConnectionString("connectingString"))
                .calculate(id);
        }



        // GET: Accountings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounting = await _context.Accounting.FindAsync(id);
            if (accounting == null)
            {
                return NotFound();
            }
            return View(accounting);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccId,TransportationAmount,SalaryAmount,ServiceAmount,Nds,InsuranceAmount,TicketAmount,StartPerion,EndPerion")] Accounting accounting)
        {
            if (id != accounting.AccId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accounting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountingExists(accounting.AccId))
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
            return View(accounting);
        }

        // GET: Accountings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounting = await _context.Accounting
                .FirstOrDefaultAsync(m => m.AccId == id);
            if (accounting == null)
            {
                return NotFound();
            }

            return View(accounting);
        }

        // POST: Accountings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accounting = await _context.Accounting.FindAsync(id);
            _context.Accounting.Remove(accounting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountingExists(int id)
        {
            return _context.Accounting.Any(e => e.AccId == id);
        }
    }
}
