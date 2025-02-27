﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly TodoContext _context; // Replace YourDbContext with your actual DbContext

        public UsersController(IConfiguration configuration, TodoContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreateDto userCreateDto)
        {
            // Check if username is already taken
            var existingUser = await _context.MK_Users.FirstOrDefaultAsync(u => u.Username == userCreateDto.Username || u.Email == userCreateDto.Email);
            if (existingUser != null)
            {
                return BadRequest("Username or email already exists");
            }

            // Hash the password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);

            // Create new user entity
            var newUser = new User
            {
                Username = userCreateDto.Username,
                Email = userCreateDto.Email,
                PasswordHash = passwordHash // Store hashed password
                // Add other properties as needed
            };

            // Save user to the database
            _context.MK_Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            // Retrieve the user from database
            var user = await _context.MK_Users.FirstOrDefaultAsync(u => u.Username == userLoginDto.Username);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Validate password
            if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid credentials");
            }

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"] ?? string.Empty); // Secret key from appsettings.json
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                    // Add more claims as needed (e.g., roles, permissions)
                }),
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:ExpirationDays"])), // Token expiration time from appsettings.json
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }
}
