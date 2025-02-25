using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using OnlineMarketplace.Server.Data;
using OnlineMarketplace.Server.Models;
using OnlineMarketplace.Server.Interface;

namespace Online_Marketplace.Server.Services
{
	public class UserService : IUserService
	{
		private readonly UserDbContext _dbContext;
		private readonly IConfiguration _configuration;

		public UserService(UserDbContext dbContext, IConfiguration configuration)
		{
			_dbContext = dbContext;
			_configuration = configuration;
		}

		public async Task<ServiceResult> RegisterAsync(UserRegisterDto dto)
		{
			if (dto.Password != dto.ConfirmPassword)
			{
				return new ServiceResult { Success = false, Message = "Passwords do not match." };
			}

			// Check if user already exists
			if (await _dbContext.Users.AnyAsync(u => u.Email == dto.Email))
			{
				return new ServiceResult { Success = false, Message = "User with this email already exists." };
			}

			// Hash the password (using SHA256 here for simplicity; consider using BCrypt in production)
			var passwordHash = ComputeSha256Hash(dto.Password);

			var user = new User
			{
				Email = dto.Email,
                Password = passwordHash
			};

			_dbContext.Users.Add(user);
			await _dbContext.SaveChangesAsync();

			return new ServiceResult { Success = true, Message = "User registered successfully." };
		}

		public async Task<string> AuthenticateAsync(UserLoginDto dto)
		{
			var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
			if (user == null)
			{
				return null;
			}

			var passwordHash = ComputeSha256Hash(dto.Password);
			if (user.Password != passwordHash)
			{
				return null;
			}

			// Generate JWT token
			return GenerateJwtToken(user);
		}

		private string ComputeSha256Hash(string rawData)
		{
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// ComputeHash returns byte array
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

				// Convert byte array to a string
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}

		private string GenerateJwtToken(User user)
		{
			// Get the JWT secret from configuration
			var secret = _configuration["Jwt:Secret"];
			if (string.IsNullOrEmpty(secret))
			{
				throw new Exception("JWT Secret is not configured.");
			}
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			// Create token options
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(JwtRegisteredClaimNames.Sub, user.Email),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					new Claim("UserId", user.Id.ToString())
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = creds
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
