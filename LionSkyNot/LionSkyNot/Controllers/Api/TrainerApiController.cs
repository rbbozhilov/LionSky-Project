using LionSkyNot.Services.Trainers;
using LionSkyNot.Views.ViewModels.Trainers;
using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers.Api
{
    [ApiController]
    public class TrainerApiController : ControllerBase
    {

        private ITrainerService trainerService;

        public TrainerApiController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
        }

        
        [Route("api/trainers")]
        [HttpGet]
        public ActionResult<IEnumerable<TrainerViewModel>> GetTopTrainers()
        {

            var trainers = this.trainerService.TopTrainers().ToList();

            if (!trainers.Any())
            {
                return NotFound();
            }

            return trainers;
        }


        [Route("api/trainers/{id}")]
        [HttpGet]
        public ActionResult<TrainerViewModel> GetTrainerById(int id)
        {

            var trainer = trainerService.GetTrainerById(id);

            if(trainer == null)
            {
                return NotFound();
            }

            return trainer;
        }

    }
}
