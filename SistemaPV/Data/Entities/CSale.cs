﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Data.Entities
{
    public class CSale
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double PaidAmount { get; set; }
        public double Change { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public int SaleDetailId { get; set; }
    }
}
