using System.Collections.Generic;

using System.Linq;

using LionSkyNot.Controllers;

using LionSkyNot.Data.Models.Shop;

using LionSkyNot.Services.Products;

using LionSkyNot.Tests.Mock;

using LionSkyNot.Views.ViewModels.Products;

using Microsoft.AspNetCore.Mvc;

using Xunit;


namespace LionSkyNot.Tests.Controllers
{
    public class ShopControllerTest
    {

        private Brand brand;
        private Type type;
        private Product product;
        private Product product2;


        public ShopControllerTest()
        {
            this.brand = new Brand()
            {
                Id = 1,
                BrandName = "Universal"
            };

            this.type = new Type()
            {
                Id = 1,
                TypeName = "Protein"
            };

            this.product = new Product()
            {
                Type = type,
                Brand = brand,
                Id = 1,
                Description = "description",
                ImageUrl = "Imageurl",
                Name = "name",
                Price = 25m,
                CountOfBuys = 10,
                IsOnPromotion = true
            };

            this.product2 = new Product()
            {
                Type = type,
                Brand = brand,
                Id = 2,
                Description = "description1",
                ImageUrl = "Imageurl1",
                Name = "name1",
                Price = 255m,
                CountOfBuys = 5,
                IsOnPromotion = false
            };
        }


        [Fact]
        public void Index_ShouldReturnCorrectViewResultWithCorrectBrandAndType()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);

            var shopController = new ShopController(productService);

        
            data.Brands.Add(this.brand);
            data.Types.Add(this.type);
            data.SaveChanges();


            //Act

            var result = shopController.Index(new AllProductsViewModel());

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var allBrandsAndTypesModel = Assert.IsType<AllProductsViewModel>(viewModel.Model);

            Assert.True(allBrandsAndTypesModel.Brands.Any());
            Assert.True(allBrandsAndTypesModel.Types.Any());

        }


        [Fact]
        public void Index_ShouldReturnCorrectViewResultAndCorrectProducts()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);

            var shopController = new ShopController(productService);

          
            data.Brands.Add(this.brand);
            data.Types.Add(this.type);
            data.Products.Add(this.product);
            data.SaveChanges();

            var currentQuery = new AllProductsViewModel()
            {
                Brand = brand.BrandName,
                Type = type.TypeName,
            };

            //Act

            var result = shopController.Index(currentQuery);

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var finalProducts = Assert.IsType<List<ProductListViewModel>>(viewModel.Model);

            Assert.True(finalProducts.Any());

        }


        [Fact]
        public void ViewDetails_ShouldReturnCorrectViewResultAndViewModel()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);

            var shopController = new ShopController(productService);

            data.Brands.Add(this.brand);
            data.Types.Add(this.type);
            data.Products.Add(this.product);
            data.SaveChanges();



            //Act

            var result = shopController.ViewDetails(1);

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var currentProductDetails = Assert.IsType<ProductDetailViewModel>(viewModel.Model);

            Assert.Equal("name", currentProductDetails.Name);

        }


        [Fact]
        public void AllProducts_ShouldReturnAllProductsCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);

            var shopController = new ShopController(productService);

            data.Brands.Add(this.brand);
            data.Types.Add(this.type);
            data.Products.AddRange(product, product2);
            data.SaveChanges();



            //Act

            var result = shopController.AllProducts();

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var allProducts = Assert.IsType<List<ProductListViewModel>>(viewModel.Model);

            Assert.Equal(2, allProducts.Count());
            Assert.Equal(1, allProducts.First().Id);
            Assert.Equal(2, allProducts.Last().Id);

        }


        [Fact]
        public void ShowMostBuyedProducts_ShouldReturnMostBuyedProductsCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);

            var shopController = new ShopController(productService);

            data.Brands.Add(this.brand);
            data.Types.Add(this.type);
            data.Products.AddRange(this.product, this.product2);
            data.SaveChanges();



            //Act

            var result = shopController.MostBuyedProducts();

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var allProducts = Assert.IsType<List<ProductListViewModel>>(viewModel.Model);

            Assert.Equal(1, allProducts.First().Id);
            Assert.Equal(2, allProducts.Last().Id);

        }


        [Fact]
        public void GetAllProductsOnPromotion_ShouldReturnOnlyPorductsOnPromotion()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);

            var shopController = new ShopController(productService);

            data.Brands.Add(this.brand);
            data.Types.Add(this.type);
            data.Products.AddRange(this.product, this.product2);
            data.SaveChanges();

            //Act

            var result = shopController.SaleProducts();

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var allProducts = Assert.IsType<List<ProductListViewModel>>(viewModel.Model);

            Assert.Equal(1, allProducts.Count());
            Assert.Equal(1, allProducts.First().Id);

        }
    }
}
