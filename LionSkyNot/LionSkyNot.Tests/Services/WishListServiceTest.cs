using System.Linq;

using LionSkyNot.Data.Models.Shop;

using LionSkyNot.Services.Products;

using LionSkyNot.Services.WishLists;

using LionSkyNot.Tests.Mock;

using Xunit;


namespace LionSkyNot.Tests.Services
{
    public class WishListServiceTest
    {


        [Fact]
        public void AddWishListWithProduct_ShouldReturnFalse_ProductIsNull()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var wishListService = new WishListService(data, productService);

            //Assert

            Assert.False(wishListService.Add(null, null));

        }


        [Fact]
        public void AddWishListWithProduct_ShouldReturnTrue_CreateNewWishList()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var wishListService = new WishListService(data, productService);

            var product = new Product()
            {
                Id = 1,
                Name = "name",
                Description = "Description",
                ImageUrl = "Image",
                Price = 25m,
            };

            //Act

            var result = wishListService.Add(product, "userid");

            //Assert

            Assert.True(result);
            Assert.True(data.WishListsProducts.Any());

        }


        [Fact]
        public void AddWishListWithProduct_ShouldReturnTrue_AddProductToCurrentWishList()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var wishListService = new WishListService(data, productService);

            var product = new Product()
            {
                Id = 1,
                Name = "name",
                Description = "Description",
                ImageUrl = "Image",
                Price = 25m,
            };

            var product2 = new Product()
            {
                Id = 2,
                Name = "name1",
                Description = "Description1",
                ImageUrl = "Image1",
                Price = 255m,
            };

            //Act

            wishListService.Add(product, "userid");
            wishListService.Add(product2, "userid");

            var wishList = data.WishListsProducts
                               .Where(w => w.ProductId == 1)
                               .FirstOrDefault();


            //Assert

            Assert.Equal(2, wishList.WishList.Products.Count());

        }


        [Fact]
        public void GetProductsOfUser_ShouldReturnFalse_WishListIsNull()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var wishListService = new WishListService(data, productService);


            //Act

            var result = wishListService.GetProductsOfUser("someid");

            //Assert

            Assert.False(result.Item1);
            Assert.Null(result.Item2);

        }


        [Fact]
        public void GetProductsOfUser_ShouldReturnTrueAndProductsOfCurrentUser()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var wishListService = new WishListService(data, productService);

            var product = new Product()
            {
                Id = 1,
                Name = "name",
                Description = "Description",
                ImageUrl = "Image",
                Price = 25m,
            };

            var product2 = new Product()
            {
                Id = 2,
                Name = "name1",
                Description = "Description1",
                ImageUrl = "Image1",
                Price = 255m,
            };

            //Act

            wishListService.Add(product, "userid");
            wishListService.Add(product2, "userid");

            var result = wishListService.GetProductsOfUser("userid");

            var isTaken = result.Item1;
            var products = result.Item2;


            //Assert

            Assert.True(isTaken);
            Assert.Equal(1, products.Products.First().Id);
            Assert.Equal(2, products.Products.Last().Id);

        }


        [Fact]
        public void RemoveProduct_ShouldReturnFalse_WishListNull()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var wishListService = new WishListService(data, productService);


            //Assert

            Assert.False(wishListService.RemoveProduct(1, "someuser"));

        }


        [Fact]
        public void RemoveProduct_ShouldReturnFalse_ProductNull()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var wishListService = new WishListService(data, productService);

            var product = new Product()
            {
                Id = 1,
                Name = "name",
                Description = "Description",
                ImageUrl = "Image",
                Price = 25m,
            };

            //Act

            wishListService.Add(product, "userid");

            //Assert

            Assert.False(wishListService.RemoveProduct(55, "userid"));

        }


        [Fact]
        public void RemoveProduct_ShouldReturnTrue_AndRemoveProduct()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var wishListService = new WishListService(data, productService);

            var product = new Product()
            {
                Id = 1,
                Name = "name",
                Description = "Description",
                ImageUrl = "Image",
                Price = 25m,
            };

            //Act

            wishListService.Add(product, "userid");

            var result = wishListService.RemoveProduct(1, "userid");

            //Assert

            Assert.True(result);

        }


        [Fact]
        public void BuyProducts_ShouldReturnFalse_WishListProductsZero()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var wishListService = new WishListService(data, productService);

            var product = new Product()
            {
                Id = 1,
                Name = "name",
                Description = "Description",
                ImageUrl = "Image",
                Price = 25m,
            };

            //Act

            wishListService.Add(product, "userid");

            wishListService.RemoveProduct(1, "userid");

            var result = wishListService.BuyProducts("userid");

            //Assert

            Assert.False(result.Item1);
            Assert.Null(result.Item2);

        }


        [Fact]
        public void BuyProducts_ShouldReturnFalse_CountOfProductInStockIsZero()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var wishListService = new WishListService(data, productService);

            var product = new Product()
            {
                Id = 1,
                Name = "name",
                Description = "Description",
                ImageUrl = "Image",
                Price = 25m,
            };

            //Act

            wishListService.Add(product, "userid");


            var result = wishListService.BuyProducts("userid");


            //Assert

            Assert.False(result.Item1);

        }


        [Fact]
        public void BuyProducts_ShouldReturnTrue_AndProductsBuy()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var wishListService = new WishListService(data, productService);

            var product = new Product()
            {
                Id = 1,
                Name = "name",
                Description = "Description",
                ImageUrl = "Image",
                Price = 25m,
                CountInStock = 1
            };

            //Act

            wishListService.Add(product, "userid");

            var result = wishListService.BuyProducts("userid");
            var isSuccess = result.Item1;
            var products = result.Item2;

            //Assert

            Assert.True(isSuccess);
            Assert.Equal("name",products.First().Name);

        }

    }
}
