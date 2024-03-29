﻿using LionSkyNot.Controllers;

using LionSkyNot.Models.Products;

using LionSkyNot.Services.Products;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static LionSkyNot.Areas.Admin.AdminConstants;


namespace LionSkyNot.Areas.Admin.Controllers
{
    [Area(AreaName)]
    public class ShopController : BaseController
    {

        private IProductService productService;

        public ShopController(IProductService productService)
        {
            this.productService = productService;
        }



        [Authorize(Roles = ModeratorAndAdminRole)]
        public IActionResult AddProduct()
        {
            return View(new AddProductFormModel()
            {
                Type = this.productService.GetAllTypesProduct(),
                Brand = this.productService.GetAllBrandProduct()
            });

        }


        [Authorize(Roles = ModeratorAndAdminRole)]
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductFormModel productModel)
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

            await this.productService.CreateProductAsync(
                 productModel.Name,
                 productModel.Price,
                 productModel.InStock,
                 productModel.Description,
                 productModel.ImageUrl,
                 productModel.TypeId,
                 productModel.BrandId);

            return RedirectToAction("Successfull");
        }


        [Authorize(Roles = ModeratorAndAdminRole)]
        public async Task<IActionResult> AddProductsInStock()
        {
            var isDone = await this.productService.UpdateInStockCountOfProductsAsync();

            if (!isDone)
            {
                return RedirectToAction("NotSuccess");
            }


            return View();
        }


        [Authorize(Roles = AdminRole)]
        public IActionResult Successfull()
        {
            return View();
        }


        [Authorize(Roles = AdminRole)]
        public IActionResult NotSuccess()
        {
            return View();
        }


        [Authorize(Roles = AdminRole)]
        public IActionResult ShowProducts(IEnumerable<ProductServiceModel> serviceModel)
        {

            serviceModel = this.productService.GetAllProductsForAdmin();

            return View(serviceModel);
        }


        [Authorize(Roles = AdminRole)]
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


        [Authorize(Roles = AdminRole)]
        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductFormModel productModel, int id)
        {

            if (!ModelState.IsValid)
            {
                return View(productModel);
            }

            var isEditted = await this.productService.EditProductAsync(
                                                              id,
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


        [Authorize(Roles = AdminRole)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var isDeleted = await this.productService.DeleteProductAsync(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return View("Successfull");
        }

    }
}
