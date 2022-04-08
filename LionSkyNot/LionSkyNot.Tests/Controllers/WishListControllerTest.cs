using LionSkyNot.Controllers;

using LionSkyNot.Models.Products;

using LionSkyNot.Models.WishLists;

using LionSkyNot.Services.Products;

using LionSkyNot.Services.WishLists;

using LionSkyNot.Tests.Mock;

using Microsoft.AspNetCore.Mvc;

using Xunit;


namespace LionSkyNot.Tests.Controllers
{
    public class WishListControllerTest
    {

        [Fact]
        public void Payment_ShouldReturnCorrectViewResult()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var wishListService = new WishListService(data, productService);

            var wishListController = new WishListController(
                                                            productService,
                                                            wishListService);

            //Act

            var result = wishListController.Payment();

            //Assert

            Assert.IsType<ViewResult>(result);


        }


        [Fact]
        public void Payment_ShouldReturnViewResultAndViewModelSameModel_BecauseOfModelError()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var wishListService = new WishListService(data, productService);

            var wishListController = new WishListController(
                                                            productService,
                                                            wishListService);


            var paymentFormModel = new PaymentFormModel();

            //Act

            wishListController.ModelState.AddModelError("fakeError", "Error");

            var result = wishListController.Payment(paymentFormModel);

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);

            Assert.IsType<PaymentFormModel>(viewModel.Model);

        }


        [Fact]
        public void Payment_ShouldReturnRedirectActionResultWithCorrectActionName()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var wishListService = new WishListService(data, productService);

            var wishListController = new WishListController(
                                                            productService,
                                                            wishListService);


            var paymentFormModel = new PaymentFormModel();

            //Act

            var result = wishListController.Payment(paymentFormModel);

            //Assert

            var viewModel = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("BuyProducts", viewModel.ActionName);

        }
    }
}
