using Microsoft.AspNetCore.Identity;

namespace ShopAppDll.Entities
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
