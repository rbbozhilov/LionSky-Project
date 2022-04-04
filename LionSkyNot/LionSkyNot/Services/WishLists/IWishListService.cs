using LionSkyNot.Data.Models.Shop;
using LionSkyNot.Models.WishLists;
using LionSkyNot.Views.ViewModels.Products;

namespace LionSkyNot.Services.WishLists
{
    public interface IWishListService
    {

        bool Add(Product product, string userId);

        bool RemoveProduct(int productId, string userId);

        Tuple<bool, WishListFormModel> GetProductsOfUser(string userId);

        Tuple<bool, IEnumerable<BuyProductViewModel>> BuyProducts(string userId);


    }
}
