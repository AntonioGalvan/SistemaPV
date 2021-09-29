using System.Collections.Generic;

namespace SistemaPV.Data.Entities
{
    public class CSaleDetail:IEntity
    {
        public int Id { get; set; }
        public double Total { get; set; }

        public ICollection<CProduct> Products { get; set; }
        public CSale Sales { get; set; }
    }
}
