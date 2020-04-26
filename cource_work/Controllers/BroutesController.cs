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
    public class BroutesController : Controller
    {
        private readonly bus_stationContext _context;

        public BroutesController(bus_stationContext context)
        {
            _context = context;
        }

        // GET: Broutes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Broute.ToListAsync());
        }

        // GET: Broutes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var broute = await _context.Broute
                .FirstOrDefaultAsync(m => m.RouteId == id);
            if (broute == null)
            {
                return NotFound();
            }

            return View(broute);
        }

        // GET: Broutes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Broutes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RouteId,StartPoint,EndPoint,RouteLength")] Broute broute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(broute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(broute);
        }

        // GET: Broutes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var broute = await _context.Broute.FindAsync(id);
            if (broute == null)
            {
                return NotFound();
            }
            return View(broute);
        }

        // POST: Broutes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RouteId,StartPoint,EndPoint,RouteLength")] Broute broute)
        {
            if (id != broute.RouteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(broute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrouteExists(broute.RouteId))
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
            return View(broute);
        }

        // GET: Broutes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var broute = await _context.Broute
                .FirstOrDefaultAsync(m => m.RouteId == id);
            if (broute == null)
            {
                return NotFound();
            }

            return View(broute);
        }

        // POST: Broutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var broute = await _context.Broute.FindAsync(id);
            _context.Broute.Remove(broute);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrouteExists(int id)
        {
            return _context.Broute.Any(e => e.RouteId == id);
        }
    }
}
