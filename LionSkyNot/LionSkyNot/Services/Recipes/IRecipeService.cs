using LionSkyNot.Models.Recipe;
using LionSkyNot.Models.Recipes;
using LionSkyNot.Views.ViewModels.Recipes;

namespace LionSkyNot.Services.Recipes
{
    public interface IRecipeService
    {

        bool Delete(int id);

        void Create(
                    string name,
                    string description,
                    float protein,
                    float calories,
                    float fat,
                    string imgUrl,
                    float carbohydrates);

        bool EditRecipe(
          int id,
          string name,
          string imageUrl,
          string description,
          float calories,
          float carbohydrates,
          float fat,
          float protein);

        RecipeFormModel GetRecipeById(int id);

        IEnumerable<RecipeViewModel> GetAll();

        IEnumerable<RecipeFormModelForAdmin> GetAllRecipesForAdmin();

    }
}
