using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using AppView.IServices;
using AppView.Services;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class ThanhToanController : Controller
    {
        private readonly IHoaDonService _iHoaDonService;
        public ThanhToanController()
        {
            _iHoaDonService = new HoaDonService();
        }
        public IActionResult GetAllHoaDonAsync()
        {
            List<HoaDon>? lst = _iHoaDonService.GetAllHoaDonAsync();
            return View();
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
