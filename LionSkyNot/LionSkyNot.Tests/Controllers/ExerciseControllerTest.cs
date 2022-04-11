using System.Collections.Generic;

using LionSkyNot.Controllers;

using LionSkyNot.Data.Models.Exercise;

using LionSkyNot.Services.Exercises;

using LionSkyNot.Tests.Mock;

using LionSkyNot.Views.ViewModels.Exercises;

using Microsoft.AspNetCore.Mvc;

using Xunit;


namespace LionSkyNot.Tests.Controllers
{
    public class ExerciseControllerTest
    {

        [Fact]
        public void TestingIndexOfExerciseController_ShouldReturnViewResult()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var exerciseService = new ExerciseService(data);
            var exerciseController = new ExerciseController(exerciseService);


            //Act

            var result = exerciseController.Index();



            //Assert

            Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public void BackExerciseAction_ShouldReturnViewResultWithCorrectModel()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var exerciseService = new ExerciseService(data);
            var exerciseController = new ExerciseController(exerciseService);

            var exercise = new Exercise()
            {
                Id = 1,
                Name = "name",
                ImageUrl = "image",
                Description = "Description",
                TypeExercise = new TypeExercise() { Id = 1, TypeName = "Back" }
            };

            data.Exercises.Add(exercise);
            data.SaveChanges();

            var exerciseViewModel = new List<ExerciseViewModel>();

            //Act

            var result = exerciseController.BackExercises(exerciseViewModel);


            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);

            var currentModel = Assert.IsType<List<ExerciseViewModel>>(viewModel.Model);
            Assert.Equal(1, currentModel.Count);
        }


        [Fact]
        public void LegExerciseAction_ShouldReturnViewResultWithCorrectModel()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var exerciseService = new ExerciseService(data);
            var exerciseController = new ExerciseController(exerciseService);

            var exercise = new Exercise()
            {
                Id = 1,
                Name = "name",
                ImageUrl = "image",
                Description = "Description",
                TypeExercise = new TypeExercise() { Id = 1, TypeName = "Legs" }
            };

            data.Exercises.Add(exercise);
            data.SaveChanges();

            var exerciseViewModel = new List<ExerciseViewModel>();

            //Act

            var result = exerciseController.LegExercises(exerciseViewModel);


            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);

            var currentModel = Assert.IsType<List<ExerciseViewModel>>(viewModel.Model);
            Assert.Equal(1, currentModel.Count);
        }


        [Fact]
        public void BicepsExerciseAction_ShouldReturnViewResultWithCorrectModel()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var exerciseService = new ExerciseService(data);
            var exerciseController = new ExerciseController(exerciseService);

            var exercise = new Exercise()
            {
                Id = 1,
                Name = "name",
                ImageUrl = "image",
                Description = "Description",
                TypeExercise = new TypeExercise() { Id = 1, TypeName = "Biceps" }
            };

            data.Exercises.Add(exercise);
            data.SaveChanges();

            var exerciseViewModel = new List<ExerciseViewModel>();

            //Act

            var result = exerciseController.BicepsExercises(exerciseViewModel);


            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);

            var currentModel = Assert.IsType<List<ExerciseViewModel>>(viewModel.Model);
            Assert.Equal(1, currentModel.Count);
        }


        [Fact]
        public void ChestErciseAction_ShouldReturnViewResultWithCorrectModel()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var exerciseService = new ExerciseService(data);
            var exerciseController = new ExerciseController(exerciseService);

            var exercise = new Exercise()
            {
                Id = 1,
                Name = "name",
                ImageUrl = "image",
                Description = "Description",
                TypeExercise = new TypeExercise() { Id = 1, TypeName = "Chest" }
            };

            data.Exercises.Add(exercise);
            data.SaveChanges();

            var exerciseViewModel = new List<ExerciseViewModel>();

            //Act

            var result = exerciseController.ChestExercises(exerciseViewModel);


            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);

            var currentModel = Assert.IsType<List<ExerciseViewModel>>(viewModel.Model);
            Assert.Equal(1, currentModel.Count);
        }

    }
}
