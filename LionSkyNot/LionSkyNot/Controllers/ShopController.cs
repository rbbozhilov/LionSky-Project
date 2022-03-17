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



        public IActionResult Index(string brand, string type, SortedProductViewModel sortedBy)
        {

            var allProductViewModel = new AllProductsViewModel()
            {
                Brands = this.productService.GetAllBrandProduct().Select(x => x.Name),
                Types = this.productService.GetAllTypesProduct().Select(x => x.Name)
            };



            if (!string.IsNullOrWhiteSpace(brand) && !string.IsNullOrEmpty(brand)
                        && !string.IsNullOrWhiteSpace(type) && !string.IsNullOrEmpty(type)
                        && !string.IsNullOrWhiteSpace(sortedBy.ToString()) && !string.IsNullOrEmpty(sortedBy.ToString()))
            {

                var products = this.productService.GetProductsByBrandAndType(type, brand);

                switch (sortedBy.ToString())
                {
                    case "SortedByPrice":
                        {
                            products = this.productService.SortedByPrice(products);
                            break;
                        }

                    case "SortedByPriceDescending":
                        {
                            products = this.productService.SortedByPriceDescending(products);
                            break;
                        }

                    case "SortedByName":
                        {
                            products = this.productService.SortedByName(products);
                            break;
                        }

                    case "SortedByMostBuys":
                        {

                            break;
                        }
                }

                var finalProducts = this.productService.GetFinalProductsSelected(products);


                return View("Result", finalProducts);

            }


            allProductViewModel.Products = this.productService.ShowAllProducts();

            return View(allProductViewModel);
        }


        public IActionResult Bcaa()
        {

            return View();
        }

        public IActionResult Protein()
        {

            return View();
        }

        public IActionResult LCarnitine()
        {

            return View();
        }

        public IActionResult Creatine()
        {

            return View();
        }



        public IActionResult TopProductsBuyed()
        {

            return View();
        }


    }
}
