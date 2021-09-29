﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Data.Entities
{
    public class CProduct:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }

        public CBrand Brand { get; set; }
        public CUser User { get; set; }
        public CCategory Category { get; set; }
        public ICollection<CSaleDetail> SaleDetails { get; set; }
    }
}
