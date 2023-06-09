using AppData.Models;
using AppView.IServices;
using Newtonsoft.Json;

namespace AppView.Services
{
    public class HoaDonService:IHoaDonService
    {
        public List<ChiTietHoaDon> GetAllChiTietHoaDon(Guid idHoaDon)
        {
            throw new NotImplementedException();
        }

        public async Task<List<HoaDon>?> GetAllHoaDonAsync()
        {
            HttpClient httpClient = new HttpClient();
            string apiUrl = "https://localhost:7021/api/HoaDon";
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var sanPhams = JsonConvert.DeserializeObject<List<HoaDon>>(apiData);
            return sanPhams;
        }

        public void ThanhToan(Guid idGioHang, string diaChi)
        {
            throw new NotImplementedException();
        }

        public void ThanhToan(List<ChiTietGioHang> chiTietGioHangs, string ten, string SDT, string email, string phuongThucThanhToan, string diaChi)
        {
            throw new NotImplementedException();
        }
        public void UpdateTrangThaiGiaoHang(int trangThai)
        {
            throw new NotImplementedException();
        }
    }
}
