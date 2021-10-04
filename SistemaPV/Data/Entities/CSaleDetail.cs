using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaPV.Data.Entities
{
    public class CSaleDetail:IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Total")]
        public double Total { get; set; }

        public ICollection<CProduct> Products { get; set; }
        public CSale Sales { get; set; }
    }
}
