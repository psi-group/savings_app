using Application.Services.Interfaces;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _productService.GetProducts());
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ProductDTOResponse>>> GetFilteredProducts(
            [FromQuery] List<Category> category,
            [FromQuery] string? search,
            [FromQuery] string? order = "by_id")
        {
            return Ok(await _productService.GetFilteredProducts(category, search, order));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            return Ok(await _productService.GetProduct(id));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "seller")]
        public async Task<ActionResult<Product>> PutProduct(Guid id, [FromForm] ProductDTORequest product)
        {
            return Ok(await _productService.PutProduct(id, product));
        }

        [HttpPost]
        [Authorize(Roles = "seller")]
        public async Task<ActionResult<Product>> PostProduct(
            [FromForm] ProductDTORequest product)
        {
            return Ok(await _productService.PostProduct(product));
        }

        [Authorize(Roles = "seller")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            return Ok(await _productService.DeleteProduct(id));
        }
    }
}
