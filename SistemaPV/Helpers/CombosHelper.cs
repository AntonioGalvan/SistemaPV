namespace SistemaPV.Helpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SistemaPV.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext dataContext;

        public CombosHelper(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetComboBrands()
        {
            var list = this.dataContext.Brands.Select(b => new SelectListItem
            {
                Text = b.Name,
                Value = $"{b.Id}"
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Selecciona una marca",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboCategories()
        {
            var list = this.dataContext.Categories.Select(b => new SelectListItem
            {
                Text = b.Name,
                Value = $"{b.Id}"
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Selecciona una categoría",
                Value = "0"
            });

            return list;

        }

        public IEnumerable<SelectListItem> GetComboProducts()
        {
            var list = this.dataContext.Products.Select(b => new SelectListItem
            {
                Text = b.Name,
                Value = $"{b.Id}"
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Selecciona un producto",
                Value = "0"
            });

            return list;

        }

        public IEnumerable<SelectListItem> GetComboItems()
        {
            var list = this.dataContext.SaleDetailTemps.Select(b => new SelectListItem
            {
                Value = $"{b.Product.Name} {b.Product.Price} {b.Product.Quantity}"

            }).ToList();

            return list;
        }
    }
}
