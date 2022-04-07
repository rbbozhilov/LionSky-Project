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
            ProductCount = this.data.Products
                                    .Where(p => p.IsDeleted == false)
                                    .Count(),
            ClassesCount = this.data.Classes
                                    .Where(c => c.IsDeleted == false)
                                    .Count(),
            ExerciseCount = this.data.Exercises
                                     .Where(e => e.IsDeleted == false)
                                     .Count(),
            TrainerCount = this.data.Trainers
                                    .Count(),
            RecipeCount = this.data.Recipes
                                   .Where(r => r.IsDeleted == false)
                                   .Count()
        };
    }
}
