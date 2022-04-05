using LionSkyNot.Data;

using LionSkyNot.Data.Models.Classes;

using LionSkyNot.Services.Trainers;

using LionSkyNot.Tests.Mock;
using System;
using System.Linq;
using Xunit;


namespace LionSkyNot.Tests.Services
{

    public class TrainerServiceTest
    {

        [Fact]
        public void ShouldNotDeleteTrainerAndReturnFalse()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);

            //Act

            data.Trainers.Add(new Trainer()
            {
                Id = 1,
                UserId = "userid",
                Description = "testtest",
                ImageUrl = "eqweq",
                FullName = "eqweq"
            });
            data.SaveChanges();

            bool result = trainerService.Delete(2);

            //Assert

            Assert.False(result);
            Assert.False(data.Trainers.Any(t => t.Id == 2));


        }

        [Fact]
        public void ShouldDeleteTrainerCorrectAndReturnTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);

            //Act

            data.Trainers.Add(new Trainer()
            {
                Id = 1,
                UserId = "userid",
                Description = "testtest",
                ImageUrl = "eqweq",
                FullName = "eqweq"
            });
            data.SaveChanges();

            bool result = trainerService.Delete(1);

            //Assert

            Assert.True(result);
            Assert.False(data.Trainers.Any());


        }


        [Fact]
        public void ShouldCreateTrainerCorrect()
        {

            //Arrange
            var birthDate = new DateTime(2022, 05, 06);
            var userId = "test";
            var imageUrl = "image";
            var description = "description";
            var fullName = "Grisho";
            var categorieId = 1;
            var height = 150;
            var weight = 80;
            var yearOfExperience = 20;
            var isCandidate = false;

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);

            var expectedTrainer = new Trainer()
            {
                UserId = "test",
                ImageUrl = "image",
                Description = "description",
                BirthDate = birthDate,
                FullName = "Grisho",
                CategorieId = 1,
                Height = 150,
                Weight = 80,
                YearOfExperience = 20,
                IsCandidate = false

            };

            //Act

            trainerService.Create(
                                  fullName,
                                  yearOfExperience,
                                  imageUrl,
                                  height,
                                  weight,
                                  birthDate,
                                  categorieId,
                                  description,
                                  userId,
                                  isCandidate);



            var actualTrainer = data.Trainers.Where(t => t.UserId == userId);

            //Assert

            Assert.NotNull(actualTrainer);


        }


        [Fact]
        public void AddCandidateShouldWorkCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);
            var currentCandidate = new Trainer()
            {
                Id = 1,
                FullName = "test",
                UserId = "testuser",
                Description = "description",
                ImageUrl = "imageurl",
                IsCandidate = true
            };



            //Act

            data.Trainers.Add(currentCandidate);

            trainerService.AddCandidate(currentCandidate);

            //Assert

            Assert.False(currentCandidate.IsCandidate);

        }


        [Fact]
        public void IsTrainerShouldReturnTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);

            var currentTrainer = new Trainer()
            {
                Id = 1,
                FullName = "test",
                UserId = "testuser",
                Description = "description",
                ImageUrl = "imageurl",
                IsCandidate = false
            };



            //Act

            data.Trainers.Add(currentTrainer);
            data.SaveChanges();

            bool isTrainer = trainerService.IsTrainer("testuser");

            //Assert

            Assert.True(isTrainer);

        }

        [Fact]
        public void IsTrainerShouldReturnFalse()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);

            var currentTrainer = new Trainer()
            {
                Id = 1,
                FullName = "test",
                UserId = "testuser",
                Description = "description",
                ImageUrl = "imageurl",
                IsCandidate = false
            };



            //Act

            data.Trainers.Add(currentTrainer);
            data.SaveChanges();

            bool isTrainer = trainerService.IsTrainer("test1");

            //Assert

            Assert.False(isTrainer);

        }

        [Fact]
        public void IsCandidateShouldReturnTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);

            var currentCandidate = new Trainer()
            {
                Id = 1,
                FullName = "test",
                UserId = "testuser",
                Description = "description",
                ImageUrl = "imageurl",
                IsCandidate = true
            };



            //Act

            data.Trainers.Add(currentCandidate);
            data.SaveChanges();

            bool isCandidate = trainerService.IsCandidate("testuser");

            //Assert

            Assert.True(isCandidate);

        }


        [Fact]
        public void IsCandidateShouldReturnFalse()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);

            var currentCandidate = new Trainer()
            {
                Id = 1,
                FullName = "test",
                UserId = "testuser",
                Description = "description",
                ImageUrl = "imageurl",
                IsCandidate = true
            };



            //Act

            data.Trainers.Add(currentCandidate);
            data.SaveChanges();

            bool isCandidate = trainerService.IsCandidate("test");

            //Assert

            Assert.False(isCandidate);

        }


        [Fact]
        public void GetCandidateTrainerById_ShouldReturnCorrectTrainer()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);

            var currentCandidate = new Trainer()
            {
                Id = 1,
                FullName = "test",
                UserId = "testuser",
                Description = "description",
                ImageUrl = "imageurl",
                IsCandidate = true
            };


            //Act

            data.Trainers.Add(currentCandidate);
            data.SaveChanges();

            var actualCandidate = trainerService.GetCandidateTrainerById(1);

            //Assert

            Assert.Equal(currentCandidate,actualCandidate);

        }


        [Fact]
        public void GetCandidateTrainerById_ShouldReturnNull()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);

            var currentCandidate = new Trainer()
            {
                Id = 1,
                FullName = "test",
                UserId = "testuser",
                Description = "description",
                ImageUrl = "imageurl",
                IsCandidate = true
            };



            //Act

            data.Trainers.Add(currentCandidate);
            data.SaveChanges();

            var actualCandidate = trainerService.GetCandidateTrainerById(2);

            //Assert

            Assert.Null(actualCandidate);

        }


        [Fact]
        public void ReturningAllCategories()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);

            data.Categories.Add(new Categorie()
            {
                Id = 1,
                Name = "universal"

            });





            //Act

            data.SaveChanges();
            var categories = trainerService.GetAllCategories();



            //Assert
            Assert.Equal(1, categories.Count());
            Assert.Equal("universal", categories.First().Name);
           

        }

    }
}
