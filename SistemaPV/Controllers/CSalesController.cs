using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin, Manager, Salesman")]
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

            var saleDetailTemps = this.datacontext.SaleDetailTemps
                .Include(od => od.Product)
                .Where(od => od.User == user)
                .ToList();

            var saleTemp = new CSaleTemp
            {
                User = user,
                Items = saleDetailTemps,
                Date = null
            };

            return View(saleDetailTemps);
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
        [Authorize(Roles = "Admin, Manager")]
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
        public IActionResult confirmOrder()
        {
            var model = new addSaleViewModel
            {
                PaidAmmount = 0,
                Items = this.combosHelper.GetComboItems()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> confirmOrder(addSaleViewModel model)
        {
            var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            var sale = await this.datacontext.CSaleTemps
                .Include(pdt => pdt.Items)
                .FirstOrDefaultAsync(usr => usr.User == user);

            if (sale == null || sale.Items.Count == 0)
            {
                return NotFound();
            }

            var items = sale.Items.ToList();

            for (int i = 0; i < items.Count; i++)
            {
                this.datacontext.Products.Find(items[i].Product.Id).Quantity -= items[i].Quantity;
            }
            sale.PaidAmount = 1;

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
    }
}

