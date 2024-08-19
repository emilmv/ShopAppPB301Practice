using ShopAppDll.Entities;
using ShopAppPB301Practice.Settings;

namespace ShopAppPB301Practice.Services.Interfaces
{
    public interface ITokenService
    {
        string GetToken(IList<string>userRoles,AppUser user, JwtSettings jwtSettings);
    }
}
