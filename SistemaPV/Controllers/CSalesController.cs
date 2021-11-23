using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPV.Data;
using SistemaPV.Data.Entities;
using SistemaPV.Helpers;
using SistemaPV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class CSalesController : Controller
    {
        private readonly DataContext datacontext;
        private readonly IUserHelper userHelper;
        private readonly ICombosHelper combosHelper;


        public CSalesController(DataContext datacontext, IUserHelper userHelper, ICombosHelper combosHelper)
        {
            this.datacontext = datacontext;
            this.userHelper = userHelper;
            this.combosHelper = combosHelper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            return View
                (
                this.datacontext.Sales.Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.User == user)
                );
        }

        public async Task<IActionResult> Create()
        {
            var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            var model = this.datacontext.SaleDetailTemps
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

        [HttpPost]
        public async Task<IActionResult> addProduct(additemViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user == null)
                {
                    return NotFound();
                }
                var product = await this.datacontext.Products.FindAsync(model.ProductId);
                if (product == null)
                {
                    return NotFound();
                }
                var saleDetailTemp = await this.datacontext.SaleDetailTemps
                    .Where(pd => pd.User == user && pd.Product == product).FirstOrDefaultAsync();
                if (saleDetailTemp == null)
                {
                    saleDetailTemp = new CSaleDetailTemp
                    {
                        Product = product,
                        Quantity = model.Quantity,
                        UnitPrice = product.Price,
                        User = user
                    };
                    this.datacontext.SaleDetailTemps.Add(saleDetailTemp);
                }
                else
                {
                    saleDetailTemp.Quantity += model.Quantity;
                    this.datacontext.SaleDetailTemps.Update(saleDetailTemp);
                }
                await this.datacontext.SaveChangesAsync();
                return this.RedirectToAction("Create");
            }

            return this.View(model);
        }

        public async Task<IActionResult> deleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var saleDetailTemp = await this.datacontext.SaleDetailTemps.FindAsync(id);
            if (saleDetailTemp == null)
            {
                return NotFound();
            }
            this.datacontext.SaleDetailTemps.Remove(saleDetailTemp);
            await this.datacontext.SaveChangesAsync();
            return this.RedirectToAction("Create");
        }
        public async Task<IActionResult> increase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var saleDetailTemp = await this.datacontext.SaleDetailTemps.FindAsync(id);
            if (saleDetailTemp == null)
            {
                return NotFound();
            }
            saleDetailTemp.Quantity += 1;
            this.datacontext.SaleDetailTemps.Update(saleDetailTemp);
            await this.datacontext.SaveChangesAsync();
            return this.RedirectToAction("Create");
        }
        public async Task<IActionResult> decrease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var saleDetailTemp = await this.datacontext.SaleDetailTemps.FindAsync(id);
            if (saleDetailTemp == null)
            {
                return NotFound();
            }
            saleDetailTemp.Quantity -= 1;
            if (saleDetailTemp.Quantity >= 1)
            {

                this.datacontext.SaleDetailTemps.Update(saleDetailTemp);
                await this.datacontext.SaveChangesAsync();
            }
            return this.RedirectToAction("Create");
        }
        public async Task<IActionResult> confirmOrder()
        {
            var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            var saleDetailTemps = await this.datacontext.SaleDetailTemps
                .Include(pdt => pdt.Product)
                .Where(pdt => pdt.User == user)
                .ToListAsync();
            if (saleDetailTemps == null || saleDetailTemps.Count == 0)
            {
                return NotFound();
            }
            var details = saleDetailTemps.Select(pdt => new CSaleDetail
            {
                User = pdt.User,
                Product = pdt.Product,
                UnitPrice = pdt.UnitPrice,
                Quantity = pdt.Quantity
            }).ToList();
            var sale = new CSale
            {
                Date = DateTime.UtcNow,
                User = user,
                
                Items = details
            };
            this.datacontext.Sales.Add(sale);
            this.datacontext.SaleDetailTemps.RemoveRange(saleDetailTemps);

            for (int i = 0; i < details.Count; i++)
            {
                this.datacontext.Products.Find(details[i].Product.Id).Quantity -= details[i].Quantity;
            }

            await this.datacontext.SaveChangesAsync();
            return this.RedirectToAction("Edit", new { id = sale.Id });

        }

        public async Task<IActionResult> Edit(int? id)
        {
            var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            var sale = await this.datacontext.Sales
                .FirstOrDefaultAsync(p => p.Id == id);
            if (sale == null)
            {
                return NotFound();
            }
            var model = new CSale
            {
                Id = sale.Id,
                Date = sale.Date,
                PaidAmount = sale.PaidAmount,
                User = user
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CSale model)
        {
            if (ModelState.IsValid)
            {
               
                var sale = new CSale
                {
                    Id = model.Id,
                    Date = model.Date,
                    PaidAmount = model.PaidAmount
                };
                this.datacontext.Update(sale);
                await this.datacontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await this.datacontext.Sales.Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

    }
}

