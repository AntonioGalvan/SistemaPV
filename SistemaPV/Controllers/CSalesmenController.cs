using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPV.Data;
using SistemaPV.Data.Entities;
using SistemaPV.Helpers;
using SistemaPV.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class CSalesmenController : Controller
    {
        private readonly DataContext dataContext;
        private readonly IUserHelper userHelper;

        public CSalesmenController(DataContext context,IUserHelper userHelper)
        {
            dataContext = context;
            this.userHelper = userHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await dataContext.Salesmen.Include(s => s.User).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSalesman = await dataContext.Salesmen.Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cSalesman == null)
            {
                return NotFound();
            }

            return View(cSalesman);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(addUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userHelper.GetUserByIdAsync(model.User.Id);
                if (user == null)
                {
                    user = new CUser
                    {
                        FirstName = model.User.FirstName,
                        LastName = model.User.LastName,
                        Email = model.User.Email,
                        UserName = model.User.Email,
                        Area = model.User.Area
                    };
                }
                var result = await userHelper.AddUserAsync(user, model.Pass);
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("No se ha podido añadir el usuario");
                }
                await userHelper.AddUserToRoleAsync(user, "Salesman");
                var salesman = new CSalesman
                {
                    Id = model.Id,
                    User = await dataContext.Users.FindAsync(user.Id)
                };
                dataContext.Add(salesman);
                await dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSalesman = await dataContext.Salesmen.FindAsync(id);
            if (cSalesman == null)
            {
                return NotFound();
            }
            return View(cSalesman);
        }

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
                    dataContext.Update(cSalesman);
                    await dataContext.SaveChangesAsync();
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

        private bool CSalesmanExists(int id)
        {
            return dataContext.Salesmen.Any(e => e.Id == id);
        }
    }
}
