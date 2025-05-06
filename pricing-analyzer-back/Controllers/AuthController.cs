using Microsoft.AspNetCore.Mvc;
using pricing_analyzer_back.Infrasctructure.Models.Dto;
using pricing_analyzer_back.Infrasctructure.Models;
using System;
using pricing_analyzer_back.Infrasctructure.Context;
using Microsoft.EntityFrameworkCore;

namespace pricing_analyzer_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context) => _context = context;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized();

            return Ok(new { user.Id, user.Username, user.Role });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
                return BadRequest("Пользователь уже существует");

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "user"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }
    }
}
