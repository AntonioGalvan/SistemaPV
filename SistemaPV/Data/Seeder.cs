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
            await userHelper.CheckRoleAsync("Manager");
            await userHelper.CheckRoleAsync("Admin");

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
            if (!this.dataContext.Admins.Any())
            {
                var user = await CheckUser("Adminaso", "El Pro", "correodeadmin@gmail.com", "contraseñadeadmin", "Admin");
                await CheckAdmin(user, "Admin");
            }
            if (!this.dataContext.Salesmen.Any())
            {
                var user = await CheckUser("Charly", "Doe", "charly.doe@gmail.com", "1234565345345", "Perfumería");
                await CheckSalesmen(user, "Salesman");
                user = await CheckUser("Angelina", "Jolie", "angelina.jolie@gmail.com", "12345634543543", "Electrónicos");
                await CheckSalesmen(user, "Salesman");
                user = await CheckUser("Brad", "Pitt", "brad.pitt@gmail.com", "123456345345345", "Librería");
                await CheckSalesmen(user, "Salesman");
            }

            if (!this.dataContext.Managers.Any())
            {
                var user = await CheckUser("Carlangas", "Does", "charlys.doe@gmail.com", "1234565345345", "Perfumería");
                await CheckManager(user, "Manager");
                user = await CheckUser("Angélica", "Jolies", "angelinas.jolie@gmail.com", "12345634543543", "Electrónicos");
                await CheckManager(user, "Manager");
                user = await CheckUser("Bernardo", "Pitts", "brads.pitt@gmail.com", "123456345345345", "Librería");
                await CheckManager(user, "Manager");
            }
            if (!this.dataContext.Products.Any())
            {
                await CheckProduct("iPhone", "Es un iphone", 10, 12000, "", 5, 4);
                await CheckProduct("Surface", "Touch", 12, 17000, "", 2, 3);
                await CheckProduct("Refrigerador", "Grande", 15, 27000, "", 1, 2);
            }
        }

        private async Task CheckAdmin(CUser user, string rol)
        {
            this.dataContext.Admins.Add(new CAdmin { User = user });
            await this.dataContext.SaveChangesAsync();
            await userHelper.AddUserToRoleAsync(user, rol);
        }

        private async Task CheckProduct(string name, string description, int quantity, double price,string img, int brandid, int categoryid)
        {
            var product = new CProduct
            {
                Name = name,
                Quantity = quantity,
                Price = price,
                ImageUrl = img,
                Brand=this.dataContext.Brands.Find(brandid),
                Category=this.dataContext.Categories.Find(categoryid)
                
            };
            this.dataContext.Products.Add(product);
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckSalesmen(CUser user, string rol)
        {
            this.dataContext.Salesmen.Add(new CSalesman { User = user });
            await this.dataContext.SaveChangesAsync();
            await userHelper.AddUserToRoleAsync(user, rol);
        }

        private async Task CheckManager(CUser user, string rol)
        {
            this.dataContext.Managers.Add(new CManager { User = user });
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

        private async Task<CUser> CheckUser(string firstName, string lastName, string email, string password, string area)
        {
            var user = await userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new CUser
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    Area = area,
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
