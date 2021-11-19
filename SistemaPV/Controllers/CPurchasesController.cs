using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPV.Data;
using SistemaPV.Helpers;
using SistemaPV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class CPurchasesController:Controller
    {
        private readonly DataContext datacontext;
        private readonly IUserHelper userHelper;
        private readonly ICombosHelper combosHelper;


        public CPurchasesController(DataContext datacontext, IUserHelper userHelper, ICombosHelper combosHelper)
        {
            this.datacontext = datacontext;
            this.userHelper = userHelper;
            this.combosHelper = combosHelper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if(user == null)
            {
                return NotFound();
            }
            return View
                (
                this.datacontext.Purchases.Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.Manager.User == user)
                );
        }
        
        public async Task<IActionResult> Create()
        {
            var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            var model = this.datacontext.PurchaseDetailTemps
                .Include(od => od.Product)
                .Where(od => od.User == user);

            return View(model);
        }

        public IActionResult addProduct()
        {
            var model = new additemViewModel
            {
                Quantity = 1,
                Products = this.combosHelper.GetComboProducts()
            };

            return View(model);
        }
    }
}
