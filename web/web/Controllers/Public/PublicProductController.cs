using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web.Web.Services.Services;
using Web.Entity.Dto.UserSite;

namespace web.Controllers.Public
{
    public class PublicProductController : PublicBaseController
    {
        private IProductService _productService;
        public PublicProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Route("~/Products", Name = "Products")]
        public async Task<ActionResult> Product(int? page, string query, int? parentProductId)
        {
            var obj = new ProductPageDto();
            obj.ParentProductId = parentProductId;
            obj.query = query?.Trim();
            obj.page = page ?? 1;
            ViewBag.parentProductId = parentProductId;
            obj.GetAllProducts = await _productService.GetDisplayProductsForProductPage(obj.page, 20, obj.query, parentProductId);
            obj.GetParentProducts = await _productService.GetParentProductsWithChildProduct();
            return View(obj);
        }

        [Route("~/ProductDetails/{productId}")]
        public async Task<ActionResult> ProductDetails(int productId)
        {
            var obj = new ProductPageDto();
            obj.Product = await _productService.GetProductById(productId);
            obj.ProductImages = await _productService.GetProductImageByProductId(productId);
            obj.ProductPrice = await _productService.GetActiveProductPriceByProductId(productId);
            return View(obj);
        }
    }
}