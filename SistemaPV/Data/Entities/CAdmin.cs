using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Data.Entities
{
    public class CAdmin
    {
        public int Id { get; set; }

        public CUser CUser { get; set; }
    }
}
