using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using todoAPI.Data;
using todoAPI.Dtos.User;
using todoAPI.Models;

namespace todoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (await _context.Users.AnyAsync(x => x.Email == dto.Email))
            return BadRequest(new { message = "Email já cadastrado" });
        
        if (await _context.Users.AnyAsync(x => x.Username == dto.Username))
            return BadRequest(new { message = "Usuário já cadastrado" });
        
        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Usuário registrado com sucesso" });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
    
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
    
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized(new { message = "Credenciais inválidas" });

        var token = GenerateJwtToken(user);

        return Ok(new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            Email = user.Email
        });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}