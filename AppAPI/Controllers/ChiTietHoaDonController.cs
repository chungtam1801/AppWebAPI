using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IAllRepository<HoaDon> repos;
        AssignmentDBContext context = new AssignmentDBContext();
        DbSet<HoaDon> hoadon;
        public HoaDonController()
        {
            hoadon = context.HoaDons;
            AllRepository<HoaDon> all = new AllRepository<HoaDon>(context, hoadon);
            repos = all;
        }

        // GET: api/<HoaDOnController>
        [HttpGet]
        public IEnumerable<HoaDon> Get()
        {
            return repos.GetAll();
        }
        // GET api/<HoaDonController>/5
        [HttpGet("{id}")]
        public HoaDon Get(Guid id)
        {
            return repos.GetAll().FirstOrDefault(x => x.ID == id);
        }

        // POST api/<HoaDonController>
        [HttpPost("Create-HoaDon")]

        public bool Post(DateTime ngayTao, DateTime? ngayThanhToan, string tenNguoiNhan, string sdt, string email, string diaChi, int tienShip, string phuongthucthanhtoan, int trangThaiGiaoHang, Guid? idNguoiDung, Guid? idVoucher)
        {
            HoaDon hoaDon = new HoaDon();
            hoaDon.ID = Guid.NewGuid();
            hoaDon.NgayTao = ngayTao;
            hoaDon.NgayThanhToan = ngayThanhToan;
            hoaDon.TenNguoiNhan = tenNguoiNhan;
            hoaDon.SDT = sdt; 
            hoaDon.Email = email;
            hoaDon.DiaChi = diaChi;
            hoaDon.TienShip = tienShip;
            hoaDon.PhuongThucThanhToan = phuongthucthanhtoan;
            hoaDon.TrangThaiGiaoHang = trangThaiGiaoHang;
            hoaDon.IDNguoiDung = idNguoiDung;
            hoaDon.IDVoucher = idVoucher;
            return repos.Add(hoaDon);
        }
        [HttpPut("Update-HoaDon")]
        public bool UpdateHoaDon(Guid id, DateTime ngayTao, DateTime ngayThanhToan, string tenNguoiNhan, string sdt, string email, string diaChi, int tienShip, string phuongthucthanhtoan, int trangThaiGiaoHang, Guid idNguoiDung, Guid idVoucher)
        {
            var hoaDon = repos.GetAll().FirstOrDefault(h => h.ID == id);
            if (hoadon != null)
            {
                hoaDon.NgayTao = ngayTao;
                hoaDon.NgayThanhToan = ngayThanhToan;
                hoaDon.SDT = sdt; ;
                hoaDon.Email = email;
                hoaDon.DiaChi = diaChi;
                hoaDon.TienShip = tienShip;
                hoaDon.PhuongThucThanhToan = phuongthucthanhtoan;
                hoaDon.TrangThaiGiaoHang = trangThaiGiaoHang;
                hoaDon.IDNguoiDung = idNguoiDung;
                hoaDon.IDVoucher = idVoucher;
                return repos.Update
                    (hoaDon);
            }
            else { return false; }
        }

        [HttpDelete("Delete-HoaDon")]

        public bool DeleteHoaDon(Guid id)
        {
            var hoaDon = repos.GetAll().FirstOrDefault(p => p.ID == id);
            if (hoaDon != null)
            {
                return repos.Delete(hoaDon);
            }
            else { return false; }
        }

    }
}
