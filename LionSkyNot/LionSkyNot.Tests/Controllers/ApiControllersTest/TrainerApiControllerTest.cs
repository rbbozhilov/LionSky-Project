using LionSkyNot.Controllers.Api;
using LionSkyNot.Data.Models.Classes;
using LionSkyNot.Services.Trainers;
using LionSkyNot.Tests.Mock;
using LionSkyNot.Views.ViewModels.Trainers;
using Microsoft.AspNetCore.Mvc;
using System;

using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace LionSkyNot.Tests.Controllers.ApiControllersTest
{

    public class TrainerApiControllerTest
    {

        [Fact]
        public void ReturningTopTrainersByYearOfExperience_ShouldBeCorrect()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);
            var trainerApiController = new TrainerApiController(trainerService);

            var categoryBox = new Categorie()
            {
                Id = 1,
                Name = "Box"
            };

            var categoryYoga = new Categorie()
            {
                Id = 2,
                Name = "Yoga"
            };

            var categoryMma = new Categorie()
            {
                Id = 3,
                Name = "MMA"
            };


            var trainerBox1 = new Trainer()
            {
                FullName = "Connor",
                BirthDate = DateTime.Now,
                Description = "Best champ ever",
                YearOfExperience = 20,
                Categorie = categoryBox,
                ImageUrl = "Some image",
                UserId = "SomeUser",
            };

            var trainerBox2 = new Trainer()
            {
                FullName = "Connor2",
                BirthDate = DateTime.Now,
                Description = "Best champ ever2",
                YearOfExperience = 5,
                Categorie = categoryBox,
                ImageUrl = "Some2 image",
                UserId = "SomeU2ser",
            };

            var trainerYoga1 = new Trainer()
            {
                FullName = "ConnorYoga",
                BirthDate = DateTime.Now,
                Description = "Best champ eve3r2",
                YearOfExperience = 20,
                Categorie = categoryYoga,
                ImageUrl = "Some22 image",
                UserId = "SomeU22ser",
            };

            var trainerYoga2 = new Trainer()
            {
                FullName = "ConnorYoga2",
                BirthDate = DateTime.Now,
                Description = "Best champ eve3r22",
                YearOfExperience = 5,
                Categorie = categoryYoga,
                ImageUrl = "Some22 i2mage",
                UserId = "SomeU222ser",
            };

            var trainerMma1 = new Trainer()
            {
                FullName = "ConnorMma1",
                BirthDate = DateTime.Now,
                Description = "Best champ eve3r2",
                YearOfExperience = 20,
                Categorie = categoryMma,
                ImageUrl = "Some22 image",
                UserId = "SomeU22ser",
            };

            var trainerMma2 = new Trainer()
            {
                FullName = "ConnorMma2",
                BirthDate = DateTime.Now,
                Description = "Best champ eve3r22",
                YearOfExperience = 5,
                Categorie = categoryMma,
                ImageUrl = "Some222 i2ma2ge",
                UserId = "SomeU222ser",
            };

            data.AddRange(trainerYoga2, trainerYoga1, trainerMma1, trainerMma2, trainerBox1, trainerBox2);

            data.SaveChanges();

            //Act

            var result = trainerApiController.GetTopTrainers();



            //Assert

            var actionResult = Assert.IsType<ActionResult<IEnumerable<TrainerViewModel>>>(result);

            var allTopTrainers = actionResult.Value;

            var actualBoxTrainer = allTopTrainers.Where(t => t.CategorieName == "Box").FirstOrDefault();
            var actualYogaTrainer = allTopTrainers.Where(t => t.CategorieName == "Yoga").FirstOrDefault();
            var actualMmaTrainer = allTopTrainers.Where(t => t.CategorieName == "MMA").FirstOrDefault();

            Assert.Equal(trainerBox1.FullName, actualBoxTrainer.FullName);
            Assert.Equal(trainerBox1.YearOfExperience, actualBoxTrainer.YearOfExperience);

            Assert.Equal(trainerYoga1.FullName, actualYogaTrainer.FullName);
            Assert.Equal(trainerYoga1.YearOfExperience, actualYogaTrainer.YearOfExperience);

            Assert.Equal(trainerMma1.FullName, actualMmaTrainer.FullName);
            Assert.Equal(trainerMma1.YearOfExperience, actualMmaTrainer.YearOfExperience);


        }



        [Fact]
        public void GetTrainerById_ShouldBeCorrectAndReturnCorrectTrainer()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var trainerService = new TrainerService(data);
            var trainerApiController = new TrainerApiController(trainerService);


            var trainerBox1 = new Trainer()
            {
                Id = 1,
                FullName = "Connor",
                BirthDate = DateTime.Now,
                Description = "Best champ ever",
                YearOfExperience = 20,
                ImageUrl = "Some image",
                UserId = "SomeUser",
            };

            var trainerBox2 = new Trainer()
            {
                Id = 2,
                FullName = "Connor2",
                BirthDate = DateTime.Now,
                Description = "Best champ ever2",
                YearOfExperience = 5,
                ImageUrl = "Some2 image",
                UserId = "SomeU2ser",
            };

          
            data.AddRange(trainerBox1, trainerBox2);

            data.SaveChanges();

            //Act

            var result = trainerApiController.GetTrainerById(1);



            //Assert

            var actionResult = Assert.IsType<ActionResult<TrainerViewModel>>(result);

            var currentTrainer = actionResult.Value;

            Assert.Equal("Connor", currentTrainer.FullName);
            Assert.Equal("Best champ ever", currentTrainer.Description);
            Assert.Equal("Some image", currentTrainer.ImageUrl);

        }



    }
}
