using AppData.Models;

namespace AppAPI.IServices
{
    public interface IHoaDonService
    {
        public bool CreateHoaDon(List<ChiTietGioHang> chiTietGioHangs, string ten, string SDT, string email, string phuongThucThanhToan, string diaChi, int tienShip);
        public List<HoaDon> GetAllHoaDon();
        public List<ChiTietHoaDon> GetAllChiTietHoaDon(Guid idHoaDon);
        public void UpdateTrangThaiGiaoHang(int trangThai);
    }
}
