using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaPV.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<CCategory> Categories { get; set; }
        public DbSet<CProduct> Products { get; set; }
        public DbSet<CBrand> Brands { get; set; }
        public DbSet<CSale> Sales { get; set; }
        //Contexto de datos
        public DataContext(DbContextOptions<DataContext> options) : base(options)

        {

        }
    }
}
