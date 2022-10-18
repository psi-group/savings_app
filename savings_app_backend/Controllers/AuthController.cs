using Microsoft.AspNetCore.Mvc;
using savings_app_backend.Extention;
using savings_app_backend.Models;
using savings_app_backend.WebSite.Services;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private DataAccessServiceUsers _dataAccessServiceUsers;

        public AuthController(DataAccessServiceUsers dataAccessServiceUsers)
        {
            _dataAccessServiceUsers = dataAccessServiceUsers;
        }

        [HttpGet("users")]
        public IEnumerable<User> Get()
        {
            return _dataAccessServiceUsers.GetUsers();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if ((_dataAccessServiceUsers.DoesUserAlreadyExists(user)))
            {
                return BadRequest("User with this email already exists");
            }
            else if (!user.Email.IsValidEmail())
            {
                return BadRequest("Email format is not valid");
            }
            else if (!user.Password.IsValidPassword())
            {
                return BadRequest("Password format is not valid");
            }
            else
            {
                if (_dataAccessServiceUsers.RegisterUser(user))
                    return Ok("registered");
                else
                    return BadRequest("Could not register");

            }

        }
        
    }
}
