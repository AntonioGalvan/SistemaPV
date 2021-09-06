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
        DbSet<CUser> Users { get; set; }
        DbSet<CCategory> Categories { get; set; }
        DbSet<CProduct> Products { get; set; }
        DbSet<CBrand> Brands { get; set; }
        DbSet<CSale> Sales { get; set; }
        //Contexto de datos
        public DataContext(DbContextOptions<DataContext> options) : base(options)

        {

        }
    }
}
