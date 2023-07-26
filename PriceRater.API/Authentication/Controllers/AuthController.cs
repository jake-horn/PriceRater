using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using PriceRater.API.Authentication.DTO;
using PriceRater.API.Authentication.Helpers;
using PriceRater.Common.Models;
using PriceRater.DataAccess.Interfaces;

namespace PriceRater.API.Authentication.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService; 

        public AuthController(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO registerDTO )
        {
            var user = new User
            {
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password)
            };

            return Created("success", _userRepository.Create(user));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var user = _userRepository.GetUserByEmail(loginDTO.Email);

            if (user == null) return BadRequest(new { message = "Invalid Credentials" });

            if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                return BadRequest(new { message = "Invalid credentials" });
            }

            var jwtToken = _jwtService.GenerateJwtToken(user.UserId);

            Response.Cookies.Append("jwtToken", jwtToken, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                message = "success"
            });
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try
            {
                var jwtToken = Request.Cookies["jwtToken"];

                var token = _jwtService.VerifyJwtToken(jwtToken);

                int userId = int.Parse(token.Issuer);

                var user = _userRepository.GetUserById(userId);

                return Ok(user);
            }
            catch(Exception ex)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwtToken");

            return Ok(new
            {
                message = "success"
            });
        }
    }
}
