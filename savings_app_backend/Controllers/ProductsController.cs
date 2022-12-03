using Application.Services.Interfaces;
using Domain.DTOs.Request;
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
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService,
            ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // GET: api/ProductContr
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _productService.GetProducts());
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Product>>> GetFilteredProducts(
            [FromQuery] List<Category> category,
            [FromQuery] string? search,
            [FromQuery] string? order = "by_id")
        {
            try
            {
                return Ok(await _productService.GetFilteredProducts(category, search, order));
            }
            catch(InvalidEnumStringException e)
            {
                _logger.LogError(e.ToString());
                return BadRequest();
            }
            catch(InvalidRequestArgumentsException e)
            {
                _logger.LogError(e.ToString());
                return BadRequest();
            }
            
        }

        // GET: api/ProductContr/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            try
            {
                return Ok(await _productService.GetProduct(id));
                
            }
            catch(RecourseNotFoundException e)
            {
                _logger.LogError(e.ToString());
                return NotFound();
            }
        }

        // PUT: api/ProductContr/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /*[HttpPost("buy")]
        //[Authorize(Roles = "buyer")]
        public async Task<ActionResult<Product>> BuyProduct([FromBody] Guid id,
            [FromQuery] int amount)
        {
            try
            {
                return Ok(await _productService.Buy(id, amount));
            }
            catch(NotEnoughProductAmountException)
            {
                return BadRequest();
            }
            catch (InvalidIdentityException e)
            {
                _logger.LogError(e.ToString());
                
                return Unauthorized();
            }
            catch (InvalidRequestArgumentsException e)
            {
                _logger.LogError(e.ToString());
                
                return BadRequest();
            }
            catch (RecourseNotFoundException e)
            {
                _logger.LogError(e.ToString());
                return NotFound();
            }
        }*/

        [HttpPut("{id}")]
        [Authorize(Roles = "seller")]
        public async Task<ActionResult<Product>> PutProduct(Guid id, [FromForm] ProductDTORequest product)
        {
            try
            {
                return Ok(await _productService.PutProduct(id, product));
            }
            catch (InvalidIdentityException e)
            {
                _logger.LogError(e.ToString());
                return Unauthorized();
            }
            catch (InvalidRequestArgumentsException e)
            {
                _logger.LogError(e.ToString());
                return BadRequest();
            }
            catch(RecourseNotFoundException e)
            {
                _logger.LogError(e.ToString());
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "seller")]
        public async Task<ActionResult<Product>> PostProduct(
            [FromForm] ProductDTORequest product)
        {
            var a = DateTime.Now;
            //throw new NotImplementedException();
            try
            {
                return Ok(await _productService.PostProduct(product));
            }
            catch(InvalidIdentityException e)
            {
                _logger.LogError(e.ToString());
                return Unauthorized();
            }
        }

        [Authorize(Roles = "seller")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            try
            {
                return Ok(await _productService.DeleteProduct(id));
            }
            catch(RecourseNotFoundException e)
            {
                _logger.LogError(e.ToString());
                return NotFound();
            }
            catch(InvalidIdentityException e)
            {
                _logger.LogError(e.ToString());
                return Unauthorized();
            }
        }
    }
}
