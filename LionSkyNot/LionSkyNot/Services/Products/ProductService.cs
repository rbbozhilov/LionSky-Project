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


        public void CreateProduct(string name, decimal price,string description, string imgUrl,  int typeProductId, int brandProductId)
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



    }
}
