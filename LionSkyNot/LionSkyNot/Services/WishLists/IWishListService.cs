using LionSkyNot.Data.Models.Shop;
using LionSkyNot.Models.WihLists;

namespace LionSkyNot.Services.WishLists
{
    public interface IWishListService
    {

        void Add(Product product, string userId);

        WishListFormModel GetProductsOfUser(string userId);


    }
}
