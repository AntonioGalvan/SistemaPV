﻿using System;
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
    public class CCategoriesController : Controller
    {
        private readonly DataContext _context;

        public CCategoriesController(DataContext context)
        {
            _context = context;
        }

        // GET: CCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: CCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cCategory = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cCategory == null)
            {
                return NotFound();
            }

            return View(cCategory);
        }

        // GET: CCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CCategory cCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cCategory);
        }

        // GET: CCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cCategory = await _context.Categories.FindAsync(id);
            if (cCategory == null)
            {
                return NotFound();
            }
            return View(cCategory);
        }

        // POST: CCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CCategory cCategory)
        {
            if (id != cCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CCategoryExists(cCategory.Id))
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
            return View(cCategory);
        }

        // GET: CCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cCategory = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cCategory == null)
            {
                return NotFound();
            }

            return View(cCategory);
        }

        // POST: CCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cCategory = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(cCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CCategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}