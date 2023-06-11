using AppData.Models;
using AppView.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace AppView.Controllers
{
    public class ChiTietBienTheController : Controller
    {
        private readonly HttpClient httpClients;
        public ChiTietBienTheController()
        {
            httpClients = new HttpClient();
        }
        public IActionResult getID()
        {
            return RedirectToAction("Create");
        }
        public async Task<IActionResult> GetAllChiTietBienThe()
        {
            string apiUrl = "https://localhost:7021/api/ChiTietBienThe";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var CTBienThe = JsonConvert.DeserializeObject<List<ChiTietBienThe>>(apiData);
            ViewBag.CTBienThe = CTBienThe;
            //Giatri LoaiSP
            string apiUrlLoaiSP = "https://localhost:7021/api/LoaiSP";
            var responseLoaiSP = await httpClients.GetAsync(apiUrlLoaiSP);
            string apiDataLoaiSP = await responseLoaiSP.Content.ReadAsStringAsync();
            var loaiSP = JsonConvert.DeserializeObject<List<LoaiSP>>(apiDataLoaiSP);
            ViewBag.LoaiSP = loaiSP;
            //Giatri ThuocTinh
            string apiUrlThuocTinh = "https://localhost:7021/api/ThuocTinh";
            var responseThuocTinh = await httpClients.GetAsync(apiUrlThuocTinh);
            string apiDataThuocTinh = await responseThuocTinh.Content.ReadAsStringAsync();
            var thuocTinh = JsonConvert.DeserializeObject<List<ThuocTinh>>(apiDataThuocTinh);
            ViewBag.ThuocTinh = thuocTinh;
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

            return View(BienThe);
        }
        public async Task<IActionResult> Create()
        {
            string apiUrl = "https://localhost:7021/api/ChiTietBienThe";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var CTBienThe = JsonConvert.DeserializeObject<List<ChiTietBienThe>>(apiData);
            ViewBag.CTBienThe = CTBienThe;
            //Giatri LoaiSP
            string apiUrlLoaiSP = "https://localhost:7021/api/LoaiSP";
            var responseLoaiSP = await httpClients.GetAsync(apiUrlLoaiSP);
            string apiDataLoaiSP = await responseLoaiSP.Content.ReadAsStringAsync();
            var loaiSP = JsonConvert.DeserializeObject<List<LoaiSP>>(apiDataLoaiSP);
            ViewBag.LoaiSP = loaiSP;
            //Giatri ThuocTinh
            string apiUrlThuocTinh = "https://localhost:7021/api/ThuocTinh";
            var responseThuocTinh = await httpClients.GetAsync(apiUrlThuocTinh);
            string apiDataThuocTinh = await responseThuocTinh.Content.ReadAsStringAsync();
            var thuocTinh = JsonConvert.DeserializeObject<List<ThuocTinh>>(apiDataThuocTinh);
            ViewBag.ThuocTinh = thuocTinh;
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
        public async Task<IActionResult> Create(AllSanPhamBienTheViewModel sanpham, IFormFile Anh,List<Guid> IDGiaTri)
        {
            //ctbt
            string apiUrlCTBT = "https://localhost:7021/api/ChiTietBienThe";
            var responseCTBT = await httpClients.GetAsync(apiUrlCTBT);
            string apiDataCTBT = await responseCTBT.Content.ReadAsStringAsync();
            var CTBienThe = JsonConvert.DeserializeObject<List<ChiTietBienThe>>(apiDataCTBT);
            ViewBag.CTBienThe = CTBienThe;
            // LoaiSP
            string apiUrlLoaiSP = "https://localhost:7021/api/LoaiSP";
            var responseLoaiSP = await httpClients.GetAsync(apiUrlLoaiSP);
            string apiDataLoaiSP = await responseLoaiSP.Content.ReadAsStringAsync();
            var loaiSP = JsonConvert.DeserializeObject<List<LoaiSP>>(apiDataLoaiSP);
            ViewBag.LoaiSP = loaiSP;
            // ThuocTinh
            string apiUrlThuocTinh = "https://localhost:7021/api/ThuocTinh";
            var responseThuocTinh = await httpClients.GetAsync(apiUrlThuocTinh);
            string apiDataThuocTinh = await responseThuocTinh.Content.ReadAsStringAsync();
            var thuocTinh = JsonConvert.DeserializeObject<List<ThuocTinh>>(apiDataThuocTinh);
            ViewBag.ThuocTinh = thuocTinh;
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
            //them sp
                Guid idSP = Guid.NewGuid(); 
                var urlthemSP = $"https://localhost:7021/api/SanPham/create-san-pham?id={idSP}&ten={sanpham.Ten}&idLoaiSP={sanpham.IDLoaiSP}&moTa={sanpham.MoTa}&trangthai={sanpham.TrangThai}";
                var responsethemSP = await httpClients.PostAsync(urlthemSP, null);
                string aapiDatasp = await responsesp.Content.ReadAsStringAsync();
                
            //them bienthe
            if (Anh != null && Anh.Length > 0) // Kiểm tra đường dẫn phù hợp
            {
                // thực hiện việc sao chép ảnh đó vào wwwroot
                // Tạo đường dẫn tới thư mục sao chép (nằm trong root)
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                    "img", Anh.FileName); // abc/wwwroot/images/xxx.png
                var stream = new FileStream(path, FileMode.Create); // Tạo 1 filestream để tạo mới
                Anh.CopyTo(stream); // Copy ảnh vừa dc chọn vào đúng cái stream đó
                // Gán lại giá trị link ảnh (lúc này đã nằm trong root cho thuộc tính description)
                sanpham.Anh = Anh.FileName;
            }
            
            DateTime ngaytao = sanpham.NgayTao;
            Guid idBienThe = Guid.NewGuid();
            var urlBienThe = $"https://localhost:7021/api/BienThe/create-bien-the?id={idBienThe}&IdSanPham={idSP}&SoLuong={sanpham.SoLuong}&GiaBan={sanpham.GiaBan}&NgayTao={ngaytao.ToString("MM-dd-yyyy")}&Anh={sanpham.Anh}";
            var responseBienThe = await httpClients.PostAsync(urlBienThe, null);
            //List<Guid> idgiatri = ViewBag.IDgiatri;
            //return Content(idgiatri.ToString());
            foreach (var item in IDGiaTri)
            {
                var url = $"https://localhost:7021/api/ChiTietBienThe/create-CTBienThe?idBienThe={idBienThe}&idGiaTri={item}";
                var response = await httpClients.PostAsync(url, null);
                if (!response.IsSuccessStatusCode)
                {
                    return View();
                }
            }
            
             return RedirectToAction("GetAllChiTietBienThe");
            
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
