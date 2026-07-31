using CoreDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDatabase.Models
{
    [Table("CTKhuyenMai")]
    [PrimaryKey(nameof(MaKM), nameof(MaSP))] // Định nghĩa khóa chính phức hợp (Composite Key)
    public class CTKhuyenMai
    {
        [StringLength(20)]
        [Display(Name = "Mã khuyến mãi")]
        public string MaKM { get; set; } = null!;

        [StringLength(20)]
        [Display(Name = "Mã sản phẩm")]
        public string MaSP { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Hệ số")]
        public decimal? HeSo { get; set; }

        [Display(Name = "Số lượng")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 0")]
        public int? SoLuong { get; set; }

        // Relationships (Khóa ngoại)
        [ForeignKey("MaKM")]
        public virtual KhuyenMai? KhuyenMai { get; set; }

        [ForeignKey("MaSP")]
        public virtual SanPham? SanPham { get; set; }
    }
}