using LionSkyNot.Controllers;
using LionSkyNot.Data.Models.Classes;
using LionSkyNot.Models;
using LionSkyNot.Services.Classes;
using LionSkyNot.Services.Trainers;
using LionSkyNot.Tests.Mock;
using LionSkyNot.Views.ViewModels.Trainers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace LionSkyNot.Tests.Controllers
{
    public class TrainerControllerTests
    {

        [Fact]
        public void Index_ShouldReturnCorrectViewResult()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);
            var classService = new ClassService(data);

            var trainerController = new TrainerController(
                                                          trainerService,
                                                          classService);


            //Act

            var result = trainerController.Index(string.Empty);

            //Assert

            Assert.IsType<ViewResult>(result);


        }


        [Fact]
        public void Index_ShouldReturnCorrectViewResultAndViewModel()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);
            var classService = new ClassService(data);

            var trainerController = new TrainerController(
                                                          trainerService,
                                                          classService);

            var trainer = new Trainer()
            {
                Id = 1,
                UserId = "someid",
                FullName = "Conor",
                ImageUrl = "image",
                Description = "description",
                Categorie = new Categorie() { Id = 1, Name = "Box" }
            };

            data.Trainers.Add(trainer);
            data.SaveChanges();

            //Act

            var result = trainerController.Index("c");

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);

            var trainers = Assert.IsType<AllTrainersViewModel>(viewModel.Model);
            Assert.Equal("TrainerSearch", viewModel.ViewName);
            Assert.True(trainers.Trainers.Any());

        }


        [Fact]
        public void BecomeTrainer_ShouldReturnViewResultlWithCorrectViewModel()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);
            var classService = new ClassService(data);

            var trainerController = new TrainerController(
                                                          trainerService,
                                                          classService);

            var categorie = new Categorie()
            {
                Id = 1,
                Name = "Box"
            };

            var categorie2 = new Categorie()
            {
                Id = 2,
                Name = "Mma"
            };

            data.Categories.AddRange(categorie,categorie2);
            data.SaveChanges();

            //Act

            var result = trainerController.BecomeTrainer();

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);

            var categories = Assert.IsType<AddTrainerFormModel>(viewModel.Model);

            
            Assert.True(categories.Categorie.Any(c => c.Name == "Box"));
            Assert.True(categories.Categorie.Any(c => c.Name == "Mma"));

        }


        [Fact]
        public void CandidateSuccess_ShouldReturnCorrectViewResult()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);
            var classService = new ClassService(data);

            var trainerController = new TrainerController(
                                                          trainerService,
                                                          classService);


            //Act

            var result = trainerController.CandidatureSuccess();

            //Assert

             Assert.IsType<ViewResult>(result);

        }


        [Fact]
        public void YogaTrainers_ShouldReturnCorrectViewResultWithCorrectViewModel()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);
            var classService = new ClassService(data);

            var trainerController = new TrainerController(
                                                          trainerService,
                                                          classService);


            var categorie = new Categorie()
            {
                Id = 1,
                Name = "Yoga"
            };

            var trainer = new Trainer()
            {
                Id = 1,
                UserId = "someid",
                FullName = "Conor",
                ImageUrl = "image",
                Description = "description",
                Categorie = categorie
            };

            data.Categories.Add(categorie);
            data.Trainers.Add(trainer);
            data.SaveChanges();

            var trainerModel = new AllTrainersViewModel();

            //Act

            var result = trainerController.YogaTrainers(trainerModel);

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var trainers = Assert.IsType<AllTrainersViewModel>(viewModel.Model);
            Assert.True(trainers.Trainers.Any());

        }


        [Fact]
        public void Box_ShouldReturnCorrectViewResultWithCorrectViewModel()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);
            var classService = new ClassService(data);

            var trainerController = new TrainerController(
                                                          trainerService,
                                                          classService);


            var categorie = new Categorie()
            {
                Id = 1,
                Name = "Box"
            };

            var trainer = new Trainer()
            {
                Id = 1,
                UserId = "someid",
                FullName = "Conor",
                ImageUrl = "image",
                Description = "description",
                Categorie = categorie
            };

            data.Categories.Add(categorie);
            data.Trainers.Add(trainer);
            data.SaveChanges();

            var trainerModel = new AllTrainersViewModel();

            //Act

            var result = trainerController.BoxTrainers(trainerModel);

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var trainers = Assert.IsType<AllTrainersViewModel>(viewModel.Model);
            Assert.True(trainers.Trainers.Any());

        }


        [Fact]
        public void MmaTrainers_ShouldReturnCorrectViewResultWithCorrectViewModel()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);
            var classService = new ClassService(data);

            var trainerController = new TrainerController(
                                                          trainerService,
                                                          classService);


            var categorie = new Categorie()
            {
                Id = 1,
                Name = "MMA"
            };

            var trainer = new Trainer()
            {
                Id = 1,
                UserId = "someid",
                FullName = "Conor",
                ImageUrl = "image",
                Description = "description",
                Categorie = categorie
            };

            data.Categories.Add(categorie);
            data.Trainers.Add(trainer);
            data.SaveChanges();

            var trainerModel = new AllTrainersViewModel();

            //Act

            var result = trainerController.MmaTrainers(trainerModel);

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var trainers = Assert.IsType<AllTrainersViewModel>(viewModel.Model);
            Assert.True(trainers.Trainers.Any());

        }


        [Fact]
        public void FitnessTrainers_ShouldReturnCorrectViewResultWithCorrectViewModel()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);
            var classService = new ClassService(data);

            var trainerController = new TrainerController(
                                                          trainerService,
                                                          classService);


            var categorie = new Categorie()
            {
                Id = 1,
                Name = "Fitness"
            };

            var trainer = new Trainer()
            {
                Id = 1,
                UserId = "someid",
                FullName = "Conor",
                ImageUrl = "image",
                Description = "description",
                Categorie = categorie
            };

            data.Categories.Add(categorie);
            data.Trainers.Add(trainer);
            data.SaveChanges();

            var trainerModel = new AllTrainersViewModel();

            //Act

            var result = trainerController.FitnessTrainers(trainerModel);

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var trainers = Assert.IsType<AllTrainersViewModel>(viewModel.Model);
            Assert.True(trainers.Trainers.Any());

        }


        [Fact]
        public void WrestlerTrainers_ShouldReturnCorrectViewResultWithCorrectViewModel()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);
            var classService = new ClassService(data);

            var trainerController = new TrainerController(
                                                          trainerService,
                                                          classService);


            var categorie = new Categorie()
            {
                Id = 1,
                Name = "Wrestling"
            };

            var trainer = new Trainer()
            {
                Id = 1,
                UserId = "someid",
                FullName = "Conor",
                ImageUrl = "image",
                Description = "description",
                Categorie = categorie
            };

            data.Categories.Add(categorie);
            data.Trainers.Add(trainer);
            data.SaveChanges();

            var trainerModel = new AllTrainersViewModel();

            //Act

            var result = trainerController.WrestlerTrainers(trainerModel);

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var trainers = Assert.IsType<AllTrainersViewModel>(viewModel.Model);
            Assert.True(trainers.Trainers.Any());

        }


        [Fact]
        public void AthleticTrainers_ShouldReturnCorrectViewResultWithCorrectViewModel()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);
            var classService = new ClassService(data);

            var trainerController = new TrainerController(
                                                          trainerService,
                                                          classService);


            var categorie = new Categorie()
            {
                Id = 1,
                Name = "Athletic"
            };

            var trainer = new Trainer()
            {
                Id = 1,
                UserId = "someid",
                FullName = "Conor",
                ImageUrl = "image",
                Description = "description",
                Categorie = categorie
            };

            data.Categories.Add(categorie);
            data.Trainers.Add(trainer);
            data.SaveChanges();

            var trainerModel = new AllTrainersViewModel();

            //Act

            var result = trainerController.AthleticTrainers(trainerModel);

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var trainers = Assert.IsType<AllTrainersViewModel>(viewModel.Model);
            Assert.True(trainers.Trainers.Any());

        }



    }
}
