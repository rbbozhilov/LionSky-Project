using LionSkyNot.Services.Recipes;

using LionSkyNot.Views.ViewModels.Recipes;

using Microsoft.AspNetCore.Mvc;


namespace LionSkyNot.Controllers
{
    public class RecipeController : BaseController
    {

        private IRecipeService recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }



        public IActionResult Index(IEnumerable<RecipeViewModel> recipeViewModel)
        => View(this.recipeService.GetAll());

    }
}
