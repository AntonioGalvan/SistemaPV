using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Data.Entities
{
    public class CManager
    {
        public int Id { get; set; }

        public string Area { get; set; }

        public CUser CUser { get; set; }
    }
}
