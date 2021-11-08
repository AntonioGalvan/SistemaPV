namespace SistemaPV.Models
{
    using System.ComponentModel.DataAnnotations;
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        [Display(Name ="Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }
    }
}
