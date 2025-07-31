using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MauiLoginAPI.Models;
using MauiLoginAPI.Data;

namespace MauiLoginAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            if (user == null)
                return BadRequest("User data is required.");

            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
                return BadRequest("Username and password must be provided.");

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);

            if (existingUser == null)
                return Unauthorized("Invalid credentials");

            return Ok("Login successful");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (user == null)
                return BadRequest("User data is required.");

            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
                return BadRequest("Username and password must be provided.");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }
    }
}
