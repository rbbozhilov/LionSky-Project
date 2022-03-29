using LionSkyNot.Data.Models.Shop;
using LionSkyNot.Models.WihLists;

namespace LionSkyNot.Services.WishLists
{
    public interface IWishListService
    {

        void Add(Product product, string userId);

        bool RemoveProduct(int productId, string userId);

        bool BuyProducts(string userId);

        WishListFormModel GetProductsOfUser(string userId);



    }
}
