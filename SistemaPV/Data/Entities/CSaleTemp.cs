using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SistemaPV.Data.Entities
{
    public class CSaleTemp:IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Fecha de venta")]
        public DateTime? Date { get; set; }

        public ICollection<CSaleDetailTemp> Items { get; set; }
        public int Quantity { get { return this.Items == null ? 0 : this.Items.Sum(i => i.Quantity); } }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Total")]
        public double Total { get { return this.Items == null ? 0 : this.Items.Sum(i => i.Amount); } }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [Display(Name = "Monto Pagado")]
        public double PaidAmount { get; set; }

        [Display(Name = "Cambio")]
        public double Change { get { return this.Items == null ? 0 : this.PaidAmount - this.Total < 0 ? 0 : this.PaidAmount - this.Total; } }

        public CUser User { get; set; }
    }
}
