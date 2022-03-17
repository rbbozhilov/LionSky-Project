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


        public IQueryable<Product> SortedByPriceDescending()
        {

            var products = this.data.Products    
                                    .Distinct()
                                    .OrderByDescending(p => p.Price);

            return products;
        }

        public IEnumerable<ProductListViewModel> SortedByPrice()
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
                                    .OrderBy(p => p.Price)
                                    .ToList();

            return products;
        }

        public IQueryable<ProductListViewModel> SortedByName()
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
                                    .OrderBy(p => p.Name);

            return products;
        }

    }
}
