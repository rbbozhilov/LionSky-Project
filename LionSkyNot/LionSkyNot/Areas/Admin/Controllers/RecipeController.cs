using LionSkyNot.Controllers;

using LionSkyNot.Models.Recipe;
using LionSkyNot.Models.Recipes;

using LionSkyNot.Services.Recipes;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static LionSkyNot.Areas.Admin.AdminConstants;


namespace LionSkyNot.Areas.Admin.Controllers
{


    [Area(AreaName)]
    public class RecipeController : BaseController
    {

        private IRecipeService recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }



        [Authorize(Roles = ModeratorAndAdminRole)]
        public IActionResult Successfull()
        {
            return View();
        }


        [Authorize(Roles = AdminRole)]
        public IActionResult ShowRecipes(IEnumerable<RecipeFormModelForAdmin> recipeModel)
        {

            recipeModel = this.recipeService.GetAllRecipesForAdmin();

            return View(recipeModel);

        }


        [Authorize(Roles = ModeratorAndAdminRole)]
        public IActionResult AddRecipe()
        => View();


        [Authorize(Roles = ModeratorAndAdminRole)]
        [HttpPost]
        public IActionResult AddRecipe(RecipeFormModel recipeModel)
        {

            if (!ModelState.IsValid)
            {
                return View(recipeModel);
            }

            this.recipeService.Create(
                                      recipeModel.Name,
                                      recipeModel.Description,
                                      recipeModel.ImageUrl);


            return RedirectToAction("Successfull");
        }


        [Authorize(Roles = AdminRole)]
        public IActionResult EditRecipe(int id)
        {
            var currentRecipe = this.recipeService.GetRecipeById(id);

            if (currentRecipe == null)
            {
                return BadRequest();
            }


            return View(new RecipeFormModel()
            {
                Name = currentRecipe.Name,
                Description = currentRecipe.Description,
                ImageUrl = currentRecipe.ImageUrl
            });
        }


        [Authorize(Roles = AdminRole)]
        [HttpPost]
        public IActionResult EditRecipe(RecipeFormModel recipeModel, int id)
        {

            if (!ModelState.IsValid)
            {
                return View(recipeModel);
            }

            bool isEditted = this.recipeService.EditRecipe(
                                                            id,
                                                            recipeModel.Name,
                                                            recipeModel.ImageUrl,
                                                            recipeModel.Description);

            if (!isEditted)
            {
                return BadRequest();
            }


            return RedirectToAction("Successfull");

        }


        [Authorize(Roles = AdminRole)]
        public IActionResult DeleteRecipe(int id)
        {

            if (!this.recipeService.Delete(id))
            {
                return BadRequest();
            }

            return View("Successfull");
        }
    }
}
