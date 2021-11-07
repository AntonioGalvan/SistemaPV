namespace SistemaPV.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using SistemaPV.Data;
    using SistemaPV.Data.Entities;
    using SistemaPV.Helpers;
    using SistemaPV.Models;
    using System.Linq;
    using System.Threading.Tasks;

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

    }
}
