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
    [Authorize(Roles = "Admin")]
    public class CManagersController : Controller
    {
        private readonly DataContext _context;

        public CManagersController(DataContext context)
        {
            _context = context;
        }

        // GET: CManagers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Managers.Include(s => s.User).ToListAsync());
        }

        // GET: CManagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cManager = await _context.Managers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cManager == null)
            {
                return NotFound();
            }

            return View(cManager);
        }

        // GET: CManagers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CManagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CManager cManager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cManager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cManager);
        }

        // GET: CManagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cManager = await _context.Managers.FindAsync(id);
            if (cManager == null)
            {
                return NotFound();
            }
            return View(cManager);
        }

        // POST: CManagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CManager cManager)
        {
            if (id != cManager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cManager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CManagerExists(cManager.Id))
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
            return View(cManager);
        }

        // GET: CManagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cManager = await _context.Managers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cManager == null)
            {
                return NotFound();
            }

            return View(cManager);
        }

        // POST: CManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cManager = await _context.Managers.FindAsync(id);
            _context.Managers.Remove(cManager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CManagerExists(int id)
        {
            return _context.Managers.Any(e => e.Id == id);
        }
    }
}
