using LionSkyNot.Controllers;

using LionSkyNot.Models.Calculator;

using LionSkyNot.Views.ViewModels.Calculator;

using Microsoft.AspNetCore.Mvc;

using Xunit;


namespace LionSkyNot.Tests.Controllers
{
    public class CalculatorControllerTest
    {

        [Fact]
        public void TestingIndex_ShouldReturnViewResult()
        {

            //Arrange

            var calculatorController = new CalculatorController();

            //Act

            var result = calculatorController.Index();

            //Assert

            Assert.IsType<ViewResult>(result);

        }


        [Fact]
        public void TestingIndex_ShouldReturnViewResultBecauseOfModelStateError()
        {

            //Arrange

            var calculatorController = new CalculatorController();

            //Act

            calculatorController.ModelState.AddModelError("fakeerror", "error");

            var result = calculatorController.Index(null, null);

            //Assert

            Assert.IsType<ViewResult>(result);

        }


        [Fact]
        public void TestingIndex_ShouldReturnViewResultWithModel()
        {

            //Arrange

            var calculatorController = new CalculatorController();
            var calculateFormModel = new CalculateFormModel()
            {
                Age = 20,
                Goal = "Weight Loss",
                Height = 180,
                Weight = 55
            };

            //Act

            var result = calculatorController.Index(calculateFormModel, null);

            //Assert

           var viewModel =  Assert.IsType<ViewResult>(result);

            Assert.IsType<CalculatorViewModel>(viewModel.Model);
            Assert.Equal("Result", viewModel.ViewName);

        }

    }
}
