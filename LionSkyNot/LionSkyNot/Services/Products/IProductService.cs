using LionSkyNot.Data.Models.Shop;
using LionSkyNot.Views.ViewModels.Products;

namespace LionSkyNot.Services.Products
{
    public interface IProductService
    {

        IEnumerable<ProductTypeViewModel> GetAllTypesProduct();

        IEnumerable<ProductBrandViewModel> GetAllBrandProduct();

        IQueryable<Product> GetProductsByBrandAndType(string type, string brand);

        IEnumerable<ProductListViewModel> ShowAllProducts();

        IQueryable<Product> SortedByPrice(IQueryable<Product> products);

        IQueryable<Product> SortedByPriceDescending(IQueryable<Product> products);

        IQueryable<Product> SortedByName(IQueryable<Product> products);

        IEnumerable<ProductListViewModel> GetFinalProductsSelected(IQueryable<Product> products);

        void CreateProduct(string name, decimal price, string description, string imgUrl, int typeProductId, int brandProductId);

    }
}
