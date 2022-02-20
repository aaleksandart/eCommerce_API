#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce_API;
using eCommerce_API.Models.Entities;
using eCommerce_API.Models.DisplayModels;
using eCommerce_API.Models.CreateModels;
using eCommerce_API.Models.SupportModels;
using eCommerce_API.Models.UpdateModels;
using eCommerce_API.Services;
using eCommerce_API.Filters;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [UserAccessApiKey]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService) =>
            _productService = productService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDisplayModel>>> GetProducts() =>
            await _productService.GetProductsAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDisplayModel>> GetProduct(int id) =>
            await _productService.GetProductAsync(id);

        [HttpPut("{id}")]
        [AdminApiKey]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateModel updateProduct) =>
            await _productService.UpdateProductAsync(id, updateProduct);

        [HttpPost]
        [AdminApiKey]
        public async Task<ActionResult<ProductDisplayModel>> CreateProduct(ProductCreateModel productCreate) =>
            await _productService.CreateProductAsync(productCreate);

        [HttpDelete("{id}")]
        [AdminApiKey]
        public async Task<IActionResult> DeleteProduct(int id) =>
            await _productService.DeleteProductAsync(id);
    }
}
