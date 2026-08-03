using System;
using CoreDatabase.Interfaces;
using CoreDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDatabase.Models
{
    [Table("CTPhieuNhap")]
    [PrimaryKey(nameof(MaPN), nameof(MaSP))] // Định nghĩa khóa chính phức hợp (Composite Key)
    public class CTPhieuNhap : IAuditable
    {
        [StringLength(20)]
        [Display(Name = "Mã phiếu nhập")]
        public string MaPN { get; set; } = null!;

        [StringLength(20)]
        [Display(Name = "Mã sản phẩm")]
        public string MaSP { get; set; } = null!;

        [Display(Name = "Số lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Đơn giá nhập")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá nhập phải lớn hơn hoặc bằng 0")]
        public decimal DonGiaNhap { get; set; }

        // Relationships (Khóa ngoại)
        [ForeignKey("MaPN")]
        public virtual PhieuNhap? PhieuNhap { get; set; }

        [ForeignKey("MaSP")]
        public virtual SanPham? SanPham { get; set; }

        // Audit
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
    }
}