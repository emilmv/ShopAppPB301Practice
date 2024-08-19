using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopAppMVC.ViewModels;

namespace ShopAppMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            using var client=new HttpClient();
            var stringData = JsonConvert.SerializeObject(loginVM);
            var content=new StringContent(stringData);
            var response= await client.PostAsync("", content);
            if (response.IsSuccessStatusCode)
            {
                var dataFromApi=await response.Content.ReadAsStringAsync();
                var tokenResponse=JsonConvert.DeserializeObject<TokenResponse>(dataFromApi);
                Response.Cookies.Append("token", JsonConvert.SerializeObject(tokenResponse));
            }
            else
            {
                throw new Exception("failed");
            }
            return View();
        }
    }
}
