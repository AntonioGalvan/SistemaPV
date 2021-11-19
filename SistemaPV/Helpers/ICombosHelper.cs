namespace SistemaPV.Helpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public interface ICombosHelper
    {
        public IEnumerable<SelectListItem> GetComboBrands();
        public IEnumerable<SelectListItem> GetComboCategories();
        public IEnumerable<SelectListItem> GetComboProducts();
    }
}
