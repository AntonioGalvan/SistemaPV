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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Fecha de orden")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Fecha de entrega")]
        public DateTime DeliveryDate { get; set; }
        public IEnumerable<CPurchaseDetail> Items { get; set; }
        public int Quantity { get { return this.Items == null ? 0 : this.Items.Sum(i=>i.Quantity); } }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Total")]
        public double Total { get { return this.Items == null ? 0 : this.Items.Sum(i => i.Amount); } }
        public CManager Manager { get; set; }
        
    }
}
