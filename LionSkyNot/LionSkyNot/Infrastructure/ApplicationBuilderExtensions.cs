
using LionSkyNot.Data;
using LionSkyNot.Data.Models.Classes;
using LionSkyNot.Data.Models.Exercise;
using LionSkyNot.Data.Models.Shop;
using LionSkyNot.Data.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Type = LionSkyNot.Data.Models.Shop.Type;

namespace LionSkyNot.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        //Method create on each start project new migration and put some data in database
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopeServices = app.ApplicationServices.CreateScope();

            var serviceProvider = scopeServices.ServiceProvider;
            var data = serviceProvider.GetService<LionSkyDbContext>();

            data.Database.Migrate();

            SeedCategories(data);
            SeedTypeExercise(data);
            SeedProductBrand(data);
            SeedProductType(data);
            SeedAdminRole(serviceProvider);
            SeedModeratorRole(serviceProvider);
            SeedUsers(serviceProvider);

            return app;
        }

        private static void SeedCategories(LionSkyDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange
                (
                new[]
                {
                    new Categorie() { Name = "Fitness"},
                    new Categorie() { Name = "Yoga"},
                    new Categorie() { Name = "Box"},
                    new Categorie() { Name = "Mma"},
                    new Categorie() { Name = "Wrestling"},
                    new Categorie() { Name = "Athletic"},
                });

            data.SaveChanges();
        }

        private static void SeedTypeExercise(LionSkyDbContext data)
        {
            if (data.TypeExercises.Any())
            {
                return;
            }

            data.TypeExercises.AddRange
                (
                new[]
                {
                    new TypeExercise() { TypeName = "Biceps"},
                    new TypeExercise() { TypeName = "Back"},
                    new TypeExercise() { TypeName = "Chest"},
                    new TypeExercise() { TypeName = "Legs"},
                });

            data.SaveChanges();
        }

        private static void SeedProductType(LionSkyDbContext data)
        {
            if (data.Types.Any())
            {
                return;
            }

            data.Types.AddRange
                (
                new[]
                {
                    new Type() { TypeName = "Protein"},
                    new Type() { TypeName = "BCAA"},
                    new Type() { TypeName = "l-Carnitine"},
                    new Type() { TypeName = "Creatine"},
                });

            data.SaveChanges();
        }

        private static void SeedProductBrand(LionSkyDbContext data)
        {
            if (data.Types.Any())
            {
                return;
            }

            data.Brands.AddRange
                (
                new[]
                {
                    new Brand() { BrandName = "Universal"},
                    new Brand() { BrandName = "MyProtein"},
                    new Brand() { BrandName = "Optimum Nutrition"},
                    new Brand() { BrandName = "AMIX"},
                });

            data.SaveChanges();
        }

        private static void SeedAdminRole(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

     
            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync("Administrator"))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = "Administrator" };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@lionsky.net";
                    const string adminPassword = "admin12";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }


        private static void SeedModeratorRole(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();


            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync("Moderator"))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = "Moderator" };

                    await roleManager.CreateAsync(role);

                    const string moderatorEmail = "moderator@lionsky.net";
                    const string moderatorPassword = "moderator12";

                    var user = new User
                    {
                        Email = moderatorEmail,
                        UserName = moderatorEmail
                    };

                    await userManager.CreateAsync(user, moderatorPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();

        }


        private static void SeedUsers(IServiceProvider services)
        {

            var userManager = services.GetRequiredService<UserManager<User>>();
            StringBuilder builder = new StringBuilder();

            Task
               .Run(async () =>
               {

                   var userPassword = "user12";

                   for(int i = 0; i < 20; i++)
                   {

                       builder.Append("user");
                       builder.Append(i.ToString());
                       builder.Append("@lionsky.net");

                       var user = new User
                       {
                           Email = builder.ToString(),
                           UserName = builder.ToString()
                       };

                       await userManager.CreateAsync(user, userPassword);

                       builder.Clear();
                   }

    
               })
               .GetAwaiter()
               .GetResult();
            
        }

    }
}
