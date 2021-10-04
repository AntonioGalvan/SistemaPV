using SistemaPV.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPV.Data
{
    public class Seeder
    {
        private readonly DataContext dataContext;

        public Seeder(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task SeedAsync()
        {
            await dataContext.Database.EnsureCreatedAsync();

            if (!this.dataContext.Brands.Any())
            {
                await CheckBrand("Sony");
                await CheckBrand("HP");
                await CheckBrand("Lenovo");
                await CheckBrand("Toshiba");
                await CheckBrand("Apple");
            }

            if (!this.dataContext.Categories.Any())
            {
                await CheckCategory("Electrónicos");
                await CheckCategory("Electrodomésticos");
                await CheckCategory("Laptops");
                await CheckCategory("Celulares");
                await CheckCategory("Línea Blanca");
            }

            if (!this.dataContext.Users.Any())
            {
                await CheckSalesman("Godofredo");
            }
        }

        private async Task CheckSalesman(string name)
        {
            this.dataContext.Users.Add(new CSalesman { Name = name });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckCategory(string name)
        {
            this.dataContext.Categories.Add(new CCategory { Name = name });
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckBrand(string name)
        {
            this.dataContext.Brands.Add(new CBrand { Name = name });
            await this.dataContext.SaveChangesAsync();
        }
    }
}
