using LionSkyNot.Data;
using LionSkyNot.Data.Models.Shop;
using LionSkyNot.Models.Products;
using LionSkyNot.Models.WihLists;
using LionSkyNot.Services.Products;
using LionSkyNot.Views.ViewModels.Products;
using Microsoft.EntityFrameworkCore;

namespace LionSkyNot.Services.WishLists
{
    public class WishListService : IWishListService
    {

        private LionSkyDbContext data;
        private IProductService productService;

        public WishListService(LionSkyDbContext data, IProductService productService)
        {
            this.data = data;
            this.productService = productService;
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

        public Tuple<bool, IEnumerable<BuyProductViewModel>> BuyProducts(string userId)
        {

            var currentWishListProduct = this.data.WishListsProducts
                                          .Where(w => w.UserId == userId)
                                          .ToList();


            var buyProducts = new List<BuyProductViewModel>();
            var errorReturnedTuple = new Tuple<bool, IEnumerable<BuyProductViewModel>>(false, buyProducts);
            
            if(currentWishListProduct == null)
            {
                return errorReturnedTuple;
            }


            foreach (var product in currentWishListProduct)
            {
                var currentProduct = this.productService.TakeProduct(product.ProductId);

                if (currentProduct.CountInStock <= 0)
                {
                    this.data.WishListsProducts.RemoveRange(currentWishListProduct);
                    this.data.SaveChanges();
                    return errorReturnedTuple;
                }

                currentProduct.CountInStock--;
                currentProduct.CountOfBuys++;
                buyProducts.Add(new BuyProductViewModel { Name = currentProduct.Name});
            }


            this.data.WishListsProducts.RemoveRange(currentWishListProduct);

            this.data.SaveChanges();

            var successReturnTuple = new Tuple<bool, IEnumerable<BuyProductViewModel>>(true, buyProducts);


            return successReturnTuple;

        }
    }
}
