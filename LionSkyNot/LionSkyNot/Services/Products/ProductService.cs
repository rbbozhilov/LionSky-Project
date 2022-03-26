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


        public void CreateProduct(string name, decimal price, int inStock, string description, string imgUrl, int typeProductId, int brandProductId)
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


        public void EditProduct(int id, string imageUrl, string name, decimal price)
        {
            var currentProduct = this.data.Products
                                          .Where(p => p.Id == id && p.IsDeleted == false)
                                          .FirstOrDefault();

            currentProduct.ImageUrl = imageUrl;
            currentProduct.Name = name;
            currentProduct.Price = price;

            this.data.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var currentProduct = this.data.Products
                                          .Where(p => p.Id == id && p.IsDeleted == false)
                                          .FirstOrDefault();

            currentProduct.IsDeleted = true;

            this.data.SaveChanges();
        }

        public IEnumerable<ProductBrandViewModel> GetAllBrandProduct()
        => this.data.Brands.Select(b => new ProductBrandViewModel
        {
            Id = b.Id,
            Name = b.BrandName
        }).ToList();


        public IEnumerable<ProductTypeViewModel> GetAllTypesProduct()
        => this.data.Types.Select(t => new ProductTypeViewModel
        {
            Id = t.Id,
            Name = t.TypeName
        }).ToList();


        public IQueryable<Product> GetProductsByBrandAndType(string type, string brand)
        {

            var products = this.data.Products
                                    .Where(p => p.Brand.BrandName == brand && p.Type.TypeName == type && p.IsDeleted == false)
                                    .Distinct();

            return products;
        }


        public IEnumerable<ProductListViewModel> ShowAllProducts()
        {

            var products = this.data.Products
                                    .Where(p => p.IsDeleted == false)
                                    .Select(p => new ProductListViewModel()
                                    {
                                        Type = p.Type.TypeName,
                                        Brand = p.Brand.BrandName,
                                        Price = p.Price,
                                        Description = p.Description,
                                        ImageUrl = p.ImageUrl,
                                        InStock = p.CountInStock
                                    })
                                    .Distinct()
                                    .ToList();

            return products;
        }

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
        {

            products = products.Distinct()
                               .OrderByDescending(p => p.Price);

            return products;
        }

        public IQueryable<Product> SortedByPrice(IQueryable<Product> products)
        {

            products = products.Distinct()
                               .OrderBy(p => p.Price);

            return products;
        }

        public IQueryable<Product> SortedByName(IQueryable<Product> products)
        {

            products = products.Distinct()
                               .OrderBy(p => p.Name);

            return products;
        }


        public IEnumerable<ProductListViewModel> GetFinalProductsSelected(IQueryable<Product> products)
        {
            return products.Select(p => new ProductListViewModel()
            {
                Type = p.Type.TypeName,
                Brand = p.Brand.BrandName,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl
            }).ToList();
        }


        public IEnumerable<ProductListViewModel> ShowMostBuyedProducts(int countOfProducts)
         => this.data
                .Products
                .Where(p => p.IsDeleted == false)
                .OrderByDescending(p => p.CountOfBuys)
                .Select(p => new ProductListViewModel()
                {
                    Name = p.Name,
                    Brand = p.Brand.BrandName,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Type = p.Type.TypeName
                })
                .Take(countOfProducts)
                .ToList();


        public IEnumerable<Product> GetAllProductsWithZeroInStock(int countOfProducts)
        => this.data
               .Products
               .Where(p => p.CountInStock == 0 && p.IsDeleted == false)
               .ToList();


        public bool UpdateInStockCountOfProducts()
        {
            var products = this.data.Products.Where(p => p.CountInStock == 0 && p.IsDeleted == false).ToList();

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

    }
}
