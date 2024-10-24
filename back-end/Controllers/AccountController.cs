using Microsoft.AspNetCore.Mvc;
using back_end.Interfaces.UserInterfaces;
using System.Security.Claims;
using back_end.DTOs.UserDTOs;
using back_end.Models.User;
using back_end.Utils;
using Microsoft.AspNetCore.Authorization;

namespace back_end.Controllers
{
    [ApiController]
    [Route("apiv1/[controller]")]
    public class AccountController(IAccountService accountService, JwtTokenGenerator jwtTokenGenerator) : ControllerBase
    {
        //Khai bao IAccountService de su dung cac ham trong AccountService
        private readonly IAccountService _accountService = accountService;
        //Khai bao JwtTokenGenerator de su dung ham GenerateJwtToken
        private readonly JwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

        //Dung util de check xem input co chua sql injection khong
        private BadRequestObjectResult? ValidateInput(RegisterDTO registerDTO)
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

        private BadRequestObjectResult? ValidateInput(LoginDTO loginDTO)
        {
            if (InputValidator.ContainsSqlInjection(loginDTO.Username) ||
                InputValidator.ContainsSqlInjection(loginDTO.Password))
            {
                return BadRequest("Invalid input detected.");
            }

            return null;
        }

        private BadRequestObjectResult? ValidateInput(ChangePasswordDTO changePasswordDTO)
        {
            if (InputValidator.ContainsSqlInjection(changePasswordDTO.OldPassword) ||
                InputValidator.ContainsSqlInjection(changePasswordDTO.NewPassword) ||
                InputValidator.ContainsSqlInjection(changePasswordDTO.ConfirmNewPassword))
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
            var newInfor = new UserInfor
            {
                Id = Guid.NewGuid().ToString(),
                Account = newAccount,
                AccountId = newAccount.Id,
                FullName = "New User"
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

        //Signout Method
        [HttpPost("signout")]
        public IActionResult UserSignOut()
        {
            return Ok(new { message = "Sign out successfully." });
        }

        //Change Password Method
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var validationResult = ValidateInput(changePasswordDTO);
            if (validationResult != null)
            {
                return validationResult;
            }

            if (changePasswordDTO.NewPassword == changePasswordDTO.OldPassword)
            {
                return BadRequest("New password and old password are the same.");
            }

            if (changePasswordDTO.NewPassword != changePasswordDTO.ConfirmNewPassword)
            {
                return BadRequest("New password and confirm new password do not match.");
            }

            //Get username from token
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("User not logged in.");
            }

            var account = await _accountService.FindByUsernameAsync(username);
            if (account == null)
            {
                return Unauthorized("Account not found.");
            }

            // Change password
            await _accountService.ChangePasswordAsync(account, changePasswordDTO.NewPassword);
            return Ok(new { message = "Change password successfully." });
        }

        //Get User Information Method
        [Authorize]
        [HttpGet("user-info")]
        public async Task<IActionResult> GetUserInfo()
        {

            return null;
        }

        //Change information Method
        [HttpPost("change-infor")]
        public async Task<IActionResult> ChangeInformation([FromBody] ChangeInformationDTO changeInformationDTO)
        {
            return Ok(new { message = "Change information successfully." });
        }
    }
}