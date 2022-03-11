using LionSkyNot.Data;
using LionSkyNot.Models.Exercises;
using LionSkyNot.Models.Products;
using LionSkyNot.Models.Recipe;
using LionSkyNot.Services.Exercises;
using LionSkyNot.Services.Products;
using LionSkyNot.Services.Recipes;
using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers
{
    public class AdminController : BaseController
    {

        private IRecipeService recipeService;
        private IExerciseService exerciseService;
        private IProductService productService;



        public AdminController(
                               IRecipeService recipeService,
                               IExerciseService exerciseService,
                               IProductService productService)
        {
            this.recipeService = recipeService;
            this.exerciseService = exerciseService;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProduct()
        {
            return View(new AddProductFormModel()
            {
                Type = this.productService.GetAllTypesProduct(),
                Brand = this.productService.GetAllBrandProduct()
            });

        }

        [HttpPost]
        public IActionResult AddProduct(AddProductFormModel productModel)
        {

            if (!ModelState.IsValid)
            {
                productModel.Type = this.productService.GetAllTypesProduct();
                productModel.Brand = this.productService.GetAllBrandProduct();

                return View(productModel);
            }

            this.productService.CreateProduct(productModel.Name,
                productModel.Price,
                productModel.Description,
                productModel.ImageUrl,
                productModel.TypeId,
                productModel.BrandId);

            return RedirectToAction("Index");
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
        public IActionResult AddExercise(AddExerciseFormModel exerciseModel)
        {
            
            if (!ModelState.IsValid)
            {
                exerciseModel.Type = this.exerciseService.GetAllTypeExercises();
                return View(exerciseModel);
            }

            this.exerciseService.Create(exerciseModel.Name, exerciseModel.ImageUrl, exerciseModel.VideoUrl, exerciseModel.TypeId);


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
