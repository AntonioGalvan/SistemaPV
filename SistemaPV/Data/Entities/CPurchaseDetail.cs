using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Data.Entities
{
    public class CPurchaseDetail:IEntity
    {
        public int Id { get; set; }
        public double Total { get; set; }

        public CPurchase Purchase { get; set; }
        public CProduct Product { get; set; }
    }
}
