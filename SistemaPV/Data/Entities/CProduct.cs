namespace SistemaPV.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    public class CProduct:IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [MaxLength(20, ErrorMessage = "Debe introducir un máximo de {1} caracteres.")]
        [Display(Name = "Producto")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [Display(Name = "Precio")]
        public double Price { get; set; }

        [Display(Name = "Imagen")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [Display(Name = "Id Usuario")]
        public int UserId { get; set; }

        //Objeto-Propiedad
        [Display(Name = "Marca")]
        public CBrand Brand { get; set; }
        [Display(Name = "Categoría")]
        public CCategory Category { get; set; }
        public ICollection<CSaleDetail> SaleDetails { get; set; }
        public ICollection<CPurchaseDetail> PurchaseDetails { get; set; }
    }
}
