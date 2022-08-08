using LionSkyNot.Controllers;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace LionSkyNot.Tests.Controllers
{
    public class GymControllerTest
    {

        [Fact]
        public void IndexOfGymController_ShouldReturnViewResult()
        {
            //Arrange

            var gymController = new GymController();

            //Act

            var result = gymController.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

    }
}
