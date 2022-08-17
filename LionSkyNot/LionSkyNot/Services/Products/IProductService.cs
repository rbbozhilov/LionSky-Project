using LionSkyNot.Data.Models.Shop;

using LionSkyNot.Models.Products;

using LionSkyNot.Views.ViewModels.Products;


namespace LionSkyNot.Services.Products
{
    public interface IProductService
    {

        Task CreateProductAsync(
                           string name,
                           decimal price,
                           int inStock,
                           string description,
                           string imgUrl,
                           int typeProductId,
                           int brandProductId);

        Task<bool> UpdateInStockCountOfProductsAsync();

        Task<bool> EditProductAsync(
                         int id,
                         string imageUrl,
                         string name,
                         decimal price,
                         float percentage);

        Task<bool> DeleteProductAsync(int id);

        bool IsHaveBrand(int brandId);

        bool IsHaveType(int typeId);

        EditProductFormModel GetProductById(int id);

        Product TakeProduct(int id);

        ProductDetailViewModel GetProductForDetails(int id);

        IEnumerable<ProductTypeViewModel> GetAllTypesProduct();

        IEnumerable<ProductBrandViewModel> GetAllBrandProduct();

        IQueryable<Product> GetProductsByBrandAndType(
                                                      string type,
                                                      string brand);

        IEnumerable<ProductListViewModel> ShowAllProducts();

        IEnumerable<ProductListViewModel> ShowMostBuyedProducts();

        IEnumerable<ProductListViewModel> GetAllProductsOnPromotion();

        IEnumerable<ProductServiceModel> GetAllProductsForAdmin();

        IQueryable<Product> SortedByPrice(IQueryable<Product> products);

        IQueryable<Product> SortedByPriceDescending(IQueryable<Product> products);

        IQueryable<Product> SortedByName(IQueryable<Product> products);

        IQueryable<Product> SortedByMostBuys(IQueryable<Product> products);

        IEnumerable<ProductListViewModel> GetFinalProductsSelected(IQueryable<Product> products);

    }
}
