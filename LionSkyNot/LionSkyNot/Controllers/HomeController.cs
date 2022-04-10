using System.Diagnostics;

using LionSkyNot.Models;

using LionSkyNot.Services.Trainers;

using LionSkyNot.Views.ViewModels.Trainers;

using Microsoft.AspNetCore.Mvc;


namespace LionSkyNot.Controllers
{
    public class HomeController : BaseController
    {

        private ITrainerService trainerService;

        public HomeController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
        }



        public IActionResult Index(IEnumerable<TrainerViewModel> trainerModel)
        => View(this.trainerService.TopTrainers());


        public IActionResult StatusCode (int code)
        {

            if(code == 404)
            {
                return View("NotFound");
            }

            else
            {
                return View("SomethingWentWrong");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}