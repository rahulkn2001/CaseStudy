using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using caselibrary.Models;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly RetailapplicationContext _context;
    private readonly IConfiguration _config;

    public AuthController(RetailapplicationContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost]
    [Route("authenticate")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] Usertable user)
    {
        // Log incoming user data for debugging
        Console.WriteLine($"Authenticating User: {user.UserId}");

        // Check for user existence and password match
        var dbUser = await _context.Usertables
            .FirstOrDefaultAsync(u => u.Email == user.Email);

        if (dbUser == null)
        {
            return Unauthorized("Invalid username or password");
        }

        // Verify password
        bool passwordMatch = BCrypt.Net.BCrypt.Verify(user.upassword, dbUser.upassword);

        if (passwordMatch)
        {
            var key = Encoding.UTF8.GetBytes(_config["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, dbUser.UserId.Value.ToString()),
                // Add other claims if needed
            }),
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Create the response object
            var response = new AuthResponse
            {
                Token = tokenString,
                UserId = dbUser.UserId.Value.ToString()
            };

            return Ok(response);
        }
        else
        {
            return Unauthorized("Invalid username or password");
        }
    }
    public class AuthResponse
    {
        public string Token { get; set; }
        public string UserId { get; set; }
    }

}
