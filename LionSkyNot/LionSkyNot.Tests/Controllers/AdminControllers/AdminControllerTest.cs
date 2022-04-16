using LionSkyNot.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using Xunit;


namespace LionSkyNot.Tests.Controllers.AdminControllers
{
    public class AdminControllerTest
    {

        [Fact]
        public void Index_ShouldReturnViewResult()
        {

            //Arrange

            var adminController = new AdminController();

            //Act

            var result = adminController.Index();


            //Assert

            Assert.IsType<ViewResult>(result);
        }


    }
}
