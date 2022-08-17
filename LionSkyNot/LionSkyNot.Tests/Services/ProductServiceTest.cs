using System.Linq;
using System.Threading.Tasks;
using LionSkyNot.Data.Models.Shop;

using LionSkyNot.Services.Products;

using LionSkyNot.Tests.Mock;

using Xunit;


namespace LionSkyNot.Tests.Services
{
    public class ProductServiceTest
    {

        [Fact]
        public async Task CreateProduct_ShouldBeSuccess()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var name = "product";
            var price = 25.4m;
            var inStock = 2;
            var description = "some description tests";
            var img = "img";
            var typeProductId = 1;
            var brandProductId = 1;

            //Act

            await productService.CreateProductAsync(
                                          name,
                                          price,
                                          inStock,
                                          description,
                                          img,
                                          typeProductId,
                                          brandProductId);

            var currentProduct = data.Products
                                     .Where(p => p.Name == name)
                                     .FirstOrDefault();

            //Assert

            Assert.True(data.Products.Any());
            Assert.Equal(name, currentProduct.Name);
            Assert.Equal(price, currentProduct.Price);
            Assert.Equal(description, currentProduct.Description);
            Assert.Equal(img, currentProduct.ImageUrl);

        }



        [Fact]
        public async Task GetProductById_ShouldReturnCorrectProduct()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var name = "product";
            var price = 25.4m;
            var inStock = 2;
            var description = "some description tests";
            var img = "img";
            var typeProductId = 1;
            var brandProductId = 1;

            //Act

            await productService.CreateProductAsync(
                                           name,
                                           price,
                                           inStock,
                                           description,
                                           img,
                                           typeProductId,
                                           brandProductId);

            var currentProductId = data.Products
                                     .Where(p => p.Name == name)
                                     .Select(p => p.Id)
                                     .FirstOrDefault();

            var actualProduct = productService.GetProductById(currentProductId);

            //Assert

            Assert.Equal(name, actualProduct.Name);
            Assert.Equal(img, actualProduct.ImageUrl);

        }


        [Fact]
        public async void EditProduct_ShouldReturnFalse()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var productId = 1;
            var name = "product";
            var price = 25.4m;
            var inStock = 2;
            var description = "some description tests";
            var img = "img";
            var typeProductId = 1;
            var brandProductId = 1;
            var percentage = 2.5f;

            //Act

            var result = await productService.EditProductAsync(
                                                    productId,
                                                    img,
                                                    name,
                                                    price,
                                                    percentage);

            //Assert

            Assert.False(result);

        }


        [Fact]
        public async Task EditProduct_ShouldReturnTrueAndEditTheProduct()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var name = "product";
            var price = 25.4m;
            var inStock = 2;
            var description = "some description tests";
            var img = "img";
            var typeProductId = 1;
            var brandProductId = 1;
            var percentage = 2.5f;

            //Act

            await productService.CreateProductAsync(
                                          name,
                                          price,
                                          inStock,
                                          description,
                                          img,
                                          typeProductId,
                                          brandProductId);

            var currentProduct = data.Products
                                     .Where(p => p.Name == name)
                                     .FirstOrDefault();


            var result = await productService.EditProductAsync(
                                                    currentProduct.Id,
                                                    img,
                                                    name,
                                                    price,
                                                    percentage);

            var expectedPriceOnPromotion = price - (price * (decimal)(percentage / 100));

            //Assert

            Assert.True(result);
            Assert.True(currentProduct.IsOnPromotion);
            Assert.Equal(expectedPriceOnPromotion, currentProduct.PriceOnPromotion);

        }


        [Fact]
        public async Task EditProduct_ShouldReturnTrueButNotOnPromotion()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var name = "product";
            var price = 25.4m;
            var inStock = 2;
            var description = "some description tests";
            var img = "img";
            var typeProductId = 1;
            var brandProductId = 1;
            var percentage = 0;

            //Act

            await productService.CreateProductAsync(
                                          name,
                                          price,
                                          inStock,
                                          description,
                                          img,
                                          typeProductId,
                                          brandProductId);

            var currentProduct = data.Products
                                      .Where(p => p.Name == name)
                                      .FirstOrDefault();


            var result = await productService.EditProductAsync(
                                                    currentProduct.Id,
                                                    img,
                                                    name,
                                                    price,
                                                    percentage);

            //Assert

            Assert.True(result);
            Assert.False(currentProduct.IsOnPromotion);

        }


        [Fact]
        public async Task DeleteProduct_ShouldReturnFalse()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);

            //Assert

            Assert.False(await productService.DeleteProductAsync(1));

        }


        [Fact]
        public async Task DeleteProduct_ShouldReturnTrueAndDelete()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var name = "product";
            var price = 25.4m;
            var inStock = 2;
            var description = "some description tests";
            var img = "img";
            var typeProductId = 1;
            var brandProductId = 1;
            var percentage = 2.5f;

            //Act

            await productService.CreateProductAsync(
                                          name,
                                          price,
                                          inStock,
                                          description,
                                          img,
                                          typeProductId,
                                          brandProductId);

            var currentProduct = data.Products
                                      .Where(p => p.Name == name)
                                      .FirstOrDefault();


            var result = await productService.DeleteProductAsync(currentProduct.Id);

            //Assert

            Assert.True(result);
            Assert.True(currentProduct.IsDeleted);

        }


        [Fact]
        public void GetAllBrandProducts_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var brandName = "Universal";
            var brandId = 1;

            //Act

            var brand = new Brand()
            {
                Id = brandId,
                BrandName = brandName
            };

            data.Brands.Add(brand);
            data.SaveChanges();

            var brands = productService.GetAllBrandProduct();


            //Assert

            Assert.Equal(brandId, brands.Count());
            Assert.Equal(brandName, brands.First().Name);

        }


        [Fact]
        public void GetAllTypeProduct_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var typeName = "Protein";
            var typeId = 1;

            //Act

            var typeProduct = new Type()
            {
                Id = typeId,
                TypeName = typeName
            };

            data.Types.Add(typeProduct);
            data.SaveChanges();

            var types = productService.GetAllTypesProduct();


            //Assert

            Assert.Equal(typeId, types.Count());
            Assert.Equal(typeName, types.First().Name);

        }


        [Fact]
        public void GetProductByBrandAndType_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var brandName = "Universal";
            var brandId = 1;
            var typeName = "Protein";
            var typeId = 1;

            //Act

            var brand = new Brand()
            {
                Id = brandId,
                BrandName = brandName
            };

            var typeProduct = new Type()
            {
                Id = typeId,
                TypeName = typeName
            };


            var product = new Product()
            {
                Id = 1,
                Brand = brand,
                Type = typeProduct,
                Name = "someName",
                Price = 2.5m,
                ImageUrl = "someimg",
                Description = "somedescript"
            };

            data.Products.Add(product);
            data.SaveChanges();

            var productByTypeAndBrand = productService.GetProductsByBrandAndType(typeName, brandName);


            //Assert

            Assert.Equal(1, productByTypeAndBrand.Count());
            Assert.Equal("someName", productByTypeAndBrand.First().Name);

        }


        [Fact]
        public void ShowAllProducts_ShouldBeCorrectReturn()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var productName = "someName";
            var description = "some Description";
            var price = 2.5m;
            var image = "someImg";
            var brandName = "Universal";
            var brandId = 1;
            var typeName = "Protein";
            var typeId = 1;

            //Act

            var brand = new Brand()
            {
                Id = brandId,
                BrandName = brandName
            };

            var typeProduct = new Type()
            {
                Id = typeId,
                TypeName = typeName
            };


            var product = new Product()
            {
                Id = 1,
                Brand = brand,
                Type = typeProduct,
                Name = productName,
                Price = price,
                ImageUrl = image,
                Description = description
            };

            var product2 = new Product()
            {
                Id = 2,
                Brand = brand,
                Type = typeProduct,
                Name = productName + " er",
                Price = price + 5,
                ImageUrl = image + "some",
                Description = description + "en"
            };

            data.Products.AddRange(product, product2);
            data.SaveChanges();

            var allProducts = productService.ShowAllProducts();


            //Assert

            Assert.Equal(2, allProducts.Count());
            Assert.Equal(productName, allProducts.First().Name);
            Assert.Equal(productName + " er", allProducts.Last().Name);

        }


        [Fact]
        public void GetAllProductsForAdmin_ShouldBeCorrectReturning()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var productName = "someName";
            var description = "some Description";
            var price = 2.5m;
            var image = "someImg";
            var brandName = "Universal";
            var brandId = 1;
            var typeName = "Protein";
            var typeId = 1;

            //Act

            var brand = new Brand()
            {
                Id = brandId,
                BrandName = brandName
            };

            var typeProduct = new Type()
            {
                Id = typeId,
                TypeName = typeName
            };


            var product = new Product()
            {
                Id = 1,
                Brand = brand,
                Type = typeProduct,
                Name = productName,
                Price = price,
                ImageUrl = image,
                Description = description
            };

            data.Products.Add(product);
            data.SaveChanges();

            var productsForAdmin = productService.GetAllProductsForAdmin();


            //Assert

            Assert.Equal(1, productsForAdmin.Count());
            Assert.Equal(productName, productsForAdmin.First().Name);

        }


        [Fact]
        public void SortedByPriceDescending_ShouldBeCorrectSorted()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var productName = "someName";
            var description = "some Description";
            var price = 2.5m;
            var image = "someImg";
            var brandName = "Universal";
            var brandId = 1;
            var typeName = "Protein";
            var typeId = 1;

            //Act

            var brand = new Brand()
            {
                Id = brandId,
                BrandName = brandName
            };

            var typeProduct = new Type()
            {
                Id = typeId,
                TypeName = typeName
            };


            var product = new Product()
            {
                Id = 1,
                Brand = brand,
                Type = typeProduct,
                Name = productName,
                Price = price,
                ImageUrl = image,
                Description = description
            };

            var product2 = new Product()
            {
                Id = 2,
                Brand = brand,
                Type = typeProduct,
                Name = productName,
                Price = 50m,
                ImageUrl = image,
                Description = description
            };

            data.Products.AddRange(product, product2);
            data.SaveChanges();

            var productsForSorting = productService.GetProductsByBrandAndType(typeName, brandName);

            var sortedProducts = productService.SortedByPriceDescending(productsForSorting);


            //Assert

            Assert.Equal(2, sortedProducts.First().Id);
            Assert.Equal(1, sortedProducts.Last().Id);

        }


        [Fact]
        public void SortedByPrice_ShouldBeCorrectSorted()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var productName = "someName";
            var description = "some Description";
            var price = 2.5m;
            var image = "someImg";
            var brandName = "Universal";
            var brandId = 1;
            var typeName = "Protein";
            var typeId = 1;

            //Act

            var brand = new Brand()
            {
                Id = brandId,
                BrandName = brandName
            };

            var typeProduct = new Type()
            {
                Id = typeId,
                TypeName = typeName
            };


            var product = new Product()
            {
                Id = 1,
                Brand = brand,
                Type = typeProduct,
                Name = productName,
                Price = price,
                ImageUrl = image,
                Description = description
            };

            var product2 = new Product()
            {
                Id = 2,
                Brand = brand,
                Type = typeProduct,
                Name = productName,
                Price = 50m,
                ImageUrl = image,
                Description = description
            };

            data.Products.AddRange(product, product2);
            data.SaveChanges();

            var productsForSorting = productService.GetProductsByBrandAndType(typeName, brandName);

            var sortedProducts = productService.SortedByPrice(productsForSorting);


            //Assert

            Assert.Equal(1, sortedProducts.First().Id);
            Assert.Equal(2, sortedProducts.Last().Id);

        }


        [Fact]
        public void SortedByName_ShouldBeCorrectSorted()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var productName = "someName";
            var description = "some Description";
            var price = 2.5m;
            var image = "someImg";
            var brandName = "Universal";
            var brandId = 1;
            var typeName = "Protein";
            var typeId = 1;

            //Act

            var brand = new Brand()
            {
                Id = brandId,
                BrandName = brandName
            };

            var typeProduct = new Type()
            {
                Id = typeId,
                TypeName = typeName
            };


            var product = new Product()
            {
                Id = 1,
                Brand = brand,
                Type = typeProduct,
                Name = "a" + productName,
                Price = price,
                ImageUrl = image,
                Description = description
            };

            var product2 = new Product()
            {
                Id = 2,
                Brand = brand,
                Type = typeProduct,
                Name = "b" + productName,
                Price = 50m,
                ImageUrl = image,
                Description = description
            };

            data.Products.AddRange(product, product2);
            data.SaveChanges();

            var productsForSorting = productService.GetProductsByBrandAndType(typeName, brandName);

            var sortedProducts = productService.SortedByName(productsForSorting);


            //Assert

            Assert.Equal(1, sortedProducts.First().Id);
            Assert.Equal(2, sortedProducts.Last().Id);

        }


        [Fact]
        public void SortedByCountOfBuys_ShouldBeCorrectSorted()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var productName = "someName";
            var description = "some Description";
            var price = 2.5m;
            var image = "someImg";
            var brandName = "Universal";
            var brandId = 1;
            var typeName = "Protein";
            var typeId = 1;

            //Act

            var brand = new Brand()
            {
                Id = brandId,
                BrandName = brandName
            };

            var typeProduct = new Type()
            {
                Id = typeId,
                TypeName = typeName
            };


            var product = new Product()
            {
                Id = 1,
                Brand = brand,
                Type = typeProduct,
                Name = productName,
                Price = price,
                ImageUrl = image,
                Description = description,
                CountOfBuys = 3
            };

            var product2 = new Product()
            {
                Id = 2,
                Brand = brand,
                Type = typeProduct,
                Name = productName,
                Price = 50m,
                ImageUrl = image,
                Description = description,
                CountOfBuys = 5
            };

            data.Products.AddRange(product, product2);
            data.SaveChanges();

            var productsForSorting = productService.GetProductsByBrandAndType(typeName, brandName);

            var sortedProducts = productService.SortedByMostBuys(productsForSorting);


            //Assert

            Assert.Equal(2, sortedProducts.First().Id);
            Assert.Equal(1, sortedProducts.Last().Id);

        }


        [Fact]
        public void GetFinalProductsSelected_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var productName = "someName";
            var description = "some Description";
            var price = 2.5m;
            var image = "someImg";
            var brandName = "Universal";
            var brandId = 1;
            var typeName = "Protein";
            var typeId = 1;

            //Act

            var brand = new Brand()
            {
                Id = brandId,
                BrandName = brandName
            };

            var typeProduct = new Type()
            {
                Id = typeId,
                TypeName = typeName
            };


            var product = new Product()
            {
                Id = 1,
                Brand = brand,
                Type = typeProduct,
                Name = productName,
                Price = price,
                ImageUrl = image,
                Description = description,
                CountOfBuys = 3,
                CountInStock = 5,
                PriceOnPromotion = 0,
                IsOnPromotion = false
            };

            var product2 = new Product()
            {
                Id = 2,
                Brand = brand,
                Type = typeProduct,
                Name = productName,
                Price = 50m,
                ImageUrl = image,
                Description = description,
                CountOfBuys = 5,
                CountInStock = 6,
                PriceOnPromotion = 0,
                IsOnPromotion = false
            };

            data.Products.AddRange(product, product2);
            data.SaveChanges();

            var products = productService.GetProductsByBrandAndType(typeName, brandName);

            var getFinalProducts = productService.GetFinalProductsSelected(products);

            //Assert

            Assert.Equal(2, getFinalProducts.Count());
            Assert.Equal(1, getFinalProducts.First().Id);
            Assert.Equal(2, getFinalProducts.Last().Id);

        }



        [Fact]
        public void GetProductsOnPromotion_ShouldReturnOnlyOnPromotionProducts()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var productName = "someName";
            var description = "some Description";
            var price = 2.5m;
            var image = "someImg";
            var brandName = "Universal";
            var brandId = 1;
            var typeName = "Protein";
            var typeId = 1;

            //Act

            var brand = new Brand()
            {
                Id = brandId,
                BrandName = brandName
            };

            var typeProduct = new Type()
            {
                Id = typeId,
                TypeName = typeName
            };


            var product = new Product()
            {
                Id = 1,
                Brand = brand,
                Type = typeProduct,
                Name = productName,
                Price = price,
                ImageUrl = image,
                Description = description,
                CountOfBuys = 3,
                CountInStock = 5,
                PriceOnPromotion = 5,
                IsOnPromotion = true
            };

            var product2 = new Product()
            {
                Id = 2,
                Brand = brand,
                Type = typeProduct,
                Name = productName,
                Price = 50m,
                ImageUrl = image,
                Description = description,
                CountOfBuys = 5,
                CountInStock = 6,
                PriceOnPromotion = 0,
                IsOnPromotion = false
            };

            data.Products.AddRange(product, product2);
            data.SaveChanges();

            var productsOnPromotion = productService.GetAllProductsOnPromotion();

            //Assert

            Assert.Equal(1, productsOnPromotion.Count());
            Assert.Equal(1, productsOnPromotion.First().Id);

        }


        [Fact]
        public async Task UpdateInStockCountOfProductsWithZeroInStock_ShouldReturnFalse()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);

            //Assert

            Assert.False(await productService.UpdateInStockCountOfProductsAsync());

        }


        [Fact]
        public async Task UpdateInStockCountOfProductsWithZeroInStock_ShouldReturnTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var productName = "someName";
            var description = "some Description";
            var price = 2.5m;
            var image = "someImg";
            var brandName = "Universal";
            var brandId = 1;
            var typeName = "Protein";
            var typeId = 1;

            //Act

            var brand = new Brand()
            {
                Id = brandId,
                BrandName = brandName
            };

            var typeProduct = new Type()
            {
                Id = typeId,
                TypeName = typeName
            };


            var product = new Product()
            {
                Id = 1,
                Brand = brand,
                Type = typeProduct,
                Name = productName,
                Price = price,
                ImageUrl = image,
                Description = description,
                CountOfBuys = 3,
                CountInStock = 0,
                PriceOnPromotion = 0,
                IsOnPromotion = false
            };

            var product2 = new Product()
            {
                Id = 2,
                Brand = brand,
                Type = typeProduct,
                Name = productName,
                Price = 50m,
                ImageUrl = image,
                Description = description,
                CountOfBuys = 5,
                CountInStock = 6,
                PriceOnPromotion = 0,
                IsOnPromotion = false
            };

            data.Products.AddRange(product, product2);
            data.SaveChanges();

            var result = await productService.UpdateInStockCountOfProductsAsync();

            //Assert

            Assert.True(result);
            Assert.Equal(10, product.CountInStock);
            Assert.Equal(6, product2.CountInStock);

        }


        [Fact]
        public void TakeProductById_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var productName = "someName";
            var description = "some Description";
            var price = 2.5m;
            var image = "someImg";
            var brandName = "Universal";
            var brandId = 1;
            var typeName = "Protein";
            var typeId = 1;

            //Act

            var brand = new Brand()
            {
                Id = brandId,
                BrandName = brandName
            };

            var typeProduct = new Type()
            {
                Id = typeId,
                TypeName = typeName
            };


            var product = new Product()
            {
                Id = 1,
                Brand = brand,
                Type = typeProduct,
                Name = productName,
                Price = price,
                ImageUrl = image,
                Description = description,
            };



            data.Products.Add(product);
            data.SaveChanges();


            var currentProduct = productService.TakeProduct(1);

            //Assert

            Assert.Equal(1, currentProduct.Id);
            Assert.Equal(productName, currentProduct.Name);

        }


        [Fact]
        public void GetProductForDetailsById_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var productService = new ProductService(data);
            var productName = "someName";
            var description = "some Description";
            var price = 2.5m;
            var image = "someImg";
            var brandName = "Universal";
            var brandId = 1;
            var typeName = "Protein";
            var typeId = 1;

            //Act

            var brand = new Brand()
            {
                Id = brandId,
                BrandName = brandName
            };

            var typeProduct = new Type()
            {
                Id = typeId,
                TypeName = typeName
            };


            var product = new Product()
            {
                Id = 1,
                Brand = brand,
                Type = typeProduct,
                Name = productName,
                Price = price,
                ImageUrl = image,
                Description = description,
                CountOfBuys = 3,
                CountInStock = 5,
                PriceOnPromotion = 0,
                IsOnPromotion = false
            };

            data.Products.Add(product);
            data.SaveChanges();

            var detailProduct = productService.GetProductForDetails(1);


            //Assert

            Assert.NotNull(detailProduct);
            Assert.Equal(productName, detailProduct.Name);

        }
    }
}
