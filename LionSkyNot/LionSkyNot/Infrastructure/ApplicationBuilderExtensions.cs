using LionSkyNot.Data;
using LionSkyNot.Data.Models.Class;
using Microsoft.EntityFrameworkCore;

namespace LionSkyNot.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        //Method create on each start project new migration and put some data in database
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {

            using var scopeServices = app.ApplicationServices.CreateScope();

            var data = scopeServices.ServiceProvider.GetService<LionSkyDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        public static void SeedCategories(LionSkyDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange
                (
                new[]
                {


                    new Categorie() { Name = "Fitness trainer"},
                    new Categorie() { Name = "Yoga trainer"},
                    new Categorie() { Name = "Box trainer"}
                }

                );

            data.SaveChanges();
        }
    }
}
