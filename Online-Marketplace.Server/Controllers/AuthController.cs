using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineMarketplace.Server.Data;
using OnlineMarketplace.Server.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMarketplace.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthController(UserDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        // ✅ User Registration (POST: api/Auth/register)
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _dbContext.Users.AnyAsync(u => u.Email == user.Email))
                return BadRequest(new { message = "Email is already registered." });

            user.Password = HashPassword(user.Password); // ✅ Hash password before storing
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "User registered successfully!" });
        }

        // ✅ User Login (POST: api/Auth/login) – Only Requires Email & Password
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
            if (user == null || !VerifyPassword(loginRequest.Password, user.Password))
                return Unauthorized(new { message = "Invalid credentials" });

            string token = GenerateJwtToken(user);

            return Ok(new { message = "Login successful!", userId = user.Id, token });
        }

        // ✅ Hash Password using SHA256
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        // ✅ Verify Password Hash
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            return HashPassword(enteredPassword) == storedHash;
        }

        // ✅ Generate JWT Token
        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("userId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // ✅ Separate DTO for Login Request
    public class LoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
