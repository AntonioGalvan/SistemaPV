﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Data.Entities
{
    public class CUser : IdentityUser
    {
        [Required(ErrorMessage = "{0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName { get { return FirstName + " " + LastName; } }


        public string User { get; set; }

        public string Job { get; set; }

        public CSalesman Salesman { get; set; }
        public CManager Manager { get; set; }
        public CAdmin Admin { get; set; }

    }
}
