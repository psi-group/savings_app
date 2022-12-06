using Application.Services.Interfaces;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersController : ControllerBase
    {
        private readonly IBuyerService _buyerService;
        private readonly ILogger<BuyersController> _logger;

        public BuyersController(IBuyerService buyerService,
            ILogger<BuyersController> logger)
        {
            _logger = logger;
            _buyerService = buyerService;
        }

        // GET: api/Buyers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuyerDTOResponse>>> GetBuyers()
        {
            return Ok(await _buyerService.GetBuyers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BuyerDTOResponse>> GetBuyer(Guid id)
        {
            try
            {
                return Ok(await _buyerService.GetBuyer(id));
            }
            catch(RecourseNotFoundException e)
            {
                _logger.LogError(e.ToString());
                return NotFound();
            }
        }

        // PUT: api/Buyers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "buyer")]
        public async Task<ActionResult<BuyerDTOResponse>> PutBuyer(Guid id, BuyerDTORequest Buyer)
        {
            try
            {
                return Ok(await _buyerService.PutBuyer(id, Buyer));
            }
            catch(InvalidRequestArgumentsException e)
            {
                _logger.LogError(e.ToString());
                return BadRequest();
            }
            catch(RecourseAlreadyExistsException e)
            {
                _logger.LogError(e.ToString());
                return BadRequest();
            }
            catch (InvalidIdentityException e)
            {
                _logger.LogError(e.ToString());
                return Unauthorized();
            }
        }

        // POST: api/Buyers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BuyerDTOResponse>> PostBuyer([FromForm] BuyerDTORequest Buyer)
        {
            //throw new NotImplementedException();
            return Ok(await _buyerService.PostBuyer(Buyer));
        }

        // DELETE: api/Buyers/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "buyer")]
        public async Task<ActionResult<BuyerDTOResponse>> DeleteBuyer(Guid id)
        {
            try
            {
                return Ok(await _buyerService.DeleteBuyer(id));
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
