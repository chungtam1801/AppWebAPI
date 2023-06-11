using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AppData.ViewModels;

namespace AppView.Controllers
{
    public class ThanhToanController : Controller
    {
        private readonly HttpClient _httpClient;
        public ThanhToanController()
        {
            _httpClient = new HttpClient();
        }
        public async Task<IActionResult> GetAllHoaDonAsync()
        {
            string apiUrl = "https://localhost:7021/api/HoaDon";
            var response = await _httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var sanPhams = JsonConvert.DeserializeObject<List<HoaDon>>(apiData);
            return View(sanPhams);
        }
        [HttpPost]
        public async Task<IActionResult> CreateHoaDonAsync(HoaDonViewModel hoaDonViewModel)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7021/api/HoaDon/create-hoaDon",hoaDonViewModel);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else return BadRequest();
        }
    }
}
