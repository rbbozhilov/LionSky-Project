using LionSkyNot.Data;
using LionSkyNot.Data.Models.Recipe;
using LionSkyNot.Models.Recipe;
using LionSkyNot.Models.Recipes;
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
        => data.Recipes
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

        public RecipeFormModel GetRecipeById(int id)
         => this.data.Recipes
                      .Where(r => r.Id == id && r.IsDeleted == false)
                      .Select(r => new RecipeFormModel()
                      {
                          Name = r.Name,
                          ImageUrl = r.ImageUrl,
                          Description = r.Description,
                          Calories = r.Calories,
                          Carbohydrates = r.Carbohydrates,
                          Fat = r.Fat,
                          Protein = r.Protein
                      })
                      .FirstOrDefault();



        public bool EditRecipe(
            int id,
            string name,
            string imageUrl,
            string description,
            float calories,
            float carbohydrates,
            float fat,
            float protein)
        {
            var currentRecipe = this.data.Recipes
                                         .Where(r => r.Id == id && r.IsDeleted == false)
                                         .FirstOrDefault();

            if(currentRecipe == null)
            {
                return false;
            }

            currentRecipe.Name = name;
            currentRecipe.Description = description;
            currentRecipe.Calories = calories;
            currentRecipe.Protein = protein;
            currentRecipe.ImageUrl = imageUrl;
            currentRecipe.Carbohydrates = carbohydrates;
            currentRecipe.Fat = fat;

            this.data.SaveChanges();

            return true;
        }


        public IEnumerable<RecipeFormModelForAdmin> GetAllRecipesForAdmin()
        {

            return this.data.Recipes
                            .Where(r => r.IsDeleted == false)
                            .Select(r => new RecipeFormModelForAdmin()
                            {
                                Id = r.Id,
                                Name = r.Name
                            })
                            .ToList();


        }


        //TODOO
        public bool Delete(int id)
        {
            var currentRecipe = this.data.Recipes
                                         .Where(r => r.Id == id && r.IsDeleted == false)
                                         .FirstOrDefault();

            if(currentRecipe == null)
            {
                return false;
            }

            currentRecipe.IsDeleted = true;

            this.data.SaveChanges();

            return true;
        }

    }
}
