using LionSkyNot.Data;
using LionSkyNot.Models.Exercises;
using LionSkyNot.Models.Recipe;
using LionSkyNot.Services.Exercises;
using LionSkyNot.Services.Recipes;
using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers
{
    public class AdminController : BaseController
    {

        private IRecipeService recipeService;
        private IExerciseService exerciseService;



        public AdminController(
                               IRecipeService recipeService,
                               IExerciseService exerciseService)
        {
            this.recipeService = recipeService;
            this.exerciseService = exerciseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProduct()
        {
            return View();
        }


        public IActionResult DeleteProduct()
        {
            return View();
        }

        public IActionResult AddRecipe()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRecipe(AddRecipeFormModel recipeModel)
        {

            if (!ModelState.IsValid)
            {
                return View(recipeModel);
            }

            this.recipeService.Create(
                                      recipeModel.Name,
                                      recipeModel.Description,
                                      recipeModel.Protein,
                                      recipeModel.Calories,
                                      recipeModel.Fat,
                                      recipeModel.ImageUrl,
                                      recipeModel.Carbohydrates);



            return RedirectToAction("Index");
        }

        public IActionResult DeleteRecipe()
        {
            return View();
        }

        public IActionResult AddClass()
        {
            return View();
        }

        public IActionResult DeleteClass()
        {
            return View();
        }

        public IActionResult GetCandidates()
        {
            return View();
        }

        public IActionResult AddExercise()
        {
            return View(new AddExerciseFormModel()
            {
                Type = this.exerciseService.GetAllTypeExercises()
            });
        }

        [HttpPost]
        public IActionResult AddExercise(AddExerciseFormModel exerciseFormModel)
        {
            if (!ModelState.IsValid)
            {
                exerciseFormModel.Type = this.exerciseService.GetAllTypeExercises();

                return View(exerciseFormModel);
            }

            this.exerciseService.Create(exerciseFormModel.Name, exerciseFormModel.ImageUrl, exerciseFormModel.VideoUrl, exerciseFormModel.TypeId);


            return RedirectToAction("Index");
        }

        public IActionResult DeleteExercise()
        {
            return View();
        }

        public IActionResult FiredTrainer()
        {
            return View();
        }

        public IActionResult FiredCooker()
        {
            return View();
        }

    }
}
