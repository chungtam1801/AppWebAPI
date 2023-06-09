using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class ThanhToanController : Controller
    {
        public async Task<IActionResult> GetAllHoaDonAsync()
        {
            HttpClient httpClient = new HttpClient();
            string apiUrl = "https://localhost:7021/api/HoaDon";
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var SanPhams = JsonConvert.DeserializeObject<List<HoaDon>>(apiData);
            return View(SanPhams);
        }
        public IActionResult Payment(Guid idGioHang)
        {
            //HttpClient httpClient = new HttpClient();
            //string apiUrl = "https://localhost:7021/api/HoaDon";
            //var response = await httpClient.GetAsync(apiUrl);
            //string apiData = await response.Content.ReadAsStringAsync();
            //var SanPhams = JsonConvert.DeserializeObject<List<HoaDon>>(apiData);
            return View();
        }
    }
}
