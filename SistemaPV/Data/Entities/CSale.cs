using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Data.Entities
{
    public class CSale:IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [MaxLength(20, ErrorMessage = "Debe introducir un máximo de {1} caracteres.")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [Display(Name = "Monto Pagado")]
        public double PaidAmount { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [Display(Name = "Cambio")]
        public double Change { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Fecha y Hora")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [Display(Name = "Id de usuario")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [Display(Name = "Detalle de venta")]
        public int SaleDetailId { get; set; }


        public CSalesman Salesman { get; set; }
        public ICollection<CSaleDetail> SaleDetails { get; set; }
    }
}
