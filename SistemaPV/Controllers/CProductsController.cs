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
    public class CProductsController : Controller
    {
        private readonly DataContext _context;

        public CProductsController(DataContext context)
        {
            _context = context;
        }

        // GET: CProducts
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Products.Include(c => c.Brand).Include(c => c.Category);
            return View(await dataContext.ToListAsync());
        }

        // GET: CProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cProduct = await _context.Products
                .Include(c => c.Brand)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cProduct == null)
            {
                return NotFound();
            }

            return View(cProduct);
        }

        // GET: CProducts/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: CProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CProduct cProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", cProduct.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", cProduct.CategoryId);
            return View(cProduct);
        }

        // GET: CProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cProduct = await _context.Products.FindAsync(id);
            if (cProduct == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", cProduct.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", cProduct.CategoryId);
            return View(cProduct);
        }

        // POST: CProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CProduct cProduct)
        {
            if (id != cProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CProductExists(cProduct.Id))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", cProduct.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", cProduct.CategoryId);
            return View(cProduct);
        }

        // GET: CProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cProduct = await _context.Products
                .Include(c => c.Brand)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cProduct == null)
            {
                return NotFound();
            }

            return View(cProduct);
        }

        // POST: CProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cProduct = await _context.Products.FindAsync(id);
            _context.Products.Remove(cProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
