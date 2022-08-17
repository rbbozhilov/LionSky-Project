using LionSkyNot.Models.Products;

using LionSkyNot.Services.Products;
using LionSkyNot.Services.WishLists;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LionSkyNot.Controllers
{

    [Authorize]
    public class WishListController : BaseController
    {
        private IProductService productService;
        private IWishListService wishListService;

        public WishListController(
                                  IProductService productService,
                                  IWishListService wishListService)
        {
            this.productService = productService;
            this.wishListService = wishListService;
        }


        public IActionResult Index()
        {
            var user = Infrastructure.ClaimsPrincipalExtensions.GetId(this.User);


            var tuple = this.wishListService.GetProductsOfUser(user);

            if (tuple.Item1 == false)
            {
                return View("NotAddedProducts");
            }

            var currentProducts = tuple.Item2;

            if (currentProducts.Products.Count() == 0)
            {
                return View("NotAddedProducts");
            }


            return View(currentProducts);
        }


        public async Task<IActionResult> AddToWishList(int id)
        {
            var user = Infrastructure.ClaimsPrincipalExtensions.GetId(this.User);

            var currentProduct = this.productService.TakeProduct(id);
            var isAdded = await this.wishListService.AddAsync(currentProduct, user);

            if (!isAdded)
            {
                return BadRequest();
            }

            return View("SuccessAddToWishList");
        }


        public async Task<IActionResult> RemoveProduct(int id)
        {
            var user = Infrastructure.ClaimsPrincipalExtensions.GetId(this.User);

            var isRemoved = await this.wishListService.RemoveProductAsync(id, user);

            if (!isRemoved)
            {
                return BadRequest();
            };

            return View("SuccessRemoveProduct");
        }


        public IActionResult Payment()
        => View();


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

            if (tuple.Item1 == false && tuple.Item2 == null)
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
