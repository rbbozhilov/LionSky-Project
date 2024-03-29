﻿using LionSkyNot.Data;

using LionSkyNot.Data.Models.Shop;

using LionSkyNot.Models.Products;

using LionSkyNot.Models.WishLists;

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



        public async Task<bool> AddAsync(Product product, string userId)
        {

            var wishListProducts = new WishListsProducts();

            var currentWishList = this.data.WishLists
                                           .Where(w => w.UserId == userId)
                                           .FirstOrDefault();

            if (product == null)
            {
                return false;
            }

            if (currentWishList == null)
            {

                currentWishList = new WishList()
                {
                    UserId = userId
                };

                await this.data.WishLists.AddAsync(currentWishList);


                wishListProducts.Product = product;
                wishListProducts.WishList = currentWishList;

            }

            else
            {

                wishListProducts.Product = product;
                wishListProducts.WishList = currentWishList;

            }

            await this.data.WishListsProducts.AddAsync(wishListProducts);


            await this.data.SaveChangesAsync();

            return true;
        }


        public async Task<bool> RemoveProductAsync(int productId, string userId)
        {

            var currentWishList = this.data.WishLists
                                           .Where(w => w.UserId == userId)
                                           .FirstOrDefault();

            if (currentWishList == null)
            {
                return false;
            }

            var currentProduct = this.data.WishListsProducts
                                          .Where(p => p.ProductId == productId && p.WishListId == currentWishList.Id)
                                          .FirstOrDefault();


            if (currentProduct == null)
            {
                return false;
            }


            this.data.WishListsProducts.Remove(currentProduct);

            await this.data.SaveChangesAsync();

            return true;
        }


        public Tuple<bool, WishListFormModel> GetProductsOfUser(string userId)
        {
            var currentWishList = this.data.WishLists
                                           .Where(w => w.UserId == userId)
                                           .FirstOrDefault();


            if (currentWishList == null)
            {
                return new Tuple<bool, WishListFormModel>(false, null);
            }

            var currentProducts = this.data.WishListsProducts
                                           .Include(p => p.Product)
                                           .Where(p => p.WishListId == currentWishList.Id)
                                           .ToList();

            var products = new List<ProductWishListFormModel>();

            foreach (var product in currentProducts)
            {
                var currentProduct = new ProductWishListFormModel()
                {
                    Id = product.Product.Id,
                    Name = product.Product.Name,
                    Price = product.Product.Price,
                    PriceOnPromotion = product.Product.PriceOnPromotion,
                    IsOnPromotion = product.Product.IsOnPromotion
                };

                products.Add(currentProduct);
            }

            var returnProducts = new WishListFormModel()
            {
                Products = products
            };

            return new Tuple<bool, WishListFormModel>(true, returnProducts);

        }


        public Tuple<bool, IEnumerable<BuyProductViewModel>> BuyProducts(string userId)
        {

            var currentWishList = this.data.WishLists
                                           .Where(w => w.UserId == userId)
                                           .FirstOrDefault();

            var currentWishListProduct = this.data.WishListsProducts
                                                  .Where(p => p.WishListId == currentWishList.Id)
                                                  .ToList();

            var buyProducts = new List<BuyProductViewModel>();
            var errorReturnedTuple = new Tuple<bool, IEnumerable<BuyProductViewModel>>(false, buyProducts);
            var errorPayment = new Tuple<bool, IEnumerable<BuyProductViewModel>>(false, null);

            if (currentWishListProduct.Count == 0)
            {
                return errorPayment;
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
                buyProducts.Add(new BuyProductViewModel { Name = currentProduct.Name });
            }


            this.data.WishListsProducts.RemoveRange(currentWishListProduct);

            this.data.SaveChanges();

            var successReturnTuple = new Tuple<bool, IEnumerable<BuyProductViewModel>>(true, buyProducts);


            return successReturnTuple;
        }
    }
}
