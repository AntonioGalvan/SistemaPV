using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPV.Data;
using SistemaPV.Data.Entities;
using SistemaPV.Helpers;
using SistemaPV.Models;

namespace SistemaPV.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CManagersController : Controller
    {
        private readonly DataContext dataContext;
        private readonly IUserHelper userHelper;

        public CManagersController(DataContext context, IUserHelper userHelper)
        {
            this.userHelper = userHelper; 
            dataContext = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await dataContext.Managers.Include(s => s.User).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cManager = await dataContext.Managers
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cManager == null)
            {
                return NotFound();
            }

            return View(cManager);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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
                        Area=model.User.Area
                    };
                }
                var result = await userHelper.AddUserAsync(user, model.Pass);
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("No se ha podido añadir el usuario");
                }
                await userHelper.AddUserToRoleAsync(user, "Manager");
                var manager = new CManager
                {
                    Id = model.Id,
                    User = await dataContext.Users.FindAsync(user.Id)
                };
                dataContext.Add(manager);
                await dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cManager = await dataContext.Managers
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id); ;
            if (cManager == null)
            {
                return NotFound();
            }
            return View(cManager);
        }

        /*
        [HttpPost]
        public async Task<IActionResult> Edit(addUserViewModel manager)
        {

            if (ModelState.IsValid)
            {
                var user = await this.dataContext.Users.FindAsync(manager.User.Id);

                user.Email = manager.User.Email;
                user.Area = manager.User.Area;
                try
                {
                    dataContext.Update(manager);
                    await dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CManagerExists(manager.Id))
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
            return View(manager);
        }
        */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CManager manager)
        {
            if (id != manager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dataContext.Update(manager);
                    await dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CManagerExists(manager.Id))
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
            return View(manager);
        }

        private bool CManagerExists(int id)
        {
            return dataContext.Managers.Any(e => e.Id == id);
        }
    }
}
