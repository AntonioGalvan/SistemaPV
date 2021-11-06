namespace SistemaPV.Models
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SistemaPV.Data.Entities;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProductViewModel:CProduct
    {
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Marca")]
        public int BrandId { get; set; }
        public IEnumerable<SelectListItem> Brands { get; set; }

        [Display(Name = "Categoría")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
