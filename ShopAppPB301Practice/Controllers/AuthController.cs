using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopAppDll.Entities;
using ShopAppPB301Practice.DTOs.UserDTOs;

namespace ShopAppPB301Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            AppUser user=await _userManager.FindByNameAsync(loginDTO.Username);
            if(user is null) return NotFound();
            var success=await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if(!success) return BadRequest();
            var userRoles=await _userManager.GetRolesAsync(user);



            return Ok(new {token=""});
        }
    }
}
