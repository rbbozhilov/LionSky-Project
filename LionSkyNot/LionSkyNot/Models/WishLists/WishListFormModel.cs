using LionSkyNot.Models.Class;
using LionSkyNot.Models.Products;

namespace LionSkyNot.Models.WishLists
{
    public class WishListFormModel
    {

        public IEnumerable<ProductWishListFormModel> Products { get; set; }

        public decimal Price => this.TotalPrice();


        private decimal TotalPrice()
        {
            decimal price = 0;

            foreach(var product in this.Products)
            {
                if (product.IsOnPromotion)
                {
                    price += product.PriceOnPromotion;
                }
                else
                {
                    price += product.Price;
                }
            }

            return price;

        }

    }
}
