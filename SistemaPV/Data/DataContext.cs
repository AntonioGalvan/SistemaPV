using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaPV.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Data
{
    public class DataContext : IdentityDbContext<CUser>
    {        
        public DbSet<CAdmin> Admins { get; set; }
        public DbSet<CBrand> Brands { get; set; }
        public DbSet<CCategory> Categories { get; set; }
        public DbSet<CManager> Managers { get; set; }
        public DbSet<CProduct> Products { get; set; }
        public DbSet<CPurchase> Purchases { get; set; }
        public DbSet<CPurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<CPurchaseDetailTemp> PurchaseDetailTemps { get; set; }
        public DbSet<CSale> Sales { get; set; }
        public DbSet<CSaleDetail> SaleDetails { get; set; }
        public DbSet<CSaleDetailTemp> SaleDetailTemps { get; set; }
        public DbSet<CSalesman> Salesmen { get; set; }
        public DbSet<CSaleTemp> CSaleTemps { get; set; }



        //Contexto de datos
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


    }
}
