﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cource_work.Models.Entity;
using cource_work.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace cource_work.Controllers
{
    [Authorize(Policy = "AccountingPolicy")]
    public class AccountingsController : Controller
    {
        private readonly buz_stationContext _context;
        private IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

        public AccountingsController(buz_stationContext context)
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
                Accounting last = _context.Accounting.DefaultIfEmpty().ToList()?.Last();
                if (last?.EndPerion > accounting.EndPerion) {
                    return View(accounting);
                }
                _context.Add(accounting);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(accounting);
        }

        public async void PrepareReport(int id) {
            int last = _context.Accounting.ToList().Last().AccId;
            new AccountingCalculator(configuration.GetConnectionString("connectingString"))
                .calculate(id);
        }


        private bool AccountingExists(int id)
        {
            return _context.Accounting.Any(e => e.AccId == id);
        }
    }
}
