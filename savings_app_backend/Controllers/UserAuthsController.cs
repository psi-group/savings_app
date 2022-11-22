using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using savings_app_backend.Exceptions;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Models.Enums;
using savings_app_backend.Services.Interfaces;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthsController : ControllerBase
    {
        private readonly IUserAuthService _UserAuthService;
        private readonly ILogger<UserAuthsController> _logger;

        public UserAuthsController(IUserAuthService UserAuthService,
            ILogger<UserAuthsController> logger)
        {
            _UserAuthService = UserAuthService;
            _logger = logger;
        }

        // GET: api/UserAuths
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAuth>>> GetUserAuthes()
        {
            return Ok(await _UserAuthService.GetUserAuths());
        }

        // GET: api/UserAuths/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAuth>> GetUserAuth(Guid id)
        {
            try
            {
                return Ok(await _UserAuthService.GetUserAuth(id));
            }
            catch(RecourseNotFoundException e)
            {
                _logger.LogError(e.ToString());
                return NotFound();
            }
        }

        // PUT: api/UserAuths/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAuth(Guid id, UserAuth userAuth)
        {
            try
            {
                return Ok(await _UserAuthService.PutUserAuth(id, userAuth));
            }
            catch(InvalidRequestArgumentsException e)
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

        // POST: api/UserAuths
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserAuth>> PostUserAuth([FromBody] UserAuth userAuth)
        {
            return Ok(await _UserAuthService.PostUserAuth(userAuth));
        }

        // DELETE: api/UserAuths/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAuth(Guid id)
        {
            try
            {
                return Ok(await _UserAuthService.DeleteUserAuth(id));
            }
            catch(RecourseNotFoundException e)
            {
                _logger.LogError(e.ToString());
                return NotFound();
            }
        }
    }
}
