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
    [Authorize(Policy = "AccountingPolicy")]
    public class SalariesController : Controller
    {
        private readonly buz_stationContext _context;

        public SalariesController(buz_stationContext context)
        {
            _context = context;
        }

        // GET: Salaries
        public async Task<IActionResult> Index()
        {
            var buz_stationContext = _context.Salary.Include(s => s.Acc).Include(s => s.Employee);
            return View(await buz_stationContext.ToListAsync());
        }

       

        // GET: Salaries/Create
        public IActionResult Create()
        {
            int lastAcc = _context.Accounting.ToList().Last().AccId;
            var items = _context.Salary.Where(t => t.AccId == lastAcc).ToList();
           ViewData["AccountingPeriod"] = new SelectList(from a in _context.Accounting.ToList()
                                                          select new
                                                          {
                                                              Value = a.AccId,
                                                              Text = a.StartPerion.Value.ToShortDateString() + " - " + a.EndPerion.Value.ToShortDateString()
                                                          }, "Value", "Text");

            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeName");
            return View(items);
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( List<Salary> salary)
        {
            if (ModelState.IsValid)
            {
                foreach (var s in salary)
                {
                    _context.Salary.Find(s.SalaryId).EmployeeSalary = s.EmployeeSalary;
                }
                _context.SaveChanges();
                return RedirectToAction("Report", "Accountings", new { id = salary[0].AccId});
            }
            return View(salary);
        }

        // GET: Salaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salary.FindAsync(id);
            if (salary == null)
            {
                return NotFound();
            }
            ViewData["AccId"] = new SelectList(_context.Accounting, "AccId", "AccId", salary.AccId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeName", salary.EmployeeId);
            return View(salary);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalaryId,EmployeeId,AccId,EmployeeSalary")] Salary salary)
        {
            if (id != salary.SalaryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaryExists(salary.SalaryId))
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
            ViewData["AccId"] = new SelectList(_context.Accounting, "AccId", "AccId", salary.AccId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeName", salary.EmployeeId);
            return View(salary);
        }

        // GET: Salaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salary
                .Include(s => s.Acc)
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.SalaryId == id);
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salary = await _context.Salary.FindAsync(id);
            _context.Salary.Remove(salary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaryExists(int id)
        {
            return _context.Salary.Any(e => e.SalaryId == id);
        }
    }
}
