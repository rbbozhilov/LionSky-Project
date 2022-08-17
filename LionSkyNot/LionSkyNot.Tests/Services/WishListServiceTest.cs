using System.Linq;
using System.Threading.Tasks;
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
        public async Task AddWishListWithProduct_ShouldReturnFalse_ProductIsNull()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var wishListService = new WishListService(data, productService);

            //Assert

            Assert.False(await wishListService.AddAsync(null, null));

        }


        [Fact]
        public async Task AddWishListWithProduct_ShouldReturnTrue_CreateNewWishList()
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

            var result = await wishListService.AddAsync(product, "userid");

            //Assert

            Assert.True(result);
            Assert.True(data.WishListsProducts.Any());

        }


        [Fact]
        public async Task AddWishListWithProduct_ShouldReturnTrue_AddProductToCurrentWishList()
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

            await wishListService.AddAsync(product, "userid");
            await wishListService.AddAsync(product2, "userid");

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
        public async Task GetProductsOfUser_ShouldReturnTrueAndProductsOfCurrentUser()
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

            await wishListService.AddAsync(product, "userid");
            await wishListService.AddAsync(product2, "userid");

            var result = wishListService.GetProductsOfUser("userid");

            var isTaken = result.Item1;
            var products = result.Item2;


            //Assert

            Assert.True(isTaken);
            Assert.Equal(1, products.Products.First().Id);
            Assert.Equal(2, products.Products.Last().Id);

        }


        [Fact]
        public async Task RemoveProduct_ShouldReturnFalse_WishListNull()
        {

            //Arrange
            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var wishListService = new WishListService(data, productService);


            //Assert

            Assert.False(await wishListService.RemoveProductAsync(1, "someuser"));

        }


        [Fact]
        public async Task RemoveProduct_ShouldReturnFalse_ProductNull()
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

            await wishListService.AddAsync(product, "userid");

            //Assert

            Assert.False(await wishListService.RemoveProductAsync(55, "userid"));

        }


        [Fact]
        public async Task RemoveProduct_ShouldReturnTrue_AndRemoveProduct()
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

            await wishListService.AddAsync(product, "userid");

            var result = await wishListService.RemoveProductAsync(1, "userid");

            //Assert

            Assert.True(result);

        }


        [Fact]
        public async Task BuyProducts_ShouldReturnFalse_WishListProductsZero()
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

            await wishListService.AddAsync(product, "userid");

            await wishListService.RemoveProductAsync(1, "userid");

            var result = wishListService.BuyProducts("userid");

            //Assert

            Assert.False(result.Item1);
            Assert.Null(result.Item2);

        }


        [Fact]
        public async Task BuyProducts_ShouldReturnFalse_CountOfProductInStockIsZero()
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

           await wishListService.AddAsync(product, "userid");


            var result = wishListService.BuyProducts("userid");


            //Assert

            Assert.False(result.Item1);

        }


        [Fact]
        public async Task BuyProducts_ShouldReturnTrue_AndProductsBuy()
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

            await wishListService.AddAsync(product, "userid");

            var result = wishListService.BuyProducts("userid");
            var isSuccess = result.Item1;
            var products = result.Item2;

            //Assert

            Assert.True(isSuccess);
            Assert.Equal("name", products.First().Name);

        }

    }
}
