﻿using LionSkyNot.Data;
using LionSkyNot.Data.Models.Classes;
using LionSkyNot.Infrastructure;
using LionSkyNot.Models;
using LionSkyNot.Services.Trainers;
using LionSkyNot.Views.ViewModels.Trainers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers
{
    public class TrainerController : BaseController
    {

        private ITrainerService trainerService;
        private LionSkyDbContext data;


        public TrainerController(ITrainerService trainerService, LionSkyDbContext data)
        {
            this.trainerService = trainerService;
            this.data = data;
        }


        public IActionResult Index(string searchTerm)
        {

            if (!string.IsNullOrWhiteSpace(searchTerm) && !string.IsNullOrEmpty(searchTerm))
            {
                var searchTrainers = trainerService.SearchTrainerByName(searchTerm);

                var allTrainersViewModel = new AllTrainersViewModel()
                {
                    Trainers = searchTrainers
                };

                return View("TrainerSearch", allTrainersViewModel);

            }


            return View();
        }

        [Authorize]
        public IActionResult BecomeTrainer()
        {
            return View(new AddTrainerFormModel
            {
                Categorie = this.trainerService.GetAllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult BecomeTrainer(AddTrainerFormModel trainerModel)
        {
            trainerModel.Categorie = this.trainerService.GetAllCategories();
            var currentUserId = ClaimsPrincipalExtensions.GetId(this.User);
            var allTrainersUserId = this.trainerService.GetAllTrainersUserId();


            if (allTrainersUserId.Any(a => a.UserId == currentUserId))
            {
                ModelState.AddModelError("error", "The current user is already a trainer!");
            }

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
                                       trainerModel.Description,
                                       currentUserId);


            return RedirectToAction("Index");
        }


        public IActionResult YogaTrainers(AllTrainersViewModel trainersModel)
        {

            trainersModel.Trainers = this.trainerService.GetAllTrainersByCategory("Yoga");

            return View(trainersModel);
        }

        public IActionResult BoxTrainers(AllTrainersViewModel trainersModel)
        {

            trainersModel.Trainers = this.trainerService.GetAllTrainersByCategory("Box");


            return View(trainersModel);
        }

        public IActionResult MmaTrainers(AllTrainersViewModel trainersModel)
        {

            trainersModel.Trainers = this.trainerService.GetAllTrainersByCategory("MMA");


            return View(trainersModel);
        }



        public IActionResult FitnessTrainers(AllTrainersViewModel trainersModel)
        {

            trainersModel.Trainers = this.trainerService.GetAllTrainersByCategory("Fitness");

            return View(trainersModel);
        }

        public IActionResult WrestlerTrainers(AllTrainersViewModel trainersModel)
        {
            trainersModel.Trainers = this.trainerService.GetAllTrainersByCategory("Wrestling");

            return View(trainersModel);
        }

        public IActionResult AthleticTrainers(AllTrainersViewModel trainersModel)
        {
            trainersModel.Trainers = this.trainerService.GetAllTrainersByCategory("Athletic");

            return View(trainersModel);
        }

    }
}
