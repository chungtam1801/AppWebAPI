using AppData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppAPI.IServices;
using AppAPI.Services;
using AppData.ViewModels;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        //private readonly IAllRepository<HoaDon> repos;
        //AssignmentDBContext context = new AssignmentDBContext();
        //DbSet<HoaDon> hoadon;
        private readonly IHoaDonService _iHoaDonService;
        public HoaDonController()
        {
            //hoadon = context.HoaDons;
            //AllRepository<HoaDon> all = new AllRepository<HoaDon>(context, hoadon);
            //repos = all;
            _iHoaDonService = new HoaDonService(); 
        }

        // GET: api/<HoaDOnController>
        [HttpGet]
        public List<HoaDon> Get()
        {
            return _iHoaDonService.GetAllHoaDon();
        }
        [HttpPost("create-hoaDon")]
        public bool CreateHoaDon(HoaDonViewModel hoaDonViewModel)
        {
            return _iHoaDonService.CreateHoaDon(hoaDonViewModel.ChiTietGioHangs, hoaDonViewModel.Ten,hoaDonViewModel.SDT,hoaDonViewModel.Email, hoaDonViewModel.PhuongThucThanhToan,hoaDonViewModel.DiaChi,
                hoaDonViewModel.TienShip);
        }
        //[HttpPost("create-hoaDon-koDangNhap")]
        //public bool CreateHoaDon(List<ChiTietGioHang> chiTietGioHangs, string ten, string SDT, string email, string phuongThucThanhToan, string diaChi, int tienShip)
        //{
        //    return _iHoaDonService.CreateHoaDon(chiTietGioHangs,ten,SDT,email, phuongThucThanhToan,diaChi,tienShip);
        //}
        // GET api/<HoaDonController>/5
        //[HttpGet("{id}")]
        //public HoaDon Get(Guid id)
        //{
        //    return repos.GetAll().FirstOrDefault(x => x.ID == id);
        //}
        // POST api/<HoaDonController>
        //[HttpPost("Create-HoaDon")]
        //public bool Post(HoaDon hoaDon)
        //{
        //    return repos.Add(hoaDon);
        //}
        //[HttpPut("Update-HoaDon")]
        //public bool UpdateHoaDon(Guid id, DateTime ngayTao, DateTime ngayThanhToan, string tenNguoiNhan, string sdt, string email, string diaChi, int tienShip, string phuongthucthanhtoan, int trangThaiGiaoHang, Guid idNguoiDung, Guid idVoucher)
        //{
        //    var hoaDon = repos.GetAll().FirstOrDefault(h => h.ID == id);
        //    if (hoadon != null)
        //    {
        //        hoaDon.NgayTao = ngayTao;
        //        hoaDon.NgayThanhToan = ngayThanhToan;
        //        hoaDon.SDT = sdt; ;
        //        hoaDon.Email = email;
        //        hoaDon.DiaChi = diaChi;
        //        hoaDon.TienShip = tienShip;
        //        hoaDon.PhuongThucThanhToan = phuongthucthanhtoan;
        //        hoaDon.TrangThaiGiaoHang = trangThaiGiaoHang;
        //        hoaDon.IDNguoiDung = idNguoiDung;
        //        hoaDon.IDVoucher = idVoucher;
        //        return repos.Update
        //            (hoaDon);
        //    }
        //    else { return false; }
        //}

        //[HttpDelete("Delete-HoaDon")]

        //public bool DeleteHoaDon(Guid id)
        //{
        //    var hoaDon = repos.GetAll().FirstOrDefault(p => p.ID == id);
        //    if (hoaDon != null)
        //    {
        //        return repos.Delete(hoaDon);
        //    }
        //    else { return false; }
        //}

    }
}



















