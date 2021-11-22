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
                var purchaseDetailTemp = await this.datacontext.PurchaseDetailTemps
                    .Where(pd => pd.User == user && pd.Product == product).FirstOrDefaultAsync();
                if (purchaseDetailTemp == null)
                {
                    purchaseDetailTemp = new CPurchaseDetailTemp
                    {
                        Product = product,
                        Quantity = model.Quantity,
                        UnitPrice = product.Price,
                        User = user
                    };
                    this.datacontext.PurchaseDetailTemps.Add(purchaseDetailTemp);
                }
                else
                {
                    purchaseDetailTemp.Quantity += model.Quantity;
                    this.datacontext.PurchaseDetailTemps.Update(purchaseDetailTemp);
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
            var purchaseDetailTemp = await this.datacontext.PurchaseDetailTemps.FindAsync(id);
            if (purchaseDetailTemp == null)
            {
                return NotFound();
            }
            this.datacontext.PurchaseDetailTemps.Remove(purchaseDetailTemp);
            await this.datacontext.SaveChangesAsync();
            return this.RedirectToAction("Create");
        }
        public async Task<IActionResult> increase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var purchaseDetailTemp = await this.datacontext.PurchaseDetailTemps.FindAsync(id);
            if (purchaseDetailTemp == null)
            {
                return NotFound();
            }
            purchaseDetailTemp.Quantity += 1;
            this.datacontext.PurchaseDetailTemps.Update(purchaseDetailTemp);
            await this.datacontext.SaveChangesAsync();
            return this.RedirectToAction("Create");
        }
        public async Task<IActionResult> decrease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var purchaseDetailTemp = await this.datacontext.PurchaseDetailTemps.FindAsync(id);
            if (purchaseDetailTemp == null)
            {
                return NotFound();
            }
            purchaseDetailTemp.Quantity -= 1;
            if (purchaseDetailTemp.Quantity >= 1)
            {
                
                this.datacontext.PurchaseDetailTemps.Update(purchaseDetailTemp);
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

            var purchaseDetailTemps = await this.datacontext.PurchaseDetailTemps
                .Include(pdt => pdt.Product)
                .Where(pdt => pdt.User == user)
                .ToListAsync();
            if (purchaseDetailTemps == null || purchaseDetailTemps.Count == 0)
            {
                return NotFound();
            }
            var details = purchaseDetailTemps.Select(pdt => new CPurchaseDetail
            {
                User = pdt.User,
                Product = pdt.Product,
                UnitPrice = pdt.UnitPrice,
                Quantity = pdt.Quantity
            }).ToList();
            var purchase = new CPurchase
            {
                OrderDate = DateTime.UtcNow,
                User = user,
                DeliveryDate = DateTime.UtcNow,
                Items = details
            };
            this.datacontext.Purchases.Add(purchase);
            this.datacontext.PurchaseDetailTemps.RemoveRange(purchaseDetailTemps);

            for (int i = 0; i < details.Count; i++)
            {
                this.datacontext.Products.Find(details[i].Product.Id).Quantity+= details[i].Quantity;
            }

            await this.datacontext.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await this.datacontext.Purchases.Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        //public async Task<IActionResult> gotOrder(additemViewModel model)
        //{
        //    var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    model
        //}
    }
}
