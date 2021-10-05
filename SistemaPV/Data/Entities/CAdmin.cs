using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Data.Entities
{
    public class CAdmin:IEntity
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        public CUser CUser { get; set; }
    }
}
