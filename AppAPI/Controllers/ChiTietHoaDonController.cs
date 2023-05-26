using AppData.IRepositories;
using AppData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppData.Repositories;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietHoaDonController : ControllerBase
    {
        private readonly IAllRepository<ChiTietHoaDon> repos;
        AssignmentDBContext context = new AssignmentDBContext();
        DbSet<ChiTietHoaDon> chiTietHoaDons;

        public ChiTietHoaDonController()
        {
            chiTietHoaDons = context.ChiTietHoaDons;
            AllRepository<ChiTietHoaDon> all = new AllRepository<ChiTietHoaDon>(context, chiTietHoaDons);
            repos = all;
        }
        // GET: api/<ChiTietHoaDOnController>
        [HttpGet]
        public IEnumerable<ChiTietHoaDon> Get()
        {
            return repos.GetAll();
        }
        [HttpGet("{id}")]
        public ChiTietHoaDon Get(Guid id)
        {
            return repos.GetAll().FirstOrDefault(x => x.ID == id);
        }

        [HttpPost("Create-ctHoaDon")]

        public bool Post(int donGia, int soLuong, int trangThai, Guid idBienThe, Guid idHoaDon)
        {
            ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
            chiTietHoaDon.ID = Guid.NewGuid();
            chiTietHoaDon.DonGia = donGia;
            chiTietHoaDon.SoLuong = soLuong;
            chiTietHoaDon.TrangThai = trangThai;
            chiTietHoaDon.IDBienThe = idBienThe;
            chiTietHoaDon.IDHoaDon = idHoaDon;
            return repos.Add(chiTietHoaDon);
        }

        // PUT api/<ChiTietHoaDonController>/5
        [HttpPut("Update-ChiTietHoaDon")]

        public bool UpdatectHoaDon(Guid id, int donGia, int soLuong, int trangThai, Guid idBienThe, Guid idHoaDon)
        {
            var chiTietHoaDon = repos.GetAll().FirstOrDefault(p => p.ID == id);
            if (chiTietHoaDon != null)
            {
                chiTietHoaDon.DonGia = donGia;
                chiTietHoaDon.SoLuong = soLuong;
                chiTietHoaDon.TrangThai = trangThai;
                chiTietHoaDon.IDBienThe = idBienThe;
                chiTietHoaDon.IDHoaDon = idHoaDon;
                return repos.Add(chiTietHoaDon);
            }
            else { return false; }
        }
        // DELETE api/<ChiTietHoaDonController>/5
        [HttpDelete("Delete-ChiTietHoaDon")]
        public bool DeleteChiTietHoaDon(Guid id)
        {
            var chiTietHoaDon = repos.GetAll().FirstOrDefault(p => p.ID == id);
            if (chiTietHoaDon != null)
            {
                return repos.Delete(chiTietHoaDon);
            }
            else { return false; }
        }
    }
}


















