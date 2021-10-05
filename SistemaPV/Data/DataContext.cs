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
        public DbSet<CCategory> Categories { get; set; }
        public DbSet<CProduct> Products { get; set; }
        public DbSet<CBrand> Brands { get; set; }
        public DbSet<CSale> Sales { get; set; }
        public DbSet<CSaleDetail> SaleDetails { get; set; }

        //Contexto de datos
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //Contexto de datos
        public DbSet<SistemaPV.Data.Entities.CSalesman> CSalesman { get; set; }

        //Contexto de datos
        public DbSet<SistemaPV.Data.Entities.CPurchaseDetail> CPurchaseDetail { get; set; }

        //Contexto de datos
        public DbSet<SistemaPV.Data.Entities.CPurchase> CPurchase { get; set; }

        //Contexto de datos
        public DbSet<SistemaPV.Data.Entities.CManager> CManager { get; set; }
    }
}
