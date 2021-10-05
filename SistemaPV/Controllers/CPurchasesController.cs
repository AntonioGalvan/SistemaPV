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
    public class CPurchasesController : Controller
    {
        private readonly DataContext _context;

        public CPurchasesController(DataContext context)
        {
            _context = context;
        }

        // GET: CPurchases
        public async Task<IActionResult> Index()
        {
            return View(await _context.CPurchase.ToListAsync());
        }

        // GET: CPurchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPurchase = await _context.CPurchase
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cPurchase == null)
            {
                return NotFound();
            }

            return View(cPurchase);
        }

        // GET: CPurchases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CPurchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CPurchase cPurchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cPurchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cPurchase);
        }

        // GET: CPurchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPurchase = await _context.CPurchase.FindAsync(id);
            if (cPurchase == null)
            {
                return NotFound();
            }
            return View(cPurchase);
        }

        // POST: CPurchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CPurchase cPurchase)
        {
            if (id != cPurchase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cPurchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CPurchaseExists(cPurchase.Id))
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
            return View(cPurchase);
        }

        // GET: CPurchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPurchase = await _context.CPurchase
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cPurchase == null)
            {
                return NotFound();
            }

            return View(cPurchase);
        }

        // POST: CPurchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cPurchase = await _context.CPurchase.FindAsync(id);
            _context.CPurchase.Remove(cPurchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CPurchaseExists(int id)
        {
            return _context.CPurchase.Any(e => e.Id == id);
        }
    }
}
