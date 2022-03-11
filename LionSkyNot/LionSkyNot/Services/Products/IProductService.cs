using LionSkyNot.Views.ViewModels.Products;

namespace LionSkyNot.Services.Products
{
    public interface IProductService
    {

        IEnumerable<ProductTypeViewModel> GetAllTypesProduct();

        IEnumerable<ProductBrandViewModel> GetAllBrandProduct();

        void CreateProduct(string name,decimal price, string description, string imgUrl, int typeProductId, int brandProductId);

    }
}
