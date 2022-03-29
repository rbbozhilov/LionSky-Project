using LionSkyNot.Models.Class;
using LionSkyNot.Models.Products;

namespace LionSkyNot.Models.WihLists
{
    public class WishListFormModel
    {

        public IEnumerable<ProductWishListFormModel> Products { get; set; }


        public decimal Price => this.Products.Sum(x => x.Price);


    }
}
