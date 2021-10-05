using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Data.Entities
{
    public class CPurchase:IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [MaxLength(30, ErrorMessage = "Debe introducir un máximo de {1} caracteres.")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [Display(Name = "Recibido")]
        public double Received { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [Display(Name = "Cambio")]
        public double Change { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Fecha y Hora")]
        public DateTime DateTime { get; set; }

        public CManager Manager { get; set; }
        public ICollection<CPurchaseDetail> PurchaseDetails { get; set; }
    }
}
