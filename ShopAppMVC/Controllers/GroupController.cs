using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopAppMVC.ViewModels;
using System.Net.Http.Headers;

namespace ShopAppMVC.Controllers
{
    public class GroupController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("yourBaseAddress");
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(Request.Cookies["token"]);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenResponse.Token);
            using HttpResponseMessage response = await client.GetAsync("group");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var groupReturnDTOs = JsonConvert.DeserializeObject<List<GroupReturnVM>>(data);
                return View(groupReturnDTOs);
            }
            return BadRequest();
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id == null) return BadRequest();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("yourBaseAddress");
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(Request.Cookies["token"]);
            client.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue("Bearer", tokenResponse.Token);
            using HttpResponseMessage response = await client.GetAsync($"group/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var groupReturnDTO = JsonConvert.DeserializeObject<GroupReturnVM>(data);
                return View(new GroupUpdateVM() { Limit = groupReturnDTO.Limit, Name = groupReturnDTO.Name });
            }
            return BadRequest();
        }
        public async Task<IActionResult> Update(int id, GroupUpdateVM groupUpdateVM)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("yourBaseAddress");
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(Request.Cookies["token"]);
            client.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue("Bearer", tokenResponse.Token);
            StringContent content = default;
            if (groupUpdateVM.File == null)
            {
                var stringData = JsonConvert.SerializeObject(groupUpdateVM);
                content = new StringContent(stringData, System.Text.Encoding.Default, "application/json");
            }
            var response = await client.PostAsync("youraddress/api/auth/login", content);
            return View();
        }
    }
}
