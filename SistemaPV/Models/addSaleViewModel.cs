using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaPV.Models
{
    public class addSaleViewModel
    {

        [Range(1, int.MaxValue, ErrorMessage = "No alcanza")]
        [Display(Name = "Monto a pagar")]
        public double PaidAmmount { get; set; }

        public IEnumerable<SelectListItem> Items { get; set; }
    }
}
