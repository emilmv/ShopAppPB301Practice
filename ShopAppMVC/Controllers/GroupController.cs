using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopAppMVC.ViewModels;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ShopAppMVC.Controllers
{
    public class GroupController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using var client=new HttpClient();
            client.BaseAddress = new Uri("yourBaseAddress");
            using HttpResponseMessage response = await client.GetAsync("group");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var groupReturnDTOs=JsonConvert.DeserializeObject<List<GroupReturnVM>>(data);
                return View(groupReturnDTOs);
            }

            return View();
        }
    }
}
