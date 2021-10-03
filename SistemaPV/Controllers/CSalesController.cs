using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaPV.Data;
using SistemaPV.Data.Entities;

namespace SistemaPV.Controllers
{
    public class CSalesController : Controller
    {
        private readonly DataContext _context;

        public CSalesController(DataContext context)
        {
            _context = context;
        }

        // GET: CSales
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Sales.Include(c => c.SaleDetail);
            return View(await dataContext.ToListAsync());
        }

        // GET: CSales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSale = await _context.Sales
                .Include(c => c.SaleDetail)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cSale == null)
            {
                return NotFound();
            }

            return View(cSale);
        }

        // GET: CSales/Create
        public IActionResult Create()
        {
            ViewData["SaleDetailId"] = new SelectList(_context.SaleDetails, "Id", "Id");
            return View();
        }

        // POST: CSales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CSale cSale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cSale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SaleDetailId"] = new SelectList(_context.SaleDetails, "Id", "Id", cSale.SaleDetailId);
            return View(cSale);
        }

        // GET: CSales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSale = await _context.Sales.FindAsync(id);
            if (cSale == null)
            {
                return NotFound();
            }
            ViewData["SaleDetailId"] = new SelectList(_context.SaleDetails, "Id", "Id", cSale.SaleDetailId);
            return View(cSale);
        }

        // POST: CSales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CSale cSale)
        {
            if (id != cSale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cSale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CSaleExists(cSale.Id))
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
            ViewData["SaleDetailId"] = new SelectList(_context.SaleDetails, "Id", "Id", cSale.SaleDetailId);
            return View(cSale);
        }

        // GET: CSales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSale = await _context.Sales
                .Include(c => c.SaleDetail)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cSale == null)
            {
                return NotFound();
            }

            return View(cSale);
        }

        // POST: CSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cSale = await _context.Sales.FindAsync(id);
            _context.Sales.Remove(cSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CSaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
