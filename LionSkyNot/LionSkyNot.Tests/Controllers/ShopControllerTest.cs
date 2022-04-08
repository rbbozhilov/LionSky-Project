using LionSkyNot.Controllers;
using LionSkyNot.Data.Models.Shop;
using LionSkyNot.Services.Products;
using LionSkyNot.Tests.Mock;
using LionSkyNot.Views.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LionSkyNot.Tests.Controllers
{
    public class ShopControllerTest
    {

        [Fact]
        public void Index_ShouldReturnCorrectViewResultWithCorrectBrandAndType()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);

            var shopController = new ShopController(productService);

            var brand = new Brand()
            {
                Id = 1,
                BrandName = "Universal"
            };

            var type = new Type()
            {
                Id = 1,
                TypeName = "Protein"
            };

            data.Brands.Add(brand);
            data.Types.Add(type);
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

            var brand = new Brand()
            {
                Id = 1,
                BrandName = "Universal"
            };

            var type = new Type()
            {
                Id = 1,
                TypeName = "Protein"
            };

            var product = new Product()
            {
                Type = type,
                Brand = brand,
                Id = 1,
                Description = "description",
                ImageUrl = "Imageurl",
                Name = "name",
                Price = 25m,

            };

            data.Brands.Add(brand);
            data.Types.Add(type);
            data.Products.Add(product);
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


    }
}
