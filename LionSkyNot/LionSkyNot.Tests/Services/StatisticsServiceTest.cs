using LionSkyNot.Data.Models.Classes;
using LionSkyNot.Data.Models.Exercise;
using LionSkyNot.Data.Models.Recipe;
using LionSkyNot.Data.Models.Shop;
using LionSkyNot.Services.Statistics;

using LionSkyNot.Tests.Mock;

using Xunit;


namespace LionSkyNot.Tests.Services
{
    public class StatisticsServiceTest
    {

        [Fact]
        public void GetStatisticsShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var statisticsService = new StatisticsService(data);


            //Act

            var statistics = statisticsService.GetStatistics();

            //Assert

            Assert.Equal(0, statistics.RecipeCount);
            Assert.Equal(0, statistics.TrainerCount);
            Assert.Equal(0, statistics.ClassesCount);
            Assert.Equal(0, statistics.ExerciseCount);
            Assert.Equal(0, statistics.ProductCount);

        }




        [Fact]
        public void GetStatisticsShouldBeCorrectAndReturnCorrectStatistics()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var statisticsService = new StatisticsService(data);

            var trainer = new Trainer()
            {
                Id = 1,
                FullName = "name",
                ImageUrl = "image",
                UserId = "userId",
                Description = "description"
            };

            var @class = new Class()
            {
                Id = "someId",
                ClassName = "classname",
                ImageUrl = "img"
            };

            var recipe = new Recipe()
            {
                Id = 1,
                ImageUrl = "some image",
                Name = "pizza",
                Description = "somedescription"
            };

            var product = new Product()
            {
                Id = 1,
                ImageUrl = "somee",
                Name = "name",
                Description = "description"
            };

            var exercise = new Exercise()
            {
                Id = 1,
                Name = "name2",
                ImageUrl = "someimg",
                VideoUrl = "videourl"
            };



            //Act

            data.Trainers.Add(trainer);
            data.Classes.Add(@class);
            data.Recipes.Add(recipe);
            data.Exercises.Add(exercise);
            data.Products.Add(product);
            data.SaveChanges();

            var statistics = statisticsService.GetStatistics();

            //Assert

            Assert.Equal(1, statistics.RecipeCount);
            Assert.Equal(1, statistics.TrainerCount);
            Assert.Equal(1, statistics.ClassesCount);
            Assert.Equal(1, statistics.ExerciseCount);
            Assert.Equal(1, statistics.ProductCount);

        }

    }
}
