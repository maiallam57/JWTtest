using AskMawaddaJWT.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace AskMawaddaJWT.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SecureDataController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public SecureDataController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromQuery] TokenRequestModel model)
    {
        var client = _httpClientFactory.CreateClient();

        // Serialize the model to JSON
        var jsonContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

        // Make a POST request to App 1's registration endpoint
        var response = await client.PostAsync(_configuration["App1:LoginUrl"], jsonContent);


        // Handle the response from App 1
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            HttpContext.Response.Cookies.Append("JWTToken", responseData); // Assuming responseData contains the JWT
            return RedirectToAction(nameof(GetProtectedData)); // Return the success response from App 1
        }
        else
        {
            var errorData = await response.Content.ReadAsStringAsync();
            return BadRequest(errorData); // Return the error from App 1
        }
    }


    [HttpPost("register")]
    public async Task<IActionResult> RedirectToRegister([FromBody] RegisterModel model)
    {
        var client = _httpClientFactory.CreateClient();

        // Serialize the model to JSON
        var jsonContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

        // Make a POST request to App 1's registration endpoint
        var response = await client.PostAsync(_configuration["App1:RegistrationUrl"], jsonContent);

        // Handle the response from App 1
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            return Ok(responseData); // Return the success response from App 1
        }
        else
        {
            var errorData = await response.Content.ReadAsStringAsync();
            return BadRequest(errorData); // Return the error from App 1
        }
    }

    [HttpGet]
    public IActionResult GetProtectedData()
    {
        var token = HttpContext.Request.Cookies["JWTToken"];

        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("No token found, please log in.");
        }

        // Proceed with the protected data
        return Ok(new { data = "This is protected data", token });
    }


}
