using LionSkyNot.Data;
using LionSkyNot.Models.Class;
using LionSkyNot.Models.Exercises;
using LionSkyNot.Models.Products;
using LionSkyNot.Models.Recipe;
using LionSkyNot.Models.Recipes;
using LionSkyNot.Models.Trainers;
using LionSkyNot.Services.Classes;
using LionSkyNot.Services.Exercises;
using LionSkyNot.Services.Products;
using LionSkyNot.Services.Recipes;
using LionSkyNot.Services.Trainers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers
{

    [Authorize(Roles = "Administrator,Moderator")]
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




        [Authorize(Roles = "Moderator,Administrator")]
        public IActionResult AddProduct()
        {
            return View(new AddProductFormModel()
            {
                Type = this.productService.GetAllTypesProduct(),
                Brand = this.productService.GetAllBrandProduct()
            });

        }

        [Authorize(Roles = "Moderator,Administrator")]
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

        [Authorize(Roles = "Moderator,Administrator")]
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

        [Authorize(Roles = "Administrator")]
        public IActionResult ShowProducts(IEnumerable<ProductServiceModel> serviceModel)
        {

            serviceModel = this.productService.GetAllProductsForAdmin();

            return View(serviceModel);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult EditProduct(int id)
        {

            var currentProduct = this.productService.GetProductById(id);

            if (currentProduct == null)
            {
                return BadRequest();
            }

            return View(new EditProductFormModel()
            {
                ImageUrl = currentProduct.ImageUrl,
                Price = currentProduct.Price,
                Name = currentProduct.Name,
            });
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult EditProduct(EditProductFormModel productModel, int id)
        {


            if (!ModelState.IsValid)
            {
                return View(productModel);
            }

            
          bool isEditted =  this.productService.EditProduct(id,
                                                            productModel.ImageUrl,
                                                            productModel.Name,
                                                            productModel.Price,
                                                            productModel.PromotionPercentage);

            if (!isEditted)
            {
                return BadRequest();
            }


            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteProduct(int id)
        {

            if (!this.productService.DeleteProduct(id))
            {
                return BadRequest();
            }

            return View("Successfull");
        }


        public IActionResult ShowTrainers(IEnumerable<TrainerFormModelForAdmin> trainerModel)
        {

            trainerModel = this.trainerService.GetAllTrainersForAdmin();

            return View(trainerModel);

        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AddTrainer()
        {
            return View(new AddTrainerFromAdminFormModel
            {
                Categorie = this.trainerService.GetAllCategories()
            });
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult AddTrainer(AddTrainerFromAdminFormModel trainerModel)
        {
            var currentUser = this.data.Users.FirstOrDefault(u => u.UserName == trainerModel.Username);
            trainerModel.Categorie = this.trainerService.GetAllCategories();

            if (currentUser == null)
            {
                ModelState.AddModelError("notFindUser", "The user is not exists");
            }

            if (this.trainerService.IsTrainer(currentUser.Id))
            {
                ModelState.AddModelError("userIsTrainer", "The user is already trainer");

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

        public IActionResult DeleteTrainer(int id)
        {

            bool isDeleted = this.trainerService.Delete(id);

            if (!isDeleted)
            {
                return BadRequest();
            }



            return View("Successfull");

        }

        [Authorize(Roles = "Administrator")]
        public IActionResult ShowRecipes(IEnumerable<RecipeFormModelForAdmin> recipeModel)
        {

            recipeModel = this.recipeService.GetAllRecipesForAdmin();

            return View(recipeModel);

        }

        [Authorize(Roles = "Moderator,Administrator")]
        public IActionResult AddRecipe()
        {
            return View();
        }

        [Authorize(Roles = "Moderator,Administrator")]
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
                                      recipeModel.Protein,
                                      recipeModel.Calories,
                                      recipeModel.Fat,
                                      recipeModel.ImageUrl,
                                      recipeModel.Carbohydrates);



            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Administrator")]
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
                Protein = currentRecipe.Protein,
                Calories = currentRecipe.Calories,
                Fat = currentRecipe.Fat,
                ImageUrl = currentRecipe.ImageUrl,
                Carbohydrates = currentRecipe.Carbohydrates,
            });
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult EditRecipe(RecipeFormModel recipeModel, int id)
        {

            if (!ModelState.IsValid)
            {
                return View(recipeModel);
            }

          bool isEditted =  this.recipeService.EditRecipe(
                                                          id,
                                                          recipeModel.Name,
                                                          recipeModel.ImageUrl,
                                                          recipeModel.Description,
                                                          recipeModel.Calories,
                                                          recipeModel.Carbohydrates,
                                                          recipeModel.Fat,
                                                          recipeModel.Protein);

            if (!isEditted)
            {
                return BadRequest();
            }


            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteRecipe(int id)
        {

            if (!this.recipeService.Delete(id))
            {
                return BadRequest();
            }

            return View("Successfull");
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult ShowClasses(IEnumerable<ClassFormModelForAdmin> classModel)
        {
            classModel = this.classService.GetAllClassesForAdmin();

            return View(classModel);
        }

        [Authorize(Roles = "Moderator,Administrator")]
        public IActionResult AddClass()
        {
            return View(new ClassFormModel()
            {
                Trainers = this.classService.GetAllTrainers()
            });

        }

        [Authorize(Roles = "Moderator,Administrator")]
        [HttpPost]
        public IActionResult AddClass(ClassFormModel classModel)
        {

            if (classModel.StartDateTime > classModel.EndDateTime)
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



        [Authorize(Roles = "Administrator")]
        public IActionResult EditClass(string id)
        {

            var currentClass = this.classService.GetClassById(id);

            if(currentClass == null)
            {
                return BadRequest();
            }

            currentClass.Trainers = this.classService.GetAllTrainers();

            return View(new ClassFormModel()
            {
                ClassName = currentClass.ClassName,
                TrainerId = currentClass.TrainerId,
                Trainers = currentClass.Trainers,
                ImageUrl = currentClass.ImageUrl,
                MaxPractitionerCount = currentClass.MaxPractitionerCount,
                Price = currentClass.Price,
                StartDateTime = currentClass.StartDateTime,
                EndDateTime = currentClass.EndDateTime,
               
            });

        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult EditClass(ClassFormModel classModel,string id)
        {

            if (!ModelState.IsValid)
            {
                return View(classModel);
            }

            bool isEditted = this.classService.Edit(
                                                    id,
                                                    classModel.ClassName,
                                                    classModel.ImageUrl,
                                                    classModel.MaxPractitionerCount,
                                                    classModel.Price,
                                                    classModel.StartDateTime,
                                                    classModel.EndDateTime,
                                                    classModel.TrainerId);
                                                            

            if (!isEditted)
            {
                return BadRequest();
            }


            return RedirectToAction("Index");

        }


        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteClass(string id)
        {

            bool isDeleted = this.classService.Delete(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return View("Successfull");
        }



        [Authorize(Roles = "Moderator,Administrator")]
        public IActionResult AddExercise()
        {
            return View(new AddExerciseFormModel()
            {
                Type = this.exerciseService.GetAllTypeExercises()
            });
        }

        [Authorize(Roles = "Moderator,Administrator")]
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


        [Authorize(Roles = "Administrator")]
        public IActionResult ShowExercise(IEnumerable<ExerciseFormModelForAdmin> exerciseModel)
        {

            exerciseModel = this.exerciseService.GetAllExercises();

            return View(exerciseModel);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult EditExercise(int id)
        {

            var exercise = this.exerciseService.GetExerciseById(id);

            if(exercise == null)
            {
                return BadRequest();
            }

            return View(new EditExerciseFormModel()
            {
                VideoUrl = exercise.VideoUrl,
                ImageUrl = exercise.ImageUrl,
                Name = exercise.Name
            });
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult EditExercise(EditExerciseFormModel exerciseModel, int id)
        {

            if (!ModelState.IsValid)
            {
                return View(exerciseModel);
            }


            bool isEditted = this.exerciseService.Edit(
                                                       id,
                                                       exerciseModel.Name,
                                                       exerciseModel.ImageUrl,
                                                       exerciseModel.VideoUrl);

            if (!isEditted)
            {
                return BadRequest();
            }


            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteExercise(int id)
        {

            if (!this.exerciseService.Delete(id))
            {
                return BadRequest();
            };

            return View("Successfull");
        }

    }
}
