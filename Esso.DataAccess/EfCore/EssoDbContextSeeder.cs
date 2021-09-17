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
            var userName = "test@esso.com";
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
        private static void SeedEntities(EssoDbContext db)
        {
            if (!db.Countries.Any())
            {
                db.Countries.Add(new Country()
                {
                    Name = "Türkiye"
                });
                db.SaveChanges();
                db.Countries.Add(new Country()
                {
                    Name = "Almanya"
                });
                db.SaveChanges();
            }

            if (!db.Cities.Any())
            {
                db.Cities.Add(new City()
                {
                    Name = "Ankara",
                    CountryID = 1,
                });
                db.Cities.Add(new City()
                {
                    Name = "İstanbul",
                    CountryID = 1,
                });

                db.Cities.Add(new City()
                {
                    Name = "İzmir",
                    CountryID = 1,
                });

                db.Cities.Add(new City()
                {
                    Name = "Bursa",
                    CountryID = 1,
                });

                db.Cities.Add(new City()
                {
                    Name = "Kayseri",
                    CountryID = 1,
                });
                db.Cities.Add(new City()
                {
                    Name = "Trabzon",
                    CountryID = 1,
                });
                db.Cities.Add(new City()
                {
                    Name = "Rize",
                    CountryID = 1,
                });
                db.Cities.Add(new City()
                {
                    Name = "Bolu",
                    CountryID = 1,
                });
                db.Cities.Add(new City()
                {
                    Name = "Iğdır",
                    CountryID = 1,
                });
                db.Cities.Add(new City()
                {
                    Name = "Urfa",
                    CountryID = 1,
                });
                db.Cities.Add(new City()
                {
                    Name = "Antalya",
                    CountryID = 1,
                });
                db.Cities.Add(new City()
                {
                    Name = "Muğla",
                    CountryID = 1,
                });

                db.Cities.Add(new City()
                {
                    Name = "Berlin",
                    CountryID = 2,
                });
                db.Cities.Add(new City()
                {
                    Name = "Münih",
                    CountryID = 2,
                });
                db.SaveChanges();
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
                    SeedEntities(db);
                }
            }
            return host;
        }
    }
}
