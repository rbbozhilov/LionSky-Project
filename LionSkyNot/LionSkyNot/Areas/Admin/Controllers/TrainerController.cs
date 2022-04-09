using LionSkyNot.Controllers;

using LionSkyNot.Models.Trainers;

using LionSkyNot.Services.Trainers;
using LionSkyNot.Services.Users;
using LionSkyNot.Views.ViewModels.Trainers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static LionSkyNot.Areas.Admin.AdminConstants;


namespace LionSkyNot.Areas.Admin.Controllers
{

    [Area(AreaName)]
    [Authorize(Roles = AdminRole)]
    public class TrainerController : BaseController
    {

        private ITrainerService trainerService;
        private IUserService userService;

        public TrainerController(ITrainerService trainerService, IUserService userService)
        {
            this.trainerService = trainerService;
            this.userService = userService;
        }


        public IActionResult Successfull()
        {
            return View();
        }

        public IActionResult ShowCandidateTrainers(IEnumerable<TrainerCandidateViewModel> trainerCandidateModel)
        {
            var candidateTrainers = this.trainerService.GetAllTrainerCandidates();

            return View(candidateTrainers);
        }

        public IActionResult ShowTrainers(IEnumerable<TrainerFormModelForAdmin> trainerModel)
        {

            trainerModel = this.trainerService.GetAllTrainersForAdmin();

            return View(trainerModel);

        }

        public IActionResult AddTrainerCandidate(int id)
        {

            var currentCandidate = this.trainerService.GetCandidateTrainerById(id);

            if (currentCandidate == null)
            {
                return BadRequest();
            }

            this.trainerService.AddCandidate(currentCandidate);


            return View("Successfull");
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
            var currentUser = this.userService.GetUser(trainerModel.Username);
            trainerModel.Categorie = this.trainerService.GetAllCategories();

            if (currentUser == null)
            {
                ModelState.AddModelError("notFindUser", "The user is not exists");
            }

            if (!ModelState.IsValid)
            {
                return View(trainerModel);
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
                                       existsUserId,
                                       false);

            return RedirectToAction("Successfull");

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


    }
}
