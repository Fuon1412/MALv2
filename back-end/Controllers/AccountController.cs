using Microsoft.AspNetCore.Mvc;
using DTOs.UserDTOs;
using Models.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        // Register Method
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var existingAccount = await _accountService.FindByEmailAsync(registerDTO.Email);
            if (existingAccount != null)
            {
                return BadRequest("Email is already registered.");
            }
            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                return BadRequest("Password and Confirm Password do not match.");
            }

            var newAccount = new Account
            {
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                Status = "active"
            };

            await _accountService.RegisterAsync(newAccount, registerDTO.Password);
            var token = GenerateJwtToken(newAccount);
            return Ok(new { token });
        }

        // Login Method
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var account = await _accountService.FindByEmailAsync(loginDTO.Email);
            if (account == null)
            {
                return BadRequest("Account not found.");
            }

            if (!_accountService.CheckPassword(account, loginDTO.Password))
            {
                return BadRequest("Invalid password.");
            }

            var token = GenerateJwtToken(account);
            return Ok(new { token });
        }


        private string GenerateJwtToken(Account account)
        {
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentNullException(nameof(jwtKey), "JWT Key is not configured properly.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Status", account.Status),
                new Claim("Username", account.Username),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
