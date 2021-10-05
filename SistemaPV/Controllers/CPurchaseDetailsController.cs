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
    public class CPurchaseDetailsController : Controller
    {
        private readonly DataContext _context;

        public CPurchaseDetailsController(DataContext context)
        {
            _context = context;
        }

        // GET: CPurchaseDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.CPurchaseDetail.ToListAsync());
        }

        // GET: CPurchaseDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPurchaseDetail = await _context.CPurchaseDetail
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cPurchaseDetail == null)
            {
                return NotFound();
            }

            return View(cPurchaseDetail);
        }

        // GET: CPurchaseDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CPurchaseDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CPurchaseDetail cPurchaseDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cPurchaseDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cPurchaseDetail);
        }

        // GET: CPurchaseDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPurchaseDetail = await _context.CPurchaseDetail.FindAsync(id);
            if (cPurchaseDetail == null)
            {
                return NotFound();
            }
            return View(cPurchaseDetail);
        }

        // POST: CPurchaseDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CPurchaseDetail cPurchaseDetail)
        {
            if (id != cPurchaseDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cPurchaseDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CPurchaseDetailExists(cPurchaseDetail.Id))
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
            return View(cPurchaseDetail);
        }

        // GET: CPurchaseDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPurchaseDetail = await _context.CPurchaseDetail
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cPurchaseDetail == null)
            {
                return NotFound();
            }

            return View(cPurchaseDetail);
        }

        // POST: CPurchaseDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cPurchaseDetail = await _context.CPurchaseDetail.FindAsync(id);
            _context.CPurchaseDetail.Remove(cPurchaseDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CPurchaseDetailExists(int id)
        {
            return _context.CPurchaseDetail.Any(e => e.Id == id);
        }
    }
}
