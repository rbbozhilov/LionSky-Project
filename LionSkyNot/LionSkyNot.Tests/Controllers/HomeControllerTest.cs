using System;
using System.Collections.Generic;

using LionSkyNot.Controllers;

using LionSkyNot.Data.Models.Classes;

using LionSkyNot.Models;

using LionSkyNot.Services.Trainers;

using LionSkyNot.Tests.Mock;

using LionSkyNot.Views.ViewModels.Trainers;

using Microsoft.AspNetCore.Mvc;

using Xunit;


namespace LionSkyNot.Tests.Controllers
{
    public class HomeControllerTest
    {

        [Fact]
        public void HomeIndex_ShouldReturnCorrectViewModelAndViewResult()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);
            var homeController = new HomeController(trainerService);

            var trainer = new Trainer()
            {
                Id = 1,
                Description = "description",
                ImageUrl = "imageurl",
                FullName = "Conor mcgregor",
                UserId = "userid",
                YearOfExperience = 5,
                BirthDate = DateTime.Now,
                Weight = 50,
                Height = 170,
                Categorie = new Categorie() { Id = 1, Name = "Box"}
            };

            data.Trainers.Add(trainer);
            data.SaveChanges();

            //Act

            var currentTrainers = new List<TrainerViewModel>();

            var viewResult = homeController.Index(currentTrainers);

            //Assert

            var viewModel = Assert.IsType<ViewResult>(viewResult);

            var listOfTrainers = Assert.IsType<List<TrainerViewModel>>(viewModel.Model);

            Assert.Equal(1, listOfTrainers.Count);

        }

    }
}
