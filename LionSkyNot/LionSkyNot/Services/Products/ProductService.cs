using LionSkyNot.Data;
using LionSkyNot.Data.Models.Product;
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


        public void CreateProduct(string name, decimal price, string description, string imgUrl, int typeProductId, int brandProductId)
        {
            var newProduct = new Product()
            {
                Name = name,
                Description = description,
                ImageUrl = imgUrl,
                Price = price,
                TypeId = typeProductId,
                BrandId = brandProductId
            };

            data.Products.Add(newProduct);

            data.SaveChanges();
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
                                    .Where(p => p.Brand.BrandName == brand && p.Type.TypeName == type)
                                    .Distinct();

            return products;
        }


        public IEnumerable<ProductListViewModel> ShowAllProducts()
        {

            var products = this.data.Products
                                    .Select(p => new ProductListViewModel()
                                    {
                                        Type = p.Type.TypeName,
                                        Brand = p.Brand.BrandName,
                                        Price = p.Price,
                                        Description = p.Description,
                                        ImageUrl = p.ImageUrl
                                    })
                                    .Distinct()
                                    .ToList();

            return products;
        }


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
    }
}
