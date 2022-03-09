using LionSkyNot.Data;
using LionSkyNot.Data.Models.Class;
using LionSkyNot.Data.Models.Exercise;
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
            SeedTypeExercise(data);

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
                });

            data.SaveChanges();
        }

        public static void SeedTypeExercise(LionSkyDbContext data)
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

    }
}
