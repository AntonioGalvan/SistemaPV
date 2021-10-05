using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Data.Entities
{
    public class CManager:IEntity
    {
        [Display(Name = "Nombre")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [MaxLength(20, ErrorMessage = "Debe introducir un máximo de {1} caracteres.")]
        [Display(Name = "Área")]
        public string Area { get; set; }

        public CUser CUser { get; set; }
        public ICollection<CPurchase> Purchases { get; set; }
    }
}
