using Esso.Entity;
using Esso.Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esso.DataAccess.EfCore
{
    public static class EssoDbContextSeeder
    {
        private static async Task SeedUsersAsync(
            UserManager<IdentityUser> userManager)
        {
            var userName = "test@essso.com";
            if (!await userManager.Users.AnyAsync(x => x.UserName == userName))
            {
                var user = new IdentityUser()
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(user, "Esso123.");
            }
        }
        private static void Seed(EssoDbContext db)
        {
            if (!db.Countries.Any())
            {
                db.Countries.Add(new Country()
                {
                    Id = 1,
                    Name = "Türkiye"
                });
                db.Countries.Add(new Country()
                {
                    Id = 2,
                    Name = "Almanya"
                });
            }

            if (!db.Cities.Any())
            {
                db.Cities.Add(new City()
                {
                    Id = 1,
                    Name = "Ankara",
                    CountryID = 1,
                });
                db.Cities.Add(new City()
                {
                    Id = 2,
                    Name = "İstanbul",
                    CountryID = 1,
                });
                db.Cities.Add(new City()
                {
                    Id = 3,
                    Name = "Berlin",
                    CountryID = 2,
                });
                db.Cities.Add(new City()
                {
                    Id = 4,
                    Name = "Münih",
                    CountryID = 2,
                });

            }
        }
        public static async Task<IHost> SeedAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var env = serviceProvider.GetRequiredService<IHostEnvironment>();
                var db = serviceProvider.GetRequiredService<EssoDbContext>();
                await SeedUsersAsync(userManager);

                if (env.IsDevelopment())
                {
                    Seed(db);
                }
            }
            return host;
        }
    }
}
