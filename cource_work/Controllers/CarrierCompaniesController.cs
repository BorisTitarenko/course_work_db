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
    public class CarrierCompaniesController : Controller
    {
        private readonly bus_stationContext _context;

        public CarrierCompaniesController(bus_stationContext context)
        {
            _context = context;
        }

        // GET: CarrierCompanies
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarrierCompany.ToListAsync());
        }

        // GET: CarrierCompanies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrierCompany = await _context.CarrierCompany
                .FirstOrDefaultAsync(m => m.CcId == id);
            if (carrierCompany == null)
            {
                return NotFound();
            }

            return View(carrierCompany);
        }

        // GET: CarrierCompanies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarrierCompanies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CcId,CcName,OfficeAdress,CcOwner,CcPhone")] CarrierCompany carrierCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrierCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carrierCompany);
        }

        // GET: CarrierCompanies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrierCompany = await _context.CarrierCompany.FindAsync(id);
            if (carrierCompany == null)
            {
                return NotFound();
            }
            return View(carrierCompany);
        }

        // POST: CarrierCompanies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CcId,CcName,OfficeAdress,CcOwner,CcPhone")] CarrierCompany carrierCompany)
        {
            if (id != carrierCompany.CcId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrierCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarrierCompanyExists(carrierCompany.CcId))
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
            return View(carrierCompany);
        }

        // GET: CarrierCompanies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrierCompany = await _context.CarrierCompany
                .FirstOrDefaultAsync(m => m.CcId == id);
            if (carrierCompany == null)
            {
                return NotFound();
            }

            return View(carrierCompany);
        }

        // POST: CarrierCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrierCompany = await _context.CarrierCompany.FindAsync(id);
            _context.CarrierCompany.Remove(carrierCompany);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarrierCompanyExists(int id)
        {
            return _context.CarrierCompany.Any(e => e.CcId == id);
        }
    }
}
