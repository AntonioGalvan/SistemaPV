using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Data.Entities
{
    public class CSalesman:IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        public CUser User { get; set; }
        public ICollection<CSale> Sales { get; set; }

    }
}
