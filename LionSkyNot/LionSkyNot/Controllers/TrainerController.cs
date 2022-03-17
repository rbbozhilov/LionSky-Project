using LionSkyNot.Services.Trainers;
using LionSkyNot.Views.ViewModels.Trainers;
using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers
{
    public class TrainerController : BaseController
    {

        private ITrainerService trainerService;


        public TrainerController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
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
