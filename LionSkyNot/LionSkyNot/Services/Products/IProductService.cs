using LionSkyNot.Data.Models.Product;
using LionSkyNot.Views.ViewModels.Products;

namespace LionSkyNot.Services.Products
{
    public interface IProductService
    {

        IEnumerable<ProductTypeViewModel> GetAllTypesProduct();

        IEnumerable<ProductBrandViewModel> GetAllBrandProduct();

        IQueryable<Product> GetProductsByBrandAndType(string type, string brand);

        IEnumerable<ProductListViewModel> ShowAllProducts();

        IEnumerable<ProductListViewModel> SortedByPrice();

        IQueryable<Product> SortedByPriceDescending();

        IQueryable<ProductListViewModel> SortedByName();

        void CreateProduct(string name, decimal price, string description, string imgUrl, int typeProductId, int brandProductId);

    }
}
