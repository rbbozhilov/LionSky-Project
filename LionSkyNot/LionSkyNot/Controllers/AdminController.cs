using LionSkyNot.Data;
using LionSkyNot.Models;
using LionSkyNot.Models.Class;
using LionSkyNot.Models.Exercises;
using LionSkyNot.Models.Products;
using LionSkyNot.Models.Recipe;
using LionSkyNot.Models.Trainers;
using LionSkyNot.Services.Classes;
using LionSkyNot.Services.Exercises;
using LionSkyNot.Services.Products;
using LionSkyNot.Services.Recipes;
using LionSkyNot.Services.Trainers;
using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers
{
    public class AdminController : BaseController
    {

        private IRecipeService recipeService;
        private IExerciseService exerciseService;
        private IProductService productService;
        private ITrainerService trainerService;
        private IClassService classService;
        private LionSkyDbContext data;



        public AdminController(
                               IRecipeService recipeService,
                               IExerciseService exerciseService,
                               IProductService productService,
                               ITrainerService trainerService,
                               IClassService classService,
                               LionSkyDbContext data)
        {
            this.recipeService = recipeService;
            this.exerciseService = exerciseService;
            this.productService = productService;
            this.trainerService = trainerService;
            this.classService = classService;
            this.data = data;

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

            if (!this.data.Types.Any(t => t.Id == productModel.TypeId))
            {
                this.ModelState.AddModelError(nameof(productModel.TypeId), "Don't make some hack tries!");
            }

            if (!this.data.Brands.Any(b => b.Id == productModel.BrandId))
            {
                this.ModelState.AddModelError(nameof(productModel.BrandId), "Don't make some hack tries!");
            }

            if (!ModelState.IsValid)
            {
                productModel.Type = this.productService.GetAllTypesProduct();
                productModel.Brand = this.productService.GetAllBrandProduct();

                return View(productModel);
            }

            this.productService.CreateProduct(productModel.Name,
                productModel.Price,
                productModel.InStock,
                productModel.Description,
                productModel.ImageUrl,
                productModel.TypeId,
                productModel.BrandId);

            return RedirectToAction("Index");
        }

        public IActionResult AddProductsInStock()
        {
            bool isDone = this.productService.UpdateInStockCountOfProducts();

            if (!isDone)
            {
                return RedirectToAction("NotSuccess");
            }


            return View();
        }

        public IActionResult NotSuccess()
        {

            return View();
        }


        public IActionResult DeleteProduct()
        {
            return View();
        }


        public IActionResult AddTrainer()
        {
            return View(new AddTrainerFromAdminFormModel
            {
                Categorie = this.trainerService.GetAllCategories()
            });
        }

        [HttpPost]
        public IActionResult AddTrainer(AddTrainerFromAdminFormModel trainerModel)
        {
            var currentUser = this.data.Users.FirstOrDefault(u => u.UserName == trainerModel.Username);
            trainerModel.Categorie = this.trainerService.GetAllCategories();

            if (currentUser == null)
            {
                ModelState.AddModelError("notFindUser", "the user is not exists");
            }

            
            if (!ModelState.IsValid)
            {
                return View(trainerModel);
            }

            var existsUserId = currentUser.Id;


            this.trainerService.Create(trainerModel.FullName,
                                       trainerModel.YearOfExperience,
                                       trainerModel.ImageUrl,
                                       trainerModel.Height,
                                       trainerModel.Weight,
                                       trainerModel.BirthDate,
                                       trainerModel.CategorieId,
                                       trainerModel.Description,
                                       existsUserId);

            return RedirectToAction("Index");

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
            return View(new AddClassFormModel()
            {
                Trainers = this.classService.GetAllTrainers()
            });

        }

        [HttpPost]
        public IActionResult AddClass(AddClassFormModel classModel)
        {

            if(classModel.StartDateTime > classModel.EndDateTime)
            {
                this.ModelState.AddModelError("errorDate", "Cannot start date be after end date");
            }


            if (!this.data.Trainers.Any(t => t.Id == classModel.TrainerId))
            {
                this.ModelState.AddModelError(nameof(classModel.TrainerId), "Don't make some hack tries!");
            }


            if (!ModelState.IsValid)
            {
                classModel.Trainers = this.classService.GetAllTrainers();


                return View(classModel);
            }

            this.classService.Create(
                                     classModel.ClassName,
                                     classModel.ImageUrl,
                                     classModel.Price,
                                     classModel.MaxPractitionerCount,
                                     classModel.TrainerId,
                                     classModel.StartDateTime,
                                     classModel.EndDateTime);

            return RedirectToAction("Index");
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

            if (!this.data.TypeExercises.Any(e => e.Id == exerciseModel.TypeId))
            {
                this.ModelState.AddModelError(nameof(exerciseModel.TypeId), "Don't make some hack tries!");
            }

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
