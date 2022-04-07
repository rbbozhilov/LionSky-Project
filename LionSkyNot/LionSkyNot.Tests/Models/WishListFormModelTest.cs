

using LionSkyNot.Models.Products;
using LionSkyNot.Models.WishLists;
using LionSkyNot.Tests.Mock;
using System.Collections.Generic;
using Xunit;

namespace LionSkyNot.Tests.Models
{
    public class WishListFormModelTest
    {

        [Fact]
        public void TotalPriceForWishList_ShouldBeCorrectWithOutOnPromotionProducts()
        {

            //Arrange

            var product1 = new ProductWishListFormModel()
            {
                Id = 1,
                Name = "some name",
                Price = 25m,
                IsOnPromotion = false
            };

            var product2 = new ProductWishListFormModel()
            {
                Id = 1,
                Name = "some name",
                Price = 25m,
                IsOnPromotion = false
            };

            var product3 = new ProductWishListFormModel()
            {
                Id = 1,
                Name = "some name",
                Price = 25m,
                IsOnPromotion = false
            };

            //Act

            var allProducts = new List<ProductWishListFormModel>() { product1, product2, product3 };

            var wishListFormModel = new WishListFormModel()
            {
                Products = allProducts
            };

            //Assert
            Assert.Equal(75m, wishListFormModel.Price);


        }


        [Fact]
        public void TotalPriceForWishList_ShouldBeCorrectWithOnPromotionProducts()
        {

            //Arrange

            var product1 = new ProductWishListFormModel()
            {
                Id = 1,
                Name = "some name",
                Price = 25m,
                IsOnPromotion = true,
                PriceOnPromotion = 10
            };

            var product2 = new ProductWishListFormModel()
            {
                Id = 1,
                Name = "some name",
                Price = 25m,
                IsOnPromotion = true,
                PriceOnPromotion = 20
            };

            var product3 = new ProductWishListFormModel()
            {
                Id = 1,
                Name = "some name",
                Price = 25m,
                IsOnPromotion = false
            };

            //Act

            var allProducts = new List<ProductWishListFormModel>() { product1, product2, product3 };

            var wishListFormModel = new WishListFormModel()
            {
                Products = allProducts
            };

            //Assert
            Assert.Equal(55m, wishListFormModel.Price);


        }

    }
}
