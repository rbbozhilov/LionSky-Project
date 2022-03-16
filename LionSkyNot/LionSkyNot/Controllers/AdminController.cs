﻿using LionSkyNot.Data;
using LionSkyNot.Models;
using LionSkyNot.Models.Exercises;
using LionSkyNot.Models.Products;
using LionSkyNot.Models.Recipe;
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
        private LionSkyDbContext data;



        public AdminController(
                               IRecipeService recipeService,
                               IExerciseService exerciseService,
                               IProductService productService,
                               ITrainerService trainerService,
                               LionSkyDbContext data)
        {
            this.recipeService = recipeService;
            this.exerciseService = exerciseService;
            this.productService = productService;
            this.trainerService = trainerService;
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

            if(!this.data.Types.Any(t=> t.Id == productModel.TypeId))
            {
                this.ModelState.AddModelError(nameof(productModel.TypeId), "Don't make some hack tries!");
            }

            if(!this.data.Brands.Any(b=>b.Id == productModel.BrandId))
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


        public IActionResult AddTrainer()
        {
            return View(new AddTrainerFormModel
            {
                Categorie = this.trainerService.GetAllCategories()
            });
        }

        [HttpPost]
        public IActionResult AddTrainer(AddTrainerFormModel trainerModel)
        {

            trainerModel.Categorie = this.trainerService.GetAllCategories();

            if (!ModelState.IsValid)
            {
                return View(trainerModel);
            }

            this.trainerService.Create(trainerModel.FullName,
                                       trainerModel.YearOfExperience,
                                       trainerModel.ImageUrl,
                                       trainerModel.Height,
                                       trainerModel.Weight,
                                       trainerModel.BirthDate,
                                       trainerModel.CategorieId,
                                       trainerModel.Description);

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

            if(!this.data.TypeExercises.Any(e=>e.Id == exerciseModel.TypeId))
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
