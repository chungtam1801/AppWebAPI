using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class ChiTietBienTheController : Controller
    {
        private readonly HttpClient httpClients;
        public ChiTietBienTheController()
        {
            httpClients = new HttpClient();
        }
        public async Task<IActionResult> GetAllChiTietBienThe()
        {
            string apiUrl = "https://localhost:7021/api/ChiTietBienThe";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var CTBienThe = JsonConvert.DeserializeObject<List<ChiTietBienThe>>(apiData);

            //Giatri getall
            string apiUrlGiaTri = "https://localhost:7021/api/GiaTri";
            var responseGT = await httpClients.GetAsync(apiUrlGiaTri);
            string apiDataGT = await responseGT.Content.ReadAsStringAsync();
            var giaTri = JsonConvert.DeserializeObject<List<GiaTri>>(apiDataGT);
            ViewBag.GiaTri = giaTri;
            //BienThe getall
            string apiUrlBT = "https://localhost:7021/api/BienThe";
            var responseBT = await httpClients.GetAsync(apiUrlBT);
            string apiDataBT = await responseBT.Content.ReadAsStringAsync();
            var BienThe = JsonConvert.DeserializeObject<List<BienThe>>(apiDataBT);
            ViewBag.BienThe = BienThe;
            //SanPham Getall
            string apiUrlsp = "https://localhost:7021/api/SanPham";
            var responsesp = await httpClients.GetAsync(apiUrlsp);
            string apiDatasp = await responsesp.Content.ReadAsStringAsync();
            var SanPham = JsonConvert.DeserializeObject<List<SanPham>>(apiDatasp);
            ViewBag.SanPham = SanPham;

            return View(CTBienThe);
        }
        public async Task<IActionResult> Create()
        {
            //Giatri getall
            string apiUrlGiaTri = "https://localhost:7021/api/GiaTri";
            var responseGT = await httpClients.GetAsync(apiUrlGiaTri);
            string apiDataGT = await responseGT.Content.ReadAsStringAsync();
            var giaTri = JsonConvert.DeserializeObject<List<GiaTri>>(apiDataGT);
            ViewBag.GiaTri = giaTri;
            //BienThe getall
            string apiUrlBT = "https://localhost:7021/api/BienThe";
            var responseBT = await httpClients.GetAsync(apiUrlBT);
            string apiDataBT = await responseBT.Content.ReadAsStringAsync();
            var BienThe = JsonConvert.DeserializeObject<List<BienThe>>(apiDataBT);
            ViewBag.BienThe = BienThe;
            //SanPham Getall
            string apiUrlsp = "https://localhost:7021/api/SanPham";
            var responsesp = await httpClients.GetAsync(apiUrlsp);
            string apiDatasp = await responsesp.Content.ReadAsStringAsync();
            var SanPham = JsonConvert.DeserializeObject<List<SanPham>>(apiDatasp);
            ViewBag.SanPham = SanPham;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ChiTietBienThe chiTietBienThe)
        {
            //Giatri getall
            string apiUrlGiaTri = "https://localhost:7021/api/GiaTri";
            var responseGT = await httpClients.GetAsync(apiUrlGiaTri);
            string apiDataGT = await responseGT.Content.ReadAsStringAsync();
            var giaTri = JsonConvert.DeserializeObject<List<GiaTri>>(apiDataGT);
            ViewBag.GiaTri = giaTri;
            //BienThe getall
            string apiUrlBT = "https://localhost:7021/api/BienThe";
            var responseBT = await httpClients.GetAsync(apiUrlBT);
            string apiDataBT = await responseBT.Content.ReadAsStringAsync();
            var BienThe = JsonConvert.DeserializeObject<List<BienThe>>(apiDataBT);
            ViewBag.BienThe = BienThe;
            //SanPham Getall
            string apiUrlsp = "https://localhost:7021/api/SanPham";
            var responsesp = await httpClients.GetAsync(apiUrlsp);
            string apiDatasp = await responsesp.Content.ReadAsStringAsync();
            var SanPham = JsonConvert.DeserializeObject<List<SanPham>>(apiDatasp);
            ViewBag.SanPham = SanPham;
            var url = $"https://localhost:7021/api/ChiTietBienThe/create-CTBienThe?idBienThe={chiTietBienThe.IDBienThe}&idGiaTri={chiTietBienThe.IDGiaTri}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllChiTietBienThe");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //Giatri getall
            string apiUrlGiaTri = "https://localhost:7021/api/GiaTri";
            var responseGT = await httpClients.GetAsync(apiUrlGiaTri);
            string apiDataGT = await responseGT.Content.ReadAsStringAsync();
            var giaTri = JsonConvert.DeserializeObject<List<GiaTri>>(apiDataGT);
            ViewBag.GiaTri = giaTri;
            //BienThe getall
            string apiUrlBT = "https://localhost:7021/api/BienThe";
            var responseBT = await httpClients.GetAsync(apiUrlBT);
            string apiDataBT = await responseBT.Content.ReadAsStringAsync();
            var BienThe = JsonConvert.DeserializeObject<List<BienThe>>(apiDataBT);
            ViewBag.BienThe = BienThe;
            //SanPham Getall
            string apiUrlsp = "https://localhost:7021/api/SanPham";
            var responsesp = await httpClients.GetAsync(apiUrlsp);
            string apiDatasp = await responsesp.Content.ReadAsStringAsync();
            var SanPham = JsonConvert.DeserializeObject<List<SanPham>>(apiDatasp);
            ViewBag.SanPham = SanPham;

            var url = $"https://localhost:7021/api/ChiTietBienThe/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var CTBT = JsonConvert.DeserializeObject<ChiTietBienThe>(result);
            return View(CTBT);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ChiTietBienThe CTBT)
        {

            var url =
                $"https://localhost:7021/api/ChiTietBienThe/Update-CTBienThe?id={CTBT.ID}&idBienThe={CTBT.IDBienThe}&idGiaTri={CTBT.IDGiaTri}&trangthai={CTBT.TrangThai}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllChiTietBienThe");
            return View();
        }
        public async Task<IActionResult> Deletes(Guid id)
        {
            var url = $"https://localhost:7021/api/ChiTietBienThe/{id}";
            var response = await httpClients.DeleteAsync(url);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllChiTietBienThe");
            return BadRequest();
        }
    }
}
