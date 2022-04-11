using System;
using System.Linq;

using LionSkyNot.Data.Models.Classes;

using LionSkyNot.Services.Trainers;

using LionSkyNot.Tests.Mock;

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

            Assert.Equal(currentCandidate, actualCandidate);

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


        [Fact]
        public void GetTopTrainers_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);

            var categorieYoga = new Categorie() { Id = 1, Name = "Yoga" };


            var trainer1 = new Trainer()
            {
                Id = 1,
                FullName = "test",
                UserId = "testuser",
                Description = "description",
                ImageUrl = "imageurl",
                YearOfExperience = 5,
                IsCandidate = false,
                Categorie = categorieYoga
            };

            var trainer2 = new Trainer()
            {
                Id = 2,
                FullName = "test1",
                UserId = "testuser1",
                Description = "description1",
                ImageUrl = "imageurl1",
                YearOfExperience = 10,
                IsCandidate = false,
                Categorie = categorieYoga
            };



            data.Categories.Add(categorieYoga);

            data.Trainers.AddRange(trainer1, trainer2);

            //Act

            data.SaveChanges();
            var bestYogaTrainer = trainerService.TopTrainers();

            //Assert

            Assert.Equal(trainer2.FullName, bestYogaTrainer.First().FullName);
            Assert.Equal(1, bestYogaTrainer.Count());

        }


        [Fact]
        public void GetTrainerId_ShouldReturnCorrectId()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);



            var trainer1 = new Trainer()
            {
                Id = 1,
                FullName = "test",
                UserId = "testuser",
                Description = "description",
                ImageUrl = "imageurl",
                YearOfExperience = 5,
                IsCandidate = false,
            };


            data.Trainers.Add(trainer1);

            //Act

            data.SaveChanges();
            int actualId = trainerService.GetTrainerId("testuser");

            //Assert

            Assert.Equal(1, actualId);

        }


        [Fact]
        public void GetTrainerId_ShouldBeNull()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);

            //Assert

            Assert.Equal(0, trainerService.GetTrainerId("userid"));

        }


        [Fact]
        public void GetTrainerByCategory_ShouldReturnCorrectTrainer()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);
            var categorieYoga = new Categorie() { Id = 1, Name = "Yoga" };
            var categorieBox = new Categorie() { Id = 2, Name = "Box" };

            var trainer1 = new Trainer()
            {
                Id = 1,
                FullName = "Yoga",
                UserId = "testuser",
                Description = "description",
                ImageUrl = "imageurl",
                YearOfExperience = 5,
                IsCandidate = false,
                Categorie = categorieYoga
            };

            var trainer2 = new Trainer()
            {
                Id = 2,
                FullName = "Box",
                UserId = "tes2tuser",
                Description = "descrip3tion",
                ImageUrl = "image3url",
                YearOfExperience = 15,
                IsCandidate = false,
                Categorie = categorieBox
            };


            data.Trainers.AddRange(trainer1,trainer2);

            //Act

            data.SaveChanges();
            var currentYogaTrainers = trainerService.GetAllTrainersByCategory("Yoga");
            var currentBoxTrainers = trainerService.GetAllTrainersByCategory("Box");


            //Assert

            Assert.Equal("Yoga", currentYogaTrainers.First().FullName);
            Assert.Equal("Box", currentBoxTrainers.First().FullName);

        }


        [Fact]
        public void GetAllTrainersUserId_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);


            var trainer1 = new Trainer()
            {
                Id = 1,
                FullName = "Yoga",
                UserId = "testuser",
                Description = "description",
                ImageUrl = "imageurl",
                YearOfExperience = 5,
                IsCandidate = false,
            };

            var trainer2 = new Trainer()
            {
                Id = 2,
                FullName = "Box",
                UserId = "tes2tuser",
                Description = "descrip3tion",
                ImageUrl = "image3url",
                YearOfExperience = 15,
                IsCandidate = true,
            };


            data.Trainers.AddRange(trainer1, trainer2);

            //Act

            data.SaveChanges();
            var allTrainersUserId = trainerService.GetAllTrainersUserId(false);

            //Assert

            Assert.Equal("testuser", allTrainersUserId.First().UserId);
            Assert.Equal(1, allTrainersUserId.Count());

        }


        [Fact]
        public void GetAllCandidateTrainerssUserId_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);


            var trainer1 = new Trainer()
            {
                Id = 1,
                FullName = "Yoga",
                UserId = "testuser",
                Description = "description",
                ImageUrl = "imageurl",
                YearOfExperience = 5,
                IsCandidate = false,
            };

            var trainer2 = new Trainer()
            {
                Id = 2,
                FullName = "Box",
                UserId = "tes2tuser",
                Description = "descrip3tion",
                ImageUrl = "image3url",
                YearOfExperience = 15,
                IsCandidate = true,
            };


            data.Trainers.AddRange(trainer1, trainer2);

            //Act

            data.SaveChanges();
            var allTrainersUserId = trainerService.GetAllTrainersUserId(true);

            //Assert

            Assert.Equal("tes2tuser", allTrainersUserId.First().UserId);
            Assert.Equal(1, allTrainersUserId.Count());

        }


        [Fact]
        public void GetTrainerById_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);


            var trainer1 = new Trainer()
            {
                Id = 1,
                FullName = "Yoga",
                UserId = "testuser",
                Description = "description",
                ImageUrl = "imageurl",
                YearOfExperience = 5,
                IsCandidate = false,
            };

            var trainer2 = new Trainer()
            {
                Id = 2,
                FullName = "Box",
                UserId = "tes2tuser",
                Description = "descrip3tion",
                ImageUrl = "image3url",
                YearOfExperience = 15,
                IsCandidate = true,
            };


            data.Trainers.AddRange(trainer1, trainer2);

            //Act

            data.SaveChanges();
            var actualTrainer = trainerService.GetTrainerById(1);

            //Assert

            Assert.Equal("Yoga", actualTrainer.FullName);

        }


        [Fact]
        public void GetAllTrainersForAdmin_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);


            var trainer1 = new Trainer()
            {
                Id = 1,
                FullName = "Yoga",
                UserId = "testuser",
                Description = "description",
                ImageUrl = "imageurl",
                YearOfExperience = 5,
                IsCandidate = false,
            };

            var trainer2 = new Trainer()
            {
                Id = 2,
                FullName = "Box",
                UserId = "tes2tuser",
                Description = "descrip3tion",
                ImageUrl = "image3url",
                YearOfExperience = 15,
                IsCandidate = true,
            };


            data.Trainers.AddRange(trainer1, trainer2);

            //Act

            data.SaveChanges();
            var allTrainers = trainerService.GetAllTrainersForAdmin();

            //Assert

            Assert.Equal("Yoga", allTrainers.First().FullName);
            Assert.Equal(1, allTrainers.Count());

        }
    }
}