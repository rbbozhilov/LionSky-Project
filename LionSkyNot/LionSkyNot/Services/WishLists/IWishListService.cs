using LionSkyNot.Data.Models.Shop;

using LionSkyNot.Models.WishLists;

using LionSkyNot.Views.ViewModels.Products;


namespace LionSkyNot.Services.WishLists
{
    public interface IWishListService
    {

        Task<bool> AddAsync(Product product, string userId);

        Task<bool> RemoveProductAsync(int productId, string userId);

        Tuple<bool, WishListFormModel> GetProductsOfUser(string userId);

        Tuple<bool, IEnumerable<BuyProductViewModel>> BuyProducts(string userId);

    }
}
