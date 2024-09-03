using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.WebUtilities;
using BCrypt;
using EmailVErification.Model;
using SendGrid.Helpers.Mail;
using System.Linq;


namespace ApiCaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly RetailapplicationContext _context;
        private readonly IEmailService _emailService;
        private readonly string _frontendUrl; // Frontend URL to redirect after email verification

        public RegisterController(RetailapplicationContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
            _frontendUrl = "https://yourfrontendurl.com"; // Replace with your actual frontend URL
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] EmailVErification.Model.Usertable userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is required.");
            }

            if (string.IsNullOrEmpty(userDto.Email))
            {
                return BadRequest("Email is required.");
            }

            // Check if the email already exists
            var existingUser = await _context.Usertables
                .FirstOrDefaultAsync(u => u.Email == userDto.Email);

            if (existingUser != null)
            {
                return BadRequest("Email already exists.");
            }

            // Hash the password

            // Generate a new verification token
            Random random = new Random();
            int otpNumber = random.Next(100000, 999999); // Generates a number between 100000 and 999999
            var otp = otpNumber.ToString("D6");
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Upassword);
            // Create a new user
            var newUser = new EmailVErification.Model.Usertable
            {
                Firstname = userDto.Firstname,
                Lastname = userDto.Lastname,
                Email = userDto.Email,
                Upassword = hashedPassword,
                IsEmailVerified = false,
                VerificationToken = otp // Use the generated token
            };


            _context.Usertables.Add(newUser);
            await _context.SaveChangesAsync();

            // Send the verification email
            await _emailService.SendVerificationEmailAsync(userDto.Email, otp);

            return Ok("User registered successfully. Please check your email to verify your account.");
        }

        [HttpGet("verifyemail")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Verification token is required.");
            }

            // Find the user by verification token
            var user = await _context.Usertables
                .FirstOrDefaultAsync(u => u.VerificationToken == token);

            if (user == null)
            {
                return BadRequest("Invalid verification token.");
            }

           
            user.IsEmailVerified = true;
            user.VerificationToken = null; 
            _context.Usertables.Update(user);
            await _context.SaveChangesAsync();

           
            return Redirect($"{_frontendUrl}/email-verified");
        }


        [HttpGet("otp/{email}")]

        public IActionResult GetUser(string email)
        {
            var user = _context.Usertables.FirstOrDefault(e => e.Email == email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                EmailVerified = user.IsEmailVerified,
                OtpToken = user.VerificationToken
            });

        }

        [HttpPut("otp")]
        public async Task<IActionResult> PutOtp([FromRoute] VerifyEmailRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Invalid request.");
            }

            // Find the user by email
            var user = await _context.Usertables
                .FirstOrDefaultAsync(e => e.Email == request.Email);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            
            user.IsEmailVerified = true;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(new { isEmailVerified = user.IsEmailVerified });
        }
        public class VerifyEmailRequest
        {
            public string Email { get; set; }
        }

    }
}




