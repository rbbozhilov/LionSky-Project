using LionSkyNot.Data;
using LionSkyNot.Models.Api.Statistics;

namespace LionSkyNot.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {

        private LionSkyDbContext data;

        public StatisticsService(LionSkyDbContext data)
        {
            this.data = data;
        }



        public StatisticsResponseModel GetStatistics()
        => new StatisticsResponseModel()
        {
            ProductCount = this.data.Products.Count(),
            ClassesCount = this.data.Classes.Count(),
            ExerciseCount = this.data.Exercises.Count(),
            TrainerCount = this.data.Trainers.Count(),
            RecipeCount = this.data.Recipes.Count()
        };
    }
}
