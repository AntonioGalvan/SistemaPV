using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaPV.Data.Entities
{
    public class CSaleDetail:IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El {0} es requerido.")]
        public CUser User { get; set; }
        [Required(ErrorMessage = "El {0} es requerido.")]
        [Display(Name = "Producto")]
        public CProduct Product { get; set; }
        [Required(ErrorMessage = "El {0} es requerido.")]
        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double UnitPrice { get; set; }
        [Required(ErrorMessage = "El {0} es requerido.")]
        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }

        [Display(Name = "Importe")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double Amount { get { return this.UnitPrice * this.Quantity; } }
    }
}
