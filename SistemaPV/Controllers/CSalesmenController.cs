using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPV.Data;
using SistemaPV.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class CSalesmenController : Controller
    {
        private readonly DataContext _context;

        public CSalesmenController(DataContext context)
        {
            _context = context;
        }

        // GET: CSalesmen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Salesmen.Include(s => s.User).ToListAsync());
        }

        // GET: CSalesmen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSalesman = await _context.Salesmen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cSalesman == null)
            {
                return NotFound();
            }

            return View(cSalesman);
        }

        // GET: CSalesmen/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CSalesmen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CSalesman cSalesman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cSalesman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cSalesman);
        }

        // GET: CSalesmen/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSalesman = await _context.Salesmen.FindAsync(id);
            if (cSalesman == null)
            {
                return NotFound();
            }
            return View(cSalesman);
        }

        // POST: CSalesmen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CSalesman cSalesman)
        {
            if (id != cSalesman.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cSalesman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CSalesmanExists(cSalesman.Id))
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
            return View(cSalesman);
        }

        // GET: CSalesmen/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSalesman = await _context.Salesmen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cSalesman == null)
            {
                return NotFound();
            }

            return View(cSalesman);
        }

        // POST: CSalesmen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cSalesman = await _context.Salesmen.FindAsync(id);
            _context.Salesmen.Remove(cSalesman);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CSalesmanExists(int id)
        {
            return _context.Salesmen.Any(e => e.Id == id);
        }
    }
}
