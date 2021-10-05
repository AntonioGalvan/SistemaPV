using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Data.Entities
{
    public class CPurchase:IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Received { get; set; }
        public double Change { get; set; }
        public DateTime DateTime { get; set; }

        public CManager Manager { get; set; }
        public ICollection<CPurchaseDetail> PurchaseDetails { get; set; }
    }
}
