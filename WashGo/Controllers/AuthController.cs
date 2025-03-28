using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WashGo.Model;
using WashGo.Repository;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;

    public AuthController(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        // Validate ConfirmPassword
        if (request.Password != request.ConfirmPassword)
        {
            return BadRequest("Password and Confirm Password do not match.");
        }

        // Check if email already exists
        var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
        if (existingUser != null)
            return BadRequest("Email already exists.");

        // Create new user
        var newUser = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = HashPassword(request.Password),
            PhoneNumber = request.MobileNumber,
            Address = request.Address,
            Role = "Customer",  // You can modify role as per your requirements
            CreatedAt = DateTime.UtcNow
        };

        // Add user to the database
        await _userRepository.AddUserAsync(newUser);
        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // Find user by username
        var user = await _userRepository.GetUserByUsernameAsync(request.Username);
        if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
            return Unauthorized("Invalid username or password.");

        var token = GenerateJwtToken(user);
        return Ok(new { Token = token });
    }

    private string GenerateJwtToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

        if (key.Length < 32)
            throw new ArgumentException("JWT key must be at least 32 characters long.");

        var securityKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role)
    };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password); 
    }

    private bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}