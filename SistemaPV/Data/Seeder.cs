namespace SistemaPV.Data
{
    using Microsoft.AspNetCore.Identity;
    using SistemaPV.Data.Entities;
    using SistemaPV.Helpers;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class Seeder
    {
        private readonly DataContext dataContext;
        private readonly IUserHelper userHelper;

        public Seeder(DataContext dataContext, IUserHelper userHelper)
        {
            this.dataContext = dataContext;
            this.userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await dataContext.Database.EnsureCreatedAsync();
            await userHelper.CheckRoleAsync("Salesman");

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
            if (!this.dataContext.Salesmen.Any())
            {
                var user = await CheckUser("Charly", "Doe", "charly.doe@gmail.com", "1234565345345");
                await CheckSalesmen(user, "Salesman");
                user = await CheckUser("Angelina", "Jolie", "angelina.jolie@gmail.com", "12345634543543");
                await CheckSalesmen(user, "Salesman");
                user = await CheckUser("Brad", "Pitt", "brad.pitt@gmail.com", "123456345345345");
                await CheckSalesmen(user, "Salesman");
            }
        }

        private async Task CheckSalesmen(CUser user, string rol)
        {
            this.dataContext.Salesmen.Add(new CSalesman { User = user });
            await this.dataContext.SaveChangesAsync();
            await userHelper.AddUserToRoleAsync(user, rol);
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

        private async Task<CUser> CheckUser(string firstName, string lastName, string email, string password)
        {
            var user = await userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new CUser
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email
                };
                var result = await userHelper.AddUserAsync(user, password);
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Error no se pudo crear el usuario");
                }
            }
            return user;
        }
    }
}
