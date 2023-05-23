namespace AppData.Models
{
    public class SanPham
    {
        public Guid ID { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public int TrangThai { get; set; }
        public Guid IDLoaiSP { get; set; }
        public virtual IEnumerable<BienThe> BienThes { get; set; }
        public virtual LoaiSP LoaiSP { get; set; }
    }
}
