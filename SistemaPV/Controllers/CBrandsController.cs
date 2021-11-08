using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaPV.Data;
using SistemaPV.Data.Entities;

namespace SistemaPV.Controllers
{
    [Authorize(Roles ="Admin, Manager")]
    public class CBrandsController : Controller
    {
        private readonly DataContext _context;

        public CBrandsController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Brands.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cBrand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cBrand == null)
            {
                return NotFound();
            }

            return View(cBrand);
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CBrand cBrand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cBrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cBrand);
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cBrand = await _context.Brands.FindAsync(id);
            if (cBrand == null)
            {
                return NotFound();
            }
            return View(cBrand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CBrand cBrand)
        {
            if (id != cBrand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cBrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CBrandExists(cBrand.Id))
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
            return View(cBrand);
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cBrand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cBrand == null)
            {
                return NotFound();
            }

            return View(cBrand);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cBrand = await _context.Brands.FindAsync(id);
            _context.Brands.Remove(cBrand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CBrandExists(int id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }
}
