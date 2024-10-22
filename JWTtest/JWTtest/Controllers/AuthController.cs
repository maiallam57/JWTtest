using System.Threading.Tasks;
using JWTtest.Models;
using JWTtest.Services;
using Microsoft.AspNetCore.Mvc;

namespace JWTtest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authServices.RegisteraAsync(model);
            if(!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            return Ok(new {token = result.Token , expireson = result.ExpiresOn ,
                user_Email = result.Email , user_name = result.UserName});
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] TokenRequestModel model)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Call the authentication service to log in the user
            var result = await _authServices.LoginAsync(model);

            // Check if authentication was successful
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message); // Return error message
            }

            // If successful, return the token and user details
            return Ok(result.Token);
        }


        [HttpPost("Token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authServices.GetTokenAsync(model);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            // Redirect back to App 2 with the JWT token as a query parameter
            string redirectUrl = $"https://localhost:7118/api/SecureData/login?token={result.Token}";
            return Redirect(redirectUrl);
        }


        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authServices.AddRoleAsync(model);
            if (!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }
            return Ok(model);
        }
    }
}
