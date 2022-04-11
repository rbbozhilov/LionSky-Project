using LionSkyNot.Models.Api.Statistics;

using LionSkyNot.Services.Statistics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LionSkyNot.Controllers.Api
{

    [ApiController]
    public class StatisticsController : ControllerBase
    {

        private IStatisticsService statisticsService;
        private IMemoryCache cache;

        public StatisticsController(IStatisticsService statisticsService, IMemoryCache cache)
        {
            this.statisticsService = statisticsService;
            this.cache = cache;
        }



        [Route("api/statistics")]
        [HttpGet]
        public ActionResult<StatisticsResponseModel> GetStatistics()
        {

            const string allStatisticsKeyCache = "AllStatisticsKeyCache";

            var allStatistics = this.cache.Get<StatisticsResponseModel>(allStatisticsKeyCache);

            if (allStatistics == null)
            {
                allStatistics = this.statisticsService.GetStatistics();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));


                this.cache.Set(allStatisticsKeyCache, allStatistics);
            }


            return allStatistics;

        }

    }
}
