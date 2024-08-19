using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShopAppDll.Entities;
using ShopAppPB301Practice.DTOs.UserDTOs;
using ShopAppPB301Practice.Services.Interfaces;
using ShopAppPB301Practice.Settings;

namespace ShopAppPB301Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly JwtSettings _jwtSettings;
        private readonly IEmailService _emailService;
        public AuthController(IOptions<JwtSettings> jwtSettings, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _jwtSettings = jwtSettings.Value;
            _emailService = emailService;
        }
        [HttpGet("verifyEmail")]
        public async Task<IActionResult> VerifyEmail(string token, string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound();
            await _userManager.ConfirmEmailAsync(user, token);
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            AppUser user = await _userManager.FindByNameAsync(registerDTO.Username);
            if (user != null) return Conflict();
            user = new()
            {
                FullName = registerDTO.FullName,
                UserName = registerDTO.Username,
                Email = registerDTO.Email
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "User");
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string link = Url.Action(nameof(VerifyEmail), "Account", new { email = user.Email, token = token }, Request.Scheme, Request.Host.ToString());
            string body = string.Empty;
            using (StreamReader streamReader = new("wwwroot/templates/VerifyEmailTemplate.html"))
            {
                body = streamReader.ReadToEnd();
            }
            body = body.Replace("{{link}}", link);
            body = body.Replace("{{username}}", user.UserName);
            _emailService.SendEmail(new List<string>() { user.Email }, "Verify Email", body);
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDTO.Username);
            if (user is null) return NotFound();
            var success = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!user.EmailConfirmed) return BadRequest("Email confirmation required");
            if (!success) return BadRequest();
            var userRoles = await _userManager.GetRolesAsync(user);

            return Ok(new { token = _tokenService.GetToken(userRoles, user, _jwtSettings) });
        }
        [HttpGet("")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return Ok(new { id = user.Id, Name = user.UserName });
        }
        [HttpGet("users")]
        public IActionResult Get()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }
        [HttpPost("resetPasswordSendEmail")]
        public async Task<IActionResult> ResetPassword(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user is null) return NotFound();
            var token = _userManager.GeneratePasswordResetTokenAsync(user);
            string url = Url.Action(nameof(ResetPassword), "Account", new { email = user.Email, token }, Request.Scheme, Request.Host.ToString());
            string body = string.Empty;
            using (StreamReader streamReader = new("wwwroot/templates/forgotPassword.html"))
            {
                body = streamReader.ReadToEnd();
            };
            body = body.Replace("{{link}}", url);
            body = body.Replace("{{username}}", user.UserName);
            _emailService.SendEmail(new List<string>() { user.Email }, "Reset Password", body);
            return Ok();
        }
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(string email, string token, [FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user is null) return NotFound();
            var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordDTO.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await _userManager.UpdateSecurityStampAsync(user);
            return Ok();
        }
    }
}
