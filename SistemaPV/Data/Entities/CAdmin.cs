namespace SistemaPV.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class CAdmin : IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        public CUser User { get; set; }
        public ICollection<CSale> Sales { get; set; }

    }
}
