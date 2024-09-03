using caselibrary.Models;
using caselibrary.repos;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore; // Required for async methods
using BCrypt.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        private readonly RetailapplicationContext _context;

        public RegisterController(RetailapplicationContext context)
        {
            _context = context;
        }
        // GET: api/<RegisterController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }




        // Import the BCrypt.Net namespace

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] Usertable userDto)
        {
            // Check if the email already exists
            var existingUser = await _context.Usertables
                .FirstOrDefaultAsync(u => u.Email == userDto.Email);

            if (existingUser != null)
            {
                return BadRequest("Email already exists.");
            }

            // Hash the password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.upassword);

            // Create a new user
            var newUser = new Usertable
            {
                Firstname = userDto.Firstname,
                Lastname = userDto.Lastname,
                Email = userDto.Email,
                upassword = hashedPassword 
            };

            _context.Usertables.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

    }

}

