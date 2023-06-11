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

        // Them nhanh gia tri (cha cua chi tiet bien the)
        public async Task<IActionResult> CreateGiaTri()
        {
            string apiUrlThuoctinh = "https://localhost:7021/api/ThuocTinh";
            var responseTT = await httpClients.GetAsync(apiUrlThuoctinh);
            string apiDataTT = await responseTT.Content.ReadAsStringAsync();
            var thuocTinh = JsonConvert.DeserializeObject<List<ThuocTinh>>(apiDataTT);
            ViewBag.ThuocTinh = thuocTinh;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateGiaTri(GiaTri giaTri)
        {
            var url = $"https://localhost:7021/api/GiaTri/Create_GiaTri?ten={giaTri.Ten}&idThuocTinh={giaTri.IdThuocTinh}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("Create");
            return View();
        }
        //
        // them nhan thuoc tinh (cha cua gia tri)
        public async Task<IActionResult> CreateThuocTinh()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateThuocTinh(ThuocTinh thuoctinh)
        {
            DateTime ngaytao = thuoctinh.NgayTao;
            var url = $"https://localhost:7021/api/ThuocTinh/Create_ThuocTinh?ten={thuoctinh.Ten}&ngaytao={ngaytao.ToString("MM-dd-yyyy")}&trangthai={thuoctinh.TrangThai}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("CreateGiaTri");
            return View();
            //repos.Add(thuoctinh);
            //return RedirectToAction("GetAllThuocTinh");
        }

        //
        // Them Nhanh Bien The( cha cua chi tiet bien the)
        public async Task<IActionResult> CreateBienThe()
        {
            //SanPham Getall
            string apiUrlsp = "https://localhost:7021/api/SanPham";
            var responsesp = await httpClients.GetAsync(apiUrlsp);
            string apiDatasp = await responsesp.Content.ReadAsStringAsync();
            var SanPham = JsonConvert.DeserializeObject<List<SanPham>>(apiDatasp);
            ViewBag.SanPham = SanPham;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBienThe(BienThe bienthe, IFormFile Anh)
        {
            if (Anh != null && Anh.Length > 0) // Kiểm tra đường dẫn phù hợp
            {
                // thực hiện việc sao chép ảnh đó vào wwwroot
                // Tạo đường dẫn tới thư mục sao chép (nằm trong root)
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                    "img", Anh.FileName); // abc/wwwroot/images/xxx.png
                var stream = new FileStream(path, FileMode.Create); // Tạo 1 filestream để tạo mới
                Anh.CopyTo(stream); // Copy ảnh vừa dc chọn vào đúng cái stream đó
                // Gán lại giá trị link ảnh (lúc này đã nằm trong root cho thuộc tính description)
                bienthe.Anh = Anh.FileName;
            }
            string apiUrlsp = "https://localhost:7021/api/SanPham";
            var responsesp = await httpClients.GetAsync(apiUrlsp);
            string apiDatasp = await responsesp.Content.ReadAsStringAsync();
            var SanPham = JsonConvert.DeserializeObject<List<SanPham>>(apiDatasp);
            ViewBag.SanPham = SanPham;
            DateTime ngaytao = bienthe.NgayTao;

            var url = $"https://localhost:7021/api/BienThe/create-bien-the?IdSanPham={bienthe.IDSanPham}&SoLuong={bienthe.SoLuong}&GiaBan={bienthe.GiaBan}&NgayTao={ngaytao.ToString("MM-dd-yyyy")}&Anh={bienthe.Anh}";
            var response = await httpClients.PostAsync(url, null);
            string apiData = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode) return RedirectToAction("Create");
            return View();
        }
        //
        // Them nhanh San Pham ( cha cua bien the)
        public async Task<IActionResult> CreateSanPham()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSanPham(SanPham sanpham)
        {
            var url = $"https://localhost:7021/api/SanPham/create-san-pham?ten={sanpham.Ten}&idLoaiSP={sanpham.IDLoaiSP}&moTa={sanpham.MoTa}&trangthai={sanpham.TrangThai}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("CreateBienThe");
            return View();
        }
        //
        // Them nhanh LoaiSp (cha cua San Pham)
        public async Task<IActionResult> CreateLoaiSP()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateLoaiSP(LoaiSP loaiSP)
        {

            var url = $"https://localhost:7021/api/LoaiSP/create-loaisp?ten={loaiSP.Ten}&idLoaiSPCha={loaiSP.IDLoaiSPCha}&trangthai={loaiSP.TrangThai}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("CreateSanPham");
            return View();
        }
        //
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
