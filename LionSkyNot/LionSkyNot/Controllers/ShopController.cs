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


            //allProductViewModel.Products = this.productService.ShowAllProducts();


            return View(allProductViewModel);
        }

  
        public IActionResult AllProducts()
        {
            var allProducts = this.productService.ShowAllProducts();

            return View(allProducts);
        }

        public IActionResult MostBuyedProducts()
        {

            var mostBuyedProducts = this.productService.ShowMostBuyedProducts();

            return View(mostBuyedProducts);
        }

        public IActionResult SaleProducts()
        {

            var productsOnPromotion = this.productService.GetAllProductsOnPromotion();

            return View(productsOnPromotion);
        }

    }
}
