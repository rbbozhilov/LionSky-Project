using LionSkyNot.Data;

using LionSkyNot.Data.Models.Shop;

using LionSkyNot.Models.Products;

using LionSkyNot.Views.ViewModels.Products;


namespace LionSkyNot.Services.Products
{
    public class ProductService : IProductService
    {

        private LionSkyDbContext data;

        public ProductService(LionSkyDbContext data)
        {
            this.data = data;
        }



        public void CreateProduct(
                                  string name,
                                  decimal price,
                                  int inStock,
                                  string description,
                                  string imgUrl,
                                  int typeProductId,
                                  int brandProductId)
        {
            var newProduct = new Product()
            {
                Name = name,
                Description = description,
                ImageUrl = imgUrl,
                Price = price,
                TypeId = typeProductId,
                BrandId = brandProductId,
                CountInStock = inStock
            };

            data.Products.Add(newProduct);

            data.SaveChanges();
        }


        public EditProductFormModel GetProductById(int id)
        => this.data.Products
                    .Where(p => p.Id == id && p.IsDeleted == false)
                    .Select(p => new EditProductFormModel()
                    {
                        ImageUrl = p.ImageUrl,
                        Name = p.Name,
                        Price = p.Price
                    })
                    .FirstOrDefault();


        public bool EditProduct(
                                int id,
                                string imageUrl,
                                string name,
                                decimal price,
                                float percentage)
        {
            var currentProduct = this.data.Products
                                          .Where(p => p.Id == id && p.IsDeleted == false)
                                          .FirstOrDefault();

            if (currentProduct == null)
            {
                return false;
            }


            if (percentage > 0)
            {
                currentProduct.IsOnPromotion = true;
            }
            else
            {
                currentProduct.IsOnPromotion = false;
            }



            currentProduct.ImageUrl = imageUrl;
            currentProduct.Name = name;
            currentProduct.Price = price;

            currentProduct.PriceOnPromotion = currentProduct.Price - (currentProduct.Price * (decimal)(percentage / 100));

            this.data.SaveChanges();

            return true;
        }


        public bool IsHaveBrand(int brandId)
        => this.data.Brands.Any(t => t.Id == brandId);


        public bool IsHaveType(int typeId)
        => this.data.Types.Any(t => t.Id == typeId);


        public bool DeleteProduct(int id)
        {
            var currentProduct = this.data.Products
                                          .Where(p => p.Id == id && p.IsDeleted == false)
                                          .FirstOrDefault();

            if (currentProduct == null)
            {
                return false;
            }

            currentProduct.IsDeleted = true;

            this.data.SaveChanges();

            return true;
        }


        public IEnumerable<ProductBrandViewModel> GetAllBrandProduct()
        => this.data.Brands
                    .Select(b => new ProductBrandViewModel
                    {
                        Id = b.Id,
                        Name = b.BrandName
                    }).ToList();


        public IEnumerable<ProductTypeViewModel> GetAllTypesProduct()
        => this.data.Types
                    .Select(t => new ProductTypeViewModel
                    {
                        Id = t.Id,
                        Name = t.TypeName
                    }).ToList();


        public IQueryable<Product> GetProductsByBrandAndType(string type, string brand)
        => this.data.Products
                    .Where(p => p.Brand.BrandName == brand && p.Type.TypeName == type && p.IsDeleted == false)
                    .Distinct();


        public IEnumerable<ProductListViewModel> ShowAllProducts()
        => this.data.Products
                    .Where(p => p.IsDeleted == false)
                    .Select(p => new ProductListViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Type = p.Type.TypeName,
                        Brand = p.Brand.BrandName,
                        Price = p.Price,
                        Description = p.Description,
                        ImageUrl = p.ImageUrl,
                        InStock = p.CountInStock,
                        PriceOnPromotion = p.PriceOnPromotion,
                        IsOnPromotion = p.IsOnPromotion
                    })
                    .Distinct()
                    .ToList();


        public IEnumerable<ProductServiceModel> GetAllProductsForAdmin()
        => this.data.Products
                       .Where(p => p.IsDeleted == false)
                       .Select(p => new ProductServiceModel()
                       {
                           Id = p.Id,
                           ImageUrl = p.ImageUrl,
                           Name = p.Name,
                           Price = p.Price
                       })
                       .ToList();


        public IQueryable<Product> SortedByPriceDescending(IQueryable<Product> products)
        => products.Distinct()
                   .OrderByDescending(p => p.Price);



        public IQueryable<Product> SortedByPrice(IQueryable<Product> products)
        => products.Distinct()
                   .OrderBy(p => p.Price);



        public IQueryable<Product> SortedByName(IQueryable<Product> products)
        => products.Distinct()
                   .OrderBy(p => p.Name);



        public IQueryable<Product> SortedByMostBuys(IQueryable<Product> products)
        => products.Distinct()
                   .OrderByDescending(p => p.CountOfBuys);




        public IEnumerable<ProductListViewModel> GetFinalProductsSelected(IQueryable<Product> products)
        => products
                   .Select(p => new ProductListViewModel()
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Type = p.Type.TypeName,
                       Brand = p.Brand.BrandName,
                       Price = p.Price,
                       Description = p.Description,
                       ImageUrl = p.ImageUrl,
                       InStock = p.CountInStock,
                       PriceOnPromotion = p.PriceOnPromotion,
                       IsOnPromotion = p.IsOnPromotion
                   })
                   .ToList();


        public IEnumerable<ProductListViewModel> GetAllProductsOnPromotion()
        => this.data.Products
                    .Where(p => p.IsOnPromotion == true && p.IsDeleted == false)
                    .Select(p => new ProductListViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Type = p.Type.TypeName,
                        Brand = p.Brand.BrandName,
                        Price = p.Price,
                        PriceOnPromotion = p.PriceOnPromotion,
                        InStock = p.CountInStock,
                        IsOnPromotion = p.IsOnPromotion,
                        Description = p.Description,
                        ImageUrl = p.ImageUrl
                    })
                    .ToList();


        public IEnumerable<ProductListViewModel> ShowMostBuyedProducts()
         => this.data
                .Products
                .Where(p => p.IsDeleted == false)
                .OrderByDescending(p => p.CountOfBuys)
                .Select(p => new ProductListViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Brand = p.Brand.BrandName,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    IsOnPromotion = p.IsOnPromotion,
                    PriceOnPromotion = p.PriceOnPromotion,
                    InStock = p.CountInStock,
                    Type = p.Type.TypeName
                })
                .Take(5)
                .ToList();


        public bool UpdateInStockCountOfProducts()
        {
            var products = this.data.Products
                                    .Where(p => p.CountInStock == 0 && p.IsDeleted == false)
                                    .ToList();

            if (products.Count() == 0)
            {
                return false;
            }

            for (int i = 0; i < products.Count(); i++)
            {
                products[i].CountInStock = 10;
            }

            this.data.SaveChanges();

            return true;
        }


        public Product TakeProduct(int id)
        {
            var product = this.data.Products
                                   .Where(p => p.Id == id && p.IsDeleted == false)
                                   .FirstOrDefault();


            return product;
        }


        public ProductDetailViewModel GetProductForDetails(int id)
        => this.data.Products
                    .Where(p => p.Id == id && p.IsDeleted == false)
                    .Select(p => new ProductDetailViewModel()
                    {
                        Brand = p.Brand.BrandName,
                        Description = p.Description,
                        ImageUrl = p.ImageUrl,
                        Name = p.Name,
                        Price = p.Price,
                        Type = p.Type.TypeName,
                        IsOnPromotion = p.IsOnPromotion,
                        PriceOnPromotion = p.PriceOnPromotion
                    })
                    .FirstOrDefault();

    }
}
