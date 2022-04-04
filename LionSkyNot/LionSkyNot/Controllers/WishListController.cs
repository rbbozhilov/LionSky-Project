using LionSkyNot.Data;
using LionSkyNot.Data.Models.Shop;
using LionSkyNot.Models.Products;
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

            
            var tuple = this.wishListService.GetProductsOfUser(user);

            var currentProducts = tuple.Item2;

            return View(currentProducts);
        }


        public IActionResult AddToWishList(int id)
        {
            var user = Infrastructure.ClaimsPrincipalExtensions.GetId(this.User);

            var currentProduct = this.productService.TakeProduct(id);

            this.wishListService.Add(currentProduct, user);

            return View("SuccessAddToWishList");

        }

        public IActionResult RemoveProduct(int id)
        {
            var user = Infrastructure.ClaimsPrincipalExtensions.GetId(this.User);

            if (!this.wishListService.RemoveProduct(id, user))
            {
                return BadRequest();
            };

            return View("SuccessRemoveProduct");
        }


        public IActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Payment(PaymentFormModel paymentModel)
        {

            if (!ModelState.IsValid)
            {
                return View(paymentModel);
            }


            return RedirectToAction("BuyProducts");
        }

        public IActionResult BuyProducts()
        {

            var user = Infrastructure.ClaimsPrincipalExtensions.GetId(this.User);


            var tuple = this.wishListService.BuyProducts(user);


            if(tuple.Item1 == false && tuple.Item2 == null)
            {
                return BadRequest();
            }

            if (tuple.Item1 == false)
            {
                return View("BuyedProducts", tuple.Item2);
            }


            return View("SuccessBuyedAll", tuple.Item2);

        }

    }
}
