using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AppView.Controllers
{
    public class HomeController : Controller
    {
        Uri urlapi = new Uri("https://localhost:7021/api");
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = urlapi;
        }

        public IActionResult Index()
        {
            return View();
        }
        public List<LoaiSP> LoadLoaiSP()
        {
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/LoaiSP").Result;
            List<LoaiSP> ListLoaiSP = new List<LoaiSP>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ListLoaiSP = JsonConvert.DeserializeObject<List<LoaiSP>>(data);
            }

            return ListLoaiSP;
        }

        // HIỂN THỊ DANH SÁCH BIẾN THỂ ĐẦU TIÊN CỦA TỪNG SẢN PHẨM SẢN PHẨM 
        [HttpGet]
        public async Task<IActionResult> Shop()
        {
            List<SanPham> listSP = new List<SanPham>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/SanPham").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listSP = JsonConvert.DeserializeObject<List<SanPham>>(data);

                // Lấy danh sách biến thể sản phẩm
                List<BienThe> listBT = new List<BienThe>();
                string urlBienThe = $"https://localhost:7021/api/BienThe";
                var httpClient = new HttpClient();
                HttpResponseMessage btresponse = await httpClient.GetAsync(urlBienThe);
                string apiData = await btresponse.Content.ReadAsStringAsync();
                // Lấy kết quả thu được bằng cách bóc dữ liệu Json
                List<BienThe> result = JsonConvert.DeserializeObject<List<BienThe>>(apiData);
                ViewData["listBienThe"] = result;
                ViewData["listLoaiSP"] = LoadLoaiSP();
            }
            return View(listSP);
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ProductDetail()
        {
            return View();
        }
        public IActionResult CheckOut()
        {
            return View();
        }
        public IActionResult BlogDetails()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}