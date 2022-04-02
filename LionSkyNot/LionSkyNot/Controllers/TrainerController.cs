using LionSkyNot.Data;
using LionSkyNot.Data.Models.Classes;
using LionSkyNot.Infrastructure;
using LionSkyNot.Models;
using LionSkyNot.Services.Classes;
using LionSkyNot.Services.Trainers;
using LionSkyNot.Views.ViewModels.Classes;
using LionSkyNot.Views.ViewModels.Trainers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers
{
    public class TrainerController : BaseController
    {

        private ITrainerService trainerService;
        private IClassService classService;


        public TrainerController(ITrainerService trainerService, IClassService classService)
        {
            this.trainerService = trainerService;
            this.classService = classService;
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
            var allTrainersUserId = this.trainerService.GetAllTrainersUserId(false);
            var allCandidateTrainersUserId = this.trainerService.GetAllTrainersUserId(true);
            

            if (allTrainersUserId.Any(a => a.UserId == currentUserId))
            {
                ModelState.AddModelError("error", "The current user is already a trainer!");
            }

            if(allCandidateTrainersUserId.Any(a => a.UserId == currentUserId))
            {
                ModelState.AddModelError("error", "The current user is candidate already for trainer!");
            }

            if (!ModelState.IsValid)
            {
                return View(trainerModel);
            }

            this.trainerService.Create(
                                       trainerModel.FullName,
                                       trainerModel.YearOfExperience,
                                       trainerModel.ImageUrl,
                                       trainerModel.Height,
                                       trainerModel.Weight,
                                       trainerModel.BirthDate,
                                       trainerModel.CategorieId,
                                       trainerModel.Description,
                                       currentUserId,
                                       true);


            return RedirectToAction("Index");
        }


        [Authorize]
        public IActionResult TrainerClasses(IEnumerable<ClassTrainerViewModel> classModel)
        {
            var currentUserId = ClaimsPrincipalExtensions.GetId(this.User);
            int currentTrainerId = this.trainerService.GetTrainerId(currentUserId);

            classModel = this.classService.GetAllTrainerClasses(currentTrainerId);


            return View(classModel);
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
