using LionSkyNot.Models.Recipe;

using LionSkyNot.Models.Recipes;

using LionSkyNot.Views.ViewModels.Recipes;


namespace LionSkyNot.Services.Recipes
{
    public interface IRecipeService
    {

        Task CreateAsync(
                    string name,
                    string description,
                    string imgUrl);

        Task<bool> DeleteAsync(int id);

        Task<bool> EditRecipeAsync(
                        int id,
                        string name,
                        string imageUrl,
                        string description);

        RecipeFormModel GetRecipeById(int id);

        IEnumerable<RecipeViewModel> GetAll();

        IEnumerable<RecipeFormModelForAdmin> GetAllRecipesForAdmin();

    }
}
