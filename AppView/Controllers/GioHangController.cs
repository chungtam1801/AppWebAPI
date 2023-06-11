using AppView.Services;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AppView.Controllers
{
    public class GioHangController : Controller
    {
        Uri urlapi = new Uri("https://localhost:7021/api");
        private readonly HttpClient _httpClient;
        private readonly ILogger<GioHangController> _logger;
        List<ChiTietGioHang> listCTGH = new List<ChiTietGioHang>();
        List<BienThe> listBT = new List<BienThe>();
        List<SanPham> listSP = new List<SanPham>();
        AssignmentDBContext _context = new AssignmentDBContext();
        public GioHangController(ILogger<GioHangController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = urlapi;
            _context = new AssignmentDBContext();

            // List ChitietGioHang
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/ChiTietGioHang").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listCTGH = JsonConvert.DeserializeObject<List<ChiTietGioHang>>(data);
            }
            // List BienThe
            HttpResponseMessage response1 = _httpClient.GetAsync(_httpClient.BaseAddress + "/BienThe").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data1 = response1.Content.ReadAsStringAsync().Result;
                listBT = JsonConvert.DeserializeObject<List<BienThe>>(data1);
            }
            // List SanPham
            HttpResponseMessage response2 = _httpClient.GetAsync(_httpClient.BaseAddress + "/SanPham").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data2 = response2.Content.ReadAsStringAsync().Result;
                listSP = JsonConvert.DeserializeObject<List<SanPham>>(data2);
            }

        }
        // HIỂN THỊ SẢN PHẨM TRONG GIỎ HÀNG
        public IActionResult ShowCart()
        {
            // Nếu chưa đăng nhập thì hiển thị toàn bộ cart trong session
            if (HttpContext.Session.GetString("IdUser") == null)
            {
                var products = GioHangService.GetObjFormSession(HttpContext.Session, "Cart");
                if (products.Count == 0)
                {
                    TempData["Message"] = "Không có sản phẩm nào trong giỏ hàng!";
                }
                return View(products);
            }
            else // Lấy toàn bộ chitietgiohang của người dùng 
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));// Lấy iduser đăng nhập
                var ctgh = listCTGH.Where(c => c.IDNguoiDung == iduser).ToList();
                if (ctgh.Count == 0)
                {
                    TempData["Message"] = "Không có sản phẩm nào trong giỏ hàng!";
                }
                return View(ctgh);
            }
        }
        // THÊM VÀO GIỎ
        public async Task<IActionResult> AddToCart(Guid id)
        {
            // Lấy Biến thể từ ID
            var product = listBT.Where(c => c.ID.Equals(id)).FirstOrDefault();

            if (HttpContext.Session.GetString("IdUser") == null)  // Nếu không đăng nhập, thêm sp thì lưu vào session giỏ hàng
            {
                //Đọc dữ liệu từ Session xem trong Cart nó có cái gì chưa?
                var cartproducts = GioHangService.GetObjFormSession(HttpContext.Session, "Cart");
                if (cartproducts.Count == 0)
                {
                    var cartitem = new ChiTietGioHang()
                    {
                        IDNguoiDung = new Guid(),
                        IDBienThe = product.ID,
                        SoLuong = 1,
                    };
                    cartproducts.Add(cartitem); // Nếu Cart rỗng thì thêm sp vào luôn
                    // Đưa dữ liệu về lại Session
                    GioHangService.SetObjToJson(HttpContext.Session, "Cart", cartproducts);
                }
                else
                {
                    if (!GioHangService.CheckProductInCart(id, cartproducts)) // Cart không rỗng nhưng k chứa sản phẩm đó
                    {
                        var cartitem = new ChiTietGioHang()
                        {
                            IDNguoiDung = new Guid(),
                            IDBienThe = product.ID,
                            SoLuong = 1,
                        };
                        cartproducts.Add(cartitem); // Nếu Cart chưa chứa sản phẩm thì thêm sp vào luôn
                                                    // Đưa dữ liệu về lại Session
                        GioHangService.SetObjToJson(HttpContext.Session, "Cart", cartproducts);
                    }
                    else // Nếu đã có sản phẩm trong giỏ rồi thì tăng số lượng lên 1
                    {
                        var cartitem = cartproducts.Where(c => c.IDBienThe == id).FirstOrDefault();
                        cartitem.SoLuong++;

                        GioHangService.SetObjToJson(HttpContext.Session, "Cart", cartproducts);
                        var x = GioHangService.GetObjFormSession(HttpContext.Session, "Cart");
                    }
                }
            } // Ngược lại có đăng nhập
            else
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                var cartDetailList = listCTGH.Where(c => c.IDNguoiDung == iduser).ToList();// Lấy cartitem trong CSDL của người dùng 

                if (cartDetailList.Count == 0)// Giỏ rỗng thì thêm mới
                {
                    var cartitem = new ChiTietGioHang()
                    {
                        IDNguoiDung = Guid.Parse(HttpContext.Session.GetString("IdUser")),
                        IDBienThe = product.ID,
                        SoLuong = 1,
                    };
                    _context.ChiTietGioHangs.Add(cartitem);
                    _context.SaveChanges();
                    //string data = JsonConvert.SerializeObject(cartitem);
                    //StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    //HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/ChiTietGioHang/Create-CTGioHang", content).Result;
                }
                else // Không rỗng thì ktra có chứa sản phẩm đấy không
                {
                    if (!GioHangService.CheckProductInCart(id, cartDetailList)) // 
                    {
                        var cartitem = new ChiTietGioHang()
                        {
                            IDNguoiDung = Guid.Parse(HttpContext.Session.GetString("IdUser")),
                            IDBienThe = product.ID,
                            SoLuong = 1,
                        };
                        _context.ChiTietGioHangs.Add(cartitem);
                        _context.SaveChanges();
                        //string data = JsonConvert.SerializeObject(cartitem);
                        //StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                        //HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/ChiTietGioHang/Create-CTGioHang", content).Result;
                    }
                    else // Nếu đã có sản phẩm trong giỏ rồi thì tăng số lượng lên 1
                    {
                        var cartitem = cartDetailList.Where(c => c.IDBienThe == id).FirstOrDefault();
                        cartitem.SoLuong++;
                        _context.ChiTietGioHangs.Update(cartitem);
                        _context.SaveChanges();
                        //string data = JsonConvert.SerializeObject(cartitem);
                        //StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                        //HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "/ChiTietGioHang/Update-CTGioHang", content).Result;
                    }
                }
            }
            return RedirectToAction("ShowCart");
        }
        // CẬP NHẬT GIỎ HÀNG
        public IActionResult Update_Quantity(IFormCollection f)
        {
            var countsp = listBT.Where(c=> c.ID == Guid.Parse(f["ID_product"].ToString())).FirstOrDefault(); // Sản phẩm bị sửa
            Guid id = Guid.Parse(f["ID_product"].ToString()); // Lấy id sp bị chỉnh sửa trong giỏ
            // đã đăng nhập
            if (HttpContext.Session.GetString("IdUser") != null) 
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                var cartDetailList = listCTGH.Where(c => c.IDNguoiDung == iduser).ToList();// Lấy cartitem trong CSDL của người dùng 

                if (int.Parse(f["Quantity"].ToString()) == 0) // bằng 0 thì xóa luôn
                {
                    var x = cartDetailList.Where(c => c.ID == id).FirstOrDefault();
                    HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/ChiTietGioHang/Delete-CTGioHang?id=" + id.ToString()).Result;
                    //_context.ChiTietGioHangs.Remove(x);
                    //_context.SaveChanges();

                }
                else if (int.Parse(f["Quantity"].ToString()) > countsp.SoLuong) // Lớn hơn thì thông báo
                {
                    TempData["OverQuantity"] = "Vượt quá số lượng sản phẩm trong kho!";
                    return RedirectToAction("ShowCart");
                }
                else
                { // cập nhật vào giỏ hàng
                    var x = cartDetailList.Where(c => c.IDBienThe == id).FirstOrDefault();
                    x.SoLuong = int.Parse(f["Quantity"].ToString());
                    _context.Update(x);
                    _context.SaveChanges();
                }
                return RedirectToAction("ShowCart");
            }
            else // chưa đăng nhập
            {
                var products = GioHangService.GetObjFormSession(HttpContext.Session, "Cart"); // Lấy cart trong session
                if (int.Parse(f["Quantity"].ToString()) == 0)
                {
                    var x = products.Where(c => c.IDBienThe == id).FirstOrDefault();
                    products.Remove(x);
                }
                else if (int.Parse(f["Quantity"].ToString()) > countsp.SoLuong)
                {
                    TempData["OverQuantity"] = "Vượt quá số lượng sản phẩm trong kho!";
                    return RedirectToAction("ShowCart");
                }
                else
                {
                    var x = products.Where(c => c.IDBienThe == id).FirstOrDefault();
                    x.SoLuong = int.Parse(f["Quantity"].ToString());
                }
                GioHangService.SetObjToJson(HttpContext.Session, "Cart", products);
                return RedirectToAction("ShowCart");
            }
        }
        // XÓA SẢN PHẨM TRONG GIỎ HÀNG
        public IActionResult RemoveAll()
        {
            if (HttpContext.Session.GetString("IdUser") != null)// đã đăng nhập
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                var cartDetailList = listCTGH.Where(c => c.IDNguoiDung == iduser).ToList();// Lấy cartitem trong CSDL của người dùng 
                for (int i = 0; i < cartDetailList.Count; i++)
                {
                    _context.ChiTietGioHangs.Remove(cartDetailList[i]);
                    _context.SaveChanges();
                }
                return RedirectToAction("ShowCart");
            }
            else
            {
                GioHangService.RemoveAll(HttpContext.Session, "Cart");
                return RedirectToAction("ShowCart");
            }
        }
        public IActionResult RemoveCartItem(Guid id)
        {
            if (HttpContext.Session.GetString("IdUser") != null)// đã đăng nhập
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                var cartDetailList = listCTGH.Where(c => c.IDNguoiDung == iduser).ToList();// Lấy cartitem trong CSDL của người dùng 
                ChiTietGioHang cartItem = cartDetailList.Where(c => c.IDBienThe == id).FirstOrDefault();
                _context.ChiTietGioHangs.Remove(cartItem);
                _context.SaveChanges();
                return RedirectToAction("ShowCart");
            }
            else
            {
                var products = GioHangService.GetObjFormSession(HttpContext.Session, "Cart");
                var x = products.Where(c => c.IDBienThe == id).FirstOrDefault();
                products.Remove(x);
                GioHangService.SetObjToJson(HttpContext.Session, "Cart", products);
                return RedirectToAction("ShowCart");
            }
        }
    }
}
