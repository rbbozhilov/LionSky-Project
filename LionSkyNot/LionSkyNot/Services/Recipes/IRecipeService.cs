using LionSkyNot.Models.Recipe;

using LionSkyNot.Models.Recipes;

using LionSkyNot.Views.ViewModels.Recipes;


namespace LionSkyNot.Services.Recipes
{
    public interface IRecipeService
    {

        void Create(
                    string name,
                    string description,
                    string imgUrl);

        bool Delete(int id);

        bool EditRecipe(
                        int id,
                        string name,
                        string imageUrl,
                        string description);

        RecipeFormModel GetRecipeById(int id);

        IEnumerable<RecipeViewModel> GetAll();

        IEnumerable<RecipeFormModelForAdmin> GetAllRecipesForAdmin();

    }
}
