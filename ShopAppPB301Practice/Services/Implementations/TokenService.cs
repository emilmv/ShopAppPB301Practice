using Microsoft.IdentityModel.Tokens;
using ShopAppDll.Entities;
using ShopAppPB301Practice.Services.Interfaces;
using ShopAppPB301Practice.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopAppPB301Practice.Services.Implementations
{
    public class TokenService : ITokenService
    {
        public string GetToken(IList<string> userRoles, AppUser user, JwtSettings _jwtSettings)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("Full Name",user.FullName)
            };
            claims.AddRange(userRoles.Select(r => new Claim(ClaimTypes.Role, r)).ToList());
            var audience = _jwtSettings.Audience;
            var issuer = _jwtSettings.Issuer;

            var secretToken = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(secretToken);
            return token;
        }
    }
}
