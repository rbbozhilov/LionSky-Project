using LionSkyNot.Data;
using LionSkyNot.Data.Models.Shop;
using LionSkyNot.Models.Products;
using LionSkyNot.Models.WihLists;
using Microsoft.EntityFrameworkCore;

namespace LionSkyNot.Services.WishLists
{
    public class WishListService : IWishListService
    {

        private LionSkyDbContext data;

        public WishListService(LionSkyDbContext data)
        {
            this.data = data;
        }


        public void Add(Product product, string userId)
        {

            var wishListProducts = new WishListsProducts();
            var wishList = new WishList();

            var currentWishListProducts = this.data.WishListsProducts
                                                   .Where(w => w.UserId == userId)
                                                   .FirstOrDefault();


            if (currentWishListProducts == null)
            {
                wishListProducts.Product = product;
                wishListProducts.WishList = wishList;
                wishListProducts.UserId = userId;

            }

            else
            {

                wishListProducts.Product = product;
                wishListProducts.WishListId = currentWishListProducts.WishListId;
                wishListProducts.UserId = currentWishListProducts.UserId;

            }

            this.data.WishListsProducts.Add(wishListProducts);


            this.data.SaveChanges();


        }

        public WishListFormModel GetProductsOfUser(string userId)
        {
            var currentWishList = this.data.WishListsProducts
                                           .Include(w => w.Product)
                                           .Where(w => w.UserId == userId)
                                           .ToList();

            var products = new List<ProductWishListFormModel>();

            foreach (var wishList in currentWishList)
            {
                var currentProduct = new ProductWishListFormModel()
                {
                    Id = wishList.Product.Id,
                    Name = wishList.Product.Name,
                    Price = wishList.Product.Price
                };

                products.Add(currentProduct);
            }

            var returnProducts = new WishListFormModel()
            {
                Products = products
            };

            return returnProducts;

        }


        public bool RemoveProduct(int productId, string userId)
        {

            var currentWishListProduct = this.data.WishListsProducts
                                           .Where(w => w.UserId == userId && w.ProductId == productId)
                                           .FirstOrDefault();

            if (currentWishListProduct == null)
            {
                return false;
            }


            this.data.WishListsProducts.Remove(currentWishListProduct);

            this.data.SaveChanges();

            return true;
        }

        public bool BuyProducts(string userId)
        {

            var currentWishListProduct = this.data.WishListsProducts
                                          .Where(w => w.UserId == userId)
                                          .ToList();

            if (currentWishListProduct == null)
            {
                return false;
            }


            this.data.WishListsProducts.RemoveRange(currentWishListProduct);

            this.data.SaveChanges();

            return true;

        }

    }
}
