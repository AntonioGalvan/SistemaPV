namespace SistemaPV.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using SistemaPV.Data;
    using SistemaPV.Data.Entities;
    using SistemaPV.Helpers;
    using SistemaPV.Models;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Roles = "Admin, Manager,Salesman")]
    public class CProductsController:Controller
    {
        private readonly DataContext dataContext;
        private readonly ICombosHelper combosHelper;
        private readonly IImageHelper imageHelper;

        public CProductsController(DataContext dataContext, ICombosHelper combosHelper, IImageHelper imageHelper)
        {
            this.dataContext = dataContext;
            this.combosHelper = combosHelper;
            this.imageHelper = imageHelper;
        }

        public async Task<IActionResult>Index()
        {
            return View(await this.dataContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ToListAsync());
        }

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Create()
        {
            var model = new ProductViewModel
            {
                Brands = this.combosHelper.GetComboBrands(),

                Categories = this.combosHelper.GetComboCategories()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                var product = new CProduct
                {
                    Name = model.Name,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Description = model.Description,
                    ImageUrl = (model.ImageFile != null ? await imageHelper.UploadImageAsync(
                        model.ImageFile, model.Name, "products") : string.Empty),
                    Brand = await this.dataContext.Brands.FindAsync(model.BrandId),
                    Category = await this.dataContext.Categories.FindAsync(model.CategoryId)
                };
                this.dataContext.Add(product);
                await this.dataContext.SaveChangesAsync();
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

            var cProduct = await dataContext.Products.Include(b=>b.Brand).Include(c=>c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cProduct == null)
            {
                return NotFound();
            }

            return View(cProduct);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.dataContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Quantity = product.Quantity,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Brand = product.Brand,
                Category = product.Category,
                BrandId = product.Brand.Id,
                CategoryId = product.Category.Id,

                Brands = this.combosHelper.GetComboBrands(), 
                Categories = this.combosHelper.GetComboCategories()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new CProduct
                {
                    Id = model.Id,
                    Name = model.Name,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Description = model.Description,
                    ImageUrl = (model.ImageFile != null ? await imageHelper.UploadImageAsync(
                        model.ImageFile, model.Name, "products") : model.ImageUrl),
                    Brand = await this.dataContext.Brands.FindAsync(model.BrandId),
                    Category = await this.dataContext.Categories.FindAsync(model.CategoryId)
                };
                this.dataContext.Update(product);
                await this.dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await dataContext.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await this.dataContext.Products.FindAsync(id);
            dataContext.Products.Remove(product);
            await dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CProductExists(int id)
        {
            return dataContext.Products.Any(e => e.Id == id);
        }
    }
}
