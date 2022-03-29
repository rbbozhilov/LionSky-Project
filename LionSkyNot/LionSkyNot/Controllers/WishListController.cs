using LionSkyNot.Data;
using LionSkyNot.Data.Models.Shop;
using LionSkyNot.Services.Products;
using LionSkyNot.Services.WishLists;
using LionSkyNot.Views.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LionSkyNot.Controllers
{

    [Authorize]
    public class WishListController : BaseController
    {
        private IProductService productService;
        private IWishListService wishListService;
        private LionSkyDbContext data;

        public WishListController(
                                  LionSkyDbContext data,
                                  IProductService productService,
                                  IWishListService wishListService)
        {
            this.data = data;
            this.productService = productService;
            this.wishListService = wishListService;
        }


        public IActionResult Index()
        {
            var user = Infrastructure.ClaimsPrincipalExtensions.GetId(this.User);

            var currentProducts = this.wishListService.GetProductsOfUser(user);

            return View(currentProducts);
        }


        public IActionResult AddToWishList(int id)
        {
            var user = Infrastructure.ClaimsPrincipalExtensions.GetId(this.User);

            var currentProduct = this.productService.TakeProduct(id);

            this.wishListService.Add(currentProduct, user);

            return View("Index");

        }

        public IActionResult RemoveProduct(int id)
        {
            var user = Infrastructure.ClaimsPrincipalExtensions.GetId(this.User);

            if (!this.wishListService.RemoveProduct(id, user))
            {
                return BadRequest();
            };

            return View("Index");
        }

        public IActionResult BuyProducts()
        {

            var user = Infrastructure.ClaimsPrincipalExtensions.GetId(this.User);

            if (!this.wishListService.BuyProducts(user))
            {
                return BadRequest();
            }


            return View("Index");
        }


    }
}
