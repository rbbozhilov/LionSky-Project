namespace LionSkyNot.Models.Products
{
    public class ProductWishListFormModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool IsOnPromotion { get; set; }

        public decimal PriceOnPromotion { get; set; }

    }
}
