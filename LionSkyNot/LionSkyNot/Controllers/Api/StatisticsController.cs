using LionSkyNot.Data;
using LionSkyNot.Models.Api.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers.Api
{

    [ApiController]
    public class StatisticsController : ControllerBase
    {

        private LionSkyDbContext data;

        public StatisticsController(LionSkyDbContext data)
        {
            this.data = data;
        }


        [HttpGet]
        [Route("api/statistics")]
        public ActionResult<StatisticsResponseModel> GetStatistics()
        {

            var statistics = new StatisticsResponseModel()
            {
                RecipeCount = this.data.Recipes.Count(),
                ProductCount = this.data.Products.Count(),
                ExerciseCount = this.data.Exercises.Count(),
                ClassesCount = this.data.Classes.Count(),
                TrainerCount = this.data.Trainers.Count(),
            };


            return statistics;
        }

    }
}
