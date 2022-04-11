using System.Diagnostics;

using LionSkyNot.Models;

using LionSkyNot.Services.Trainers;

using LionSkyNot.Views.ViewModels.Trainers;

using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Caching.Memory;


namespace LionSkyNot.Controllers
{
    public class HomeController : BaseController
    {

        private ITrainerService trainerService;
        private IMemoryCache cache;

        public HomeController(ITrainerService trainerService,IMemoryCache cache)
        {
            this.trainerService = trainerService;
            this.cache = cache;
        }



        public IActionResult Index(IEnumerable<TrainerViewModel> trainerModel)
        {

            const string topTrainersKeyCache = "topTrainersKeyCache";

            var topTrainers = this.cache.Get<IEnumerable<TrainerViewModel>>(topTrainersKeyCache);

            if (topTrainers == null)
            {
                topTrainers = this.trainerService.TopTrainers();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));


                this.cache.Set(topTrainersKeyCache, topTrainers);
            }

            return View(topTrainers);
        }


        public IActionResult StatusCode(int code)
        {

            if (code == 404)
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