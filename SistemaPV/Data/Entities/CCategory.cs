namespace SistemaPV.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class CCategory : IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [MaxLength(20, ErrorMessage = "Debe introducir un máximo de {1} caracteres.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        public ICollection<CProduct> Products { get; set; }
    }
}
