using SistemaPV.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Models
{
    public class addUserViewModel:CManager
    {
        [Display(Name ="Contraseña")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Pass { get; set; }
    }
}
