using LionSkyNot.Models.Api.Statistics;

using LionSkyNot.Services.Statistics;

using Microsoft.AspNetCore.Mvc;


namespace LionSkyNot.Controllers.Api
{

    [ApiController]
    public class StatisticsController : ControllerBase
    {

        private IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }



        [Route("api/statistics")]
        [HttpGet]
        public ActionResult<StatisticsResponseModel> GetStatistics()
        => this.statisticsService.GetStatistics();

    }
}
