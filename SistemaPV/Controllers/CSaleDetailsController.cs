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
    public class CSaleDetailsController : Controller
    {
        private readonly DataContext _context;

        public CSaleDetailsController(DataContext context)
        {
            _context = context;
        }

        // GET: CSaleDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.SaleDetails.ToListAsync());
        }

        // GET: CSaleDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSaleDetail = await _context.SaleDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cSaleDetail == null)
            {
                return NotFound();
            }

            return View(cSaleDetail);
        }

        // GET: CSaleDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CSaleDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CSaleDetail cSaleDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cSaleDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cSaleDetail);
        }

        // GET: CSaleDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSaleDetail = await _context.SaleDetails.FindAsync(id);
            if (cSaleDetail == null)
            {
                return NotFound();
            }
            return View(cSaleDetail);
        }

        // POST: CSaleDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CSaleDetail cSaleDetail)
        {
            if (id != cSaleDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cSaleDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CSaleDetailExists(cSaleDetail.Id))
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
            return View(cSaleDetail);
        }

        // GET: CSaleDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSaleDetail = await _context.SaleDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cSaleDetail == null)
            {
                return NotFound();
            }

            return View(cSaleDetail);
        }

        // POST: CSaleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cSaleDetail = await _context.SaleDetails.FindAsync(id);
            _context.SaleDetails.Remove(cSaleDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CSaleDetailExists(int id)
        {
            return _context.SaleDetails.Any(e => e.Id == id);
        }
    }
}
