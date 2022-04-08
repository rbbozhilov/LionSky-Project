using LionSkyNot.Services.Products;

using LionSkyNot.Views.ViewModels.Products;

using Microsoft.AspNetCore.Mvc;


namespace LionSkyNot.Controllers
{
    public class ShopController : BaseController
    {

        private IProductService productService;

        public ShopController(IProductService productService)
        {
            this.productService = productService;
        }



        public IActionResult Index([FromQuery] AllProductsViewModel query)
        {

            var allProductViewModel = new AllProductsViewModel()
            {
                Brands = this.productService.GetAllBrandProduct().Select(x => x.Name),
                Types = this.productService.GetAllTypesProduct().Select(x => x.Name)
            };

            if (!string.IsNullOrWhiteSpace(query.Brand) &&
                !string.IsNullOrEmpty(query.Brand) &&
                !string.IsNullOrWhiteSpace(query.Type) &&
                !string.IsNullOrEmpty(query.Type) &&
                !string.IsNullOrWhiteSpace(query.SortedBy.ToString()) &&
                !string.IsNullOrEmpty(query.SortedBy.ToString()))
            {

                var products = this.productService.GetProductsByBrandAndType(query.Type, query.Brand);

                products = query.SortedBy switch
                {
                    SortedProductViewModel.SortedByPrice => products = this.productService.SortedByPrice(products),
                    SortedProductViewModel.SortedByPriceDescending => products = this.productService.SortedByPriceDescending(products),
                    SortedProductViewModel.SortedByName => products = this.productService.SortedByName(products),
                    SortedProductViewModel.SortedByMostBuys => products = this.productService.SortedByMostBuys(products),
                    _ => products = this.productService.SortedByName(products)
                };


                var finalProducts = this.productService.GetFinalProductsSelected(products);


                return View("Result", finalProducts);
            }

            return View(allProductViewModel);
        }


        public IActionResult ViewDetails(int id)
        => View(this.productService.GetProductForDetails(id));


        public IActionResult AllProducts()
         => View(this.productService.ShowAllProducts());


        public IActionResult MostBuyedProducts()
         => View(this.productService.ShowMostBuyedProducts());


        public IActionResult SaleProducts()
        => View(this.productService.GetAllProductsOnPromotion());

    }
}
