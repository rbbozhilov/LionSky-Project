using LionSkyNot.Data;

using LionSkyNot.Data.Models.Class;
using LionSkyNot.Data.Models.Exercise;
using LionSkyNot.Data.Models.Product;

using Microsoft.EntityFrameworkCore;
using Type = LionSkyNot.Data.Models.Product.Type;

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
            SeedProductBrand(data);
            SeedProductType(data);

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
                    new Categorie() { Name = "Fitness"},
                    new Categorie() { Name = "Yoga"},
                    new Categorie() { Name = "Box"},
                    new Categorie() { Name = "Mma"},
                    new Categorie() { Name = "Wrestling"},
                    new Categorie() { Name = "Athletic"},
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

        public static void SeedProductType(LionSkyDbContext data)
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

        public static void SeedProductBrand(LionSkyDbContext data)
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

    }
}
