using LionSkyNot.Views.ViewModels.Recipes;

namespace LionSkyNot.Services.Recipes
{
    public interface IRecipeService
    {

        void Create(
                    string name,
                    string description,
                    float protein,
                    float calories,
                    float fat,
                    string imgUrl,
                    float carbohydrates);

        IEnumerable<RecipeViewModel> GetAll();

    }
}
