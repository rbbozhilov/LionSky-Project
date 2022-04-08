using LionSkyNot.Controllers;

using LionSkyNot.Models.Products;

using LionSkyNot.Services.Products;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;


namespace LionSkyNot.Areas.Admin.Controllers
{
    [Area(AdminConstants.AreaName)]
    public class ShopController : BaseController
    {

        private IProductService productService;

        public ShopController(IProductService productService)
        {
            this.productService = productService;
        }



        [Authorize(Roles = "Moderator,Administrator")]
        public IActionResult AddProduct()
        {
            return View(new AddProductFormModel()
            {
                Type = this.productService.GetAllTypesProduct(),
                Brand = this.productService.GetAllBrandProduct()
            });

        }


        [Authorize(Roles = "Moderator,Administrator")]
        [HttpPost]
        public IActionResult AddProduct(AddProductFormModel productModel)
        {

            if (!productService.IsHaveType(productModel.TypeId))
            {
                this.ModelState.AddModelError(nameof(productModel.TypeId), "Don't make some hack tries!");
            }

            if (!productService.IsHaveBrand(productModel.BrandId))
            {
                this.ModelState.AddModelError(nameof(productModel.BrandId), "Don't make some hack tries!");
            }

            if (!ModelState.IsValid)
            {
                productModel.Type = this.productService.GetAllTypesProduct();
                productModel.Brand = this.productService.GetAllBrandProduct();

                return View(productModel);
            }

            this.productService.CreateProduct(productModel.Name,
                productModel.Price,
                productModel.InStock,
                productModel.Description,
                productModel.ImageUrl,
                productModel.TypeId,
                productModel.BrandId);

            return RedirectToAction("Successfull");
        }

        [Authorize(Roles = "Moderator,Administrator")]
        public IActionResult AddProductsInStock()
        {
            bool isDone = this.productService.UpdateInStockCountOfProducts();

            if (!isDone)
            {
                return RedirectToAction("NotSuccess");
            }


            return View();
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult Successfull()
        {
            return View();
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult NotSuccess()
        {
            return View();
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult ShowProducts(IEnumerable<ProductServiceModel> serviceModel)
        {

            serviceModel = this.productService.GetAllProductsForAdmin();

            return View(serviceModel);
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult EditProduct(int id)
        {

            var currentProduct = this.productService.GetProductById(id);

            if (currentProduct == null)
            {
                return BadRequest();
            }

            return View(new EditProductFormModel()
            {
                ImageUrl = currentProduct.ImageUrl,
                Price = currentProduct.Price,
                Name = currentProduct.Name,
            });
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult EditProduct(EditProductFormModel productModel, int id)
        {


            if (!ModelState.IsValid)
            {
                return View(productModel);
            }


            bool isEditted = this.productService.EditProduct(id,
                                                              productModel.ImageUrl,
                                                              productModel.Name,
                                                              productModel.Price,
                                                              productModel.PromotionPercentage);

            if (!isEditted)
            {
                return BadRequest();
            }


            return RedirectToAction("Successfull");

        }


        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteProduct(int id)
        {

            if (!this.productService.DeleteProduct(id))
            {
                return BadRequest();
            }

            return View("Successfull");
        }

    }
}
