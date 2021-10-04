using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Data.Entities
{
    public class CSalesman:IEntity
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public CUser User { get; set; }
    }
}
