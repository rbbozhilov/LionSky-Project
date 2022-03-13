using System.Collections.Generic;
using System.Diagnostics;

using LionSkyNot.Models;
using LionSkyNot.Services.Trainers;
using LionSkyNot.Views.ViewModels.Trainers;
using Microsoft.AspNetCore.Authorization;
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
        {

            trainerModel = this.trainerService.TopTrainers();

            return View(trainerModel);
        }

      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}