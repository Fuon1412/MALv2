using Microsoft.AspNetCore.Mvc;
using DTOs.UserDTOs;
using Models.User;
using back_end.Utils;

namespace back_end.Controllers
{
    [ApiController]
    [Route("apiv1/[controller]")]
    public class AccountController(IAccountService accountService, JwtTokenGenerator jwtTokenGenerator) : ControllerBase
    {
        private readonly IAccountService _accountService = accountService;
        private readonly JwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

        //Dung util de check xem input co chua sql injection khong
        private IActionResult? ValidateInput(RegisterDTO registerDTO)
        {
            if (InputValidator.ContainsSqlInjection(registerDTO.Username) ||
                InputValidator.ContainsSqlInjection(registerDTO.Password) ||
                InputValidator.ContainsSqlInjection(registerDTO.Email) ||
                InputValidator.ContainsSqlInjection(registerDTO.ConfirmPassword))
            {
                return BadRequest("Invalid input detected.");
            }

            return null;
        }

        private IActionResult? ValidateInput(LoginDTO loginDTO)
        {
            if (InputValidator.ContainsSqlInjection(loginDTO.Username) ||
                InputValidator.ContainsSqlInjection(loginDTO.Password))
            {
                return BadRequest("Invalid input detected.");
            }

            return null;
        }

        // Register Method
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var validationResult = ValidateInput(registerDTO);
            if (validationResult != null)
            {
                return validationResult;
            }
            var existingAccount = await _accountService.FindByEmailAsync(registerDTO.Email);
            if (existingAccount != null)
            {
                return BadRequest("Email is already registered.");
            }
            else if (await _accountService.FindByUsernameAsync(registerDTO.Username) != null)
            {
                return BadRequest("Username is already taken.");
            }
            else if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                return BadRequest("Password and Confirm Password do not match.");
            }

            var newAccount = new Account
            {
                Id = Guid.NewGuid().ToString(),
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                Password = registerDTO.Password,
                Status = "active"
            };

            await _accountService.RegisterAsync(newAccount, registerDTO.Password);
            var token = _jwtTokenGenerator.GenerateJwtToken(newAccount);
            return Ok(new { token });
        }

        // Login Method
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var validationResult = ValidateInput(loginDTO);
            if (validationResult != null)
            {
                return validationResult;
            }
            var account = await _accountService.FindByEmailAsync(loginDTO.Username);
            if (account == null)
            {
                return BadRequest("Account not found.");
            }

            if (!_accountService.CheckPassword(account, loginDTO.Password))
            {
                return BadRequest("Invalid password.");
            }

            var token = _jwtTokenGenerator.GenerateJwtToken(account);
            return Ok(new { token });
        }
    }
}
