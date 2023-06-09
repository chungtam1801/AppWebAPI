using AppData.Models;

namespace AppView.IServices
{
    public interface IHoaDonService
    {
        public void ThanhToan(Guid idGioHang,string diaChi);
        public void ThanhToan(List<ChiTietGioHang> chiTietGioHangs, string ten, string SDT, string email, string phuongThucThanhToan,string diaChi);
        public Task<List<HoaDon>?> GetAllHoaDonAsync();
        public List<ChiTietHoaDon> GetAllChiTietHoaDon(Guid idHoaDon);
        public void UpdateTrangThaiGiaoHang(int trangThai);
    }
}
