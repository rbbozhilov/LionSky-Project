using LionSkyNot.Data;
using LionSkyNot.Data.Models.Recipe;
using LionSkyNot.Views.ViewModels.Recipes;
using System.Linq;

namespace LionSkyNot.Services.Recipes
{
    public class RecipeService : IRecipeService
    {

        private LionSkyDbContext data;

        public RecipeService(LionSkyDbContext data)
        {
            this.data = data;
        }


        public void Create(
            string name,
            string description,
            float protein,
            float calories,
            float fat,
            string imgUrl,
            float carbohydrates)
        {

            var recipe = new Recipe()
            {
                Name = name,
                Description = description,
                Protein = protein,
                Fat = fat,
                Calories = calories,
                Carbohydrates = carbohydrates,
                ImageUrl = imgUrl
            };

            this.data.Recipes.Add(recipe);

            this.data.SaveChanges();

        }


        public IEnumerable<RecipeViewModel> GetAll()
        =>      data.Recipes
               .Where(x => x.IsDeleted == false)
               .Select(x => new RecipeViewModel()
               {
                   Name = x.Name,
                   Description = x.Description,
                   Protein = x.Protein,
                   Fat = x.Fat,
                   Calories = x.Calories,
                   ImageUrl = x.ImageUrl,
               }).ToList();



        //TODOO
        public void Delete()
        {

        }

    }
}
