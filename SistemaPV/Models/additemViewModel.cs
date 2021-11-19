using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Models
{
    public class additemViewModel
    {
        [Display(Name = "Producto")]
        [Range(1,int.MaxValue, ErrorMessage = "Debe seleccionar un producto")]

        public int ProductId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe ser positivo")]

        public int Quantity { get; set; }

        public IEnumerable<SelectListItem> Products { get; set; }
    }
}
