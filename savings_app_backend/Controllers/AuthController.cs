using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using savings_app_backend.Extention;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly savingsAppContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;

        public AuthController(savingsAppContext context, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }



        // GET: api/Auth
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAuth>>> GetUserAuth()
        {
            return await _context.UserAuths.ToListAsync();
        }

        // GET: api/Auth/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAuth>> GetUserAuth(Guid id)
        {
            var userAuth = await _context.UserAuths.FindAsync(id);

            if (userAuth == null)
            {
                return NotFound();
            }

            return userAuth;
        }

        // PUT: api/Auth/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAuth(Guid id, UserAuth userAuth)
        {
            if (id != userAuth.Id)
            {
                return BadRequest();
            }

            _context.Entry(userAuth).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAuthExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // POST: api/Auth
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("register/buyer")]
        public async Task<IActionResult> RegisterBuyer([FromForm] Buyer buyer)
        {
            if (buyer == null || buyer.UserAuth == null)
                return BadRequest();
            var user = _context.UserAuths.FirstOrDefault(b => b.Id == buyer.UserAuth.Id);
            if (user != null)
            {
                return BadRequest("User with this email already exists");
            }
            else if (!buyer.UserAuth.Email.IsValidEmail())
            {
                return BadRequest("Invalid email format");
            }
            else if (!buyer.UserAuth.Password.IsValidPassword())
            {
                return BadRequest("Invalid password format");
            }
            else
            {
                buyer.Id = Guid.NewGuid();

                var userAuthId = Guid.NewGuid();
                buyer.UserAuth.Id = userAuthId;
                buyer.UserAuthId = userAuthId;

                var addressId = Guid.NewGuid();
                buyer.Address.Id = addressId;
                buyer.AddressId = addressId;

                var imageName = buyer.Id.ToString() + ".jpg";
                SaveImage(buyer.ImageFile, imageName);
                buyer.ImageName = imageName;
                _context.Buyers.Add(buyer);
                await _context.SaveChangesAsync();
                return Ok();
            }


            //return CreatedAtAction("GetUserAuth", new { id = userAuth.Id }, userAuth);
        }

        [HttpPost("register/seller")]
        public async Task<ActionResult<UserAuth>> RegisterSeller([FromForm] Restaurant restaurant)
        {
            if (restaurant == null || restaurant.UserAuth == null)
                return BadRequest();
            var user = _context.UserAuths.FirstOrDefault(b => b.Id == restaurant.UserAuth.Id);
            if (user != null)
            {
                return BadRequest("User with this email already exists");
            }
            else if (!restaurant.UserAuth.Email.IsValidEmail())
            {
                return BadRequest("Invalid email format");
            }
            else if (!restaurant.UserAuth.Password.IsValidPassword())
            {
                return BadRequest("Invalid password format");
            }
            else
            {
                restaurant.Id = Guid.NewGuid();

                var userAuthId = Guid.NewGuid();
                restaurant.UserAuth.Id = userAuthId;
                restaurant.UserAuthId = userAuthId;

                var addressId = Guid.NewGuid();
                restaurant.Address.Id = addressId;
                restaurant.AddressId = addressId;

                var imageName = restaurant.Id.ToString() + ".jpg";
                SaveImage(restaurant.ImageFile, imageName);
                restaurant.ImageName = imageName;
                //_context.Buyers.Add(restaurant);
                await _context.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserLoginDTO userLogin)
        {
            var user = AuthenticateUser(userLogin);

            if(user != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }
            else
            {
                return NotFound("No such user found");
            }
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string role = "";
            if (user.GetType() == typeof(Buyer))
            {
                role = "buyer";
            }
            else if(user.GetType() == typeof(Restaurant))
            {
                role = "seller";
            }
            else
            {
                role = "unknow";
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                //new Claim(ClaimTypes.Email, user.UserAuth.Email),
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User AuthenticateUser(UserLoginDTO userLogin)
        {
            var buyer = _context.Buyers.FirstOrDefault(user => user.UserAuth.Email.ToLower() == userLogin.Email.ToLower() &&
                user.UserAuth.Password == userLogin.Password);

            if(buyer != null)
            {
                return buyer;
            }
            else
            {
                var seller = _context.Restaurants.FirstOrDefault(user => user.UserAuth.Email.ToLower() == userLogin.Email.ToLower() &&
                    user.UserAuth.Password == userLogin.Password);
                if(seller != null)
                {
                    return seller;
                }
                else
                {
                    return null;
                }
            }
        }

        [NonAction]
        private async void SaveImage(IFormFile imageFile, string imageName)
        {
            var imagePath = System.IO.Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "userImg", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
        }

        // DELETE: api/Auth/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAuth(Guid id)
        {
            var userAuth = await _context.UserAuths.FindAsync(id);
            if (userAuth == null)
            {
                return NotFound();
            }

            _context.UserAuths.Remove(userAuth);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserAuthExists(Guid id)
        {
            return _context.UserAuths.Any(e => e.Id == id);
        }
    }
}
