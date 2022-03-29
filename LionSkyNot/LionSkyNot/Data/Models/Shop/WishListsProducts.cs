namespace LionSkyNot.Data.Models.Shop
{
    public class WishListsProducts
    {

        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int WishListId { get; set; }

        public WishList WishList { get; set; }

        public string UserId { get; set; }
    }
}
