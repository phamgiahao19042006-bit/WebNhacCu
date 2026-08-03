using System;
using CoreDatabase.Interfaces;
using CoreDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDatabase.Models
{
    [Table("CTHoaDon")]
    [PrimaryKey(nameof(MaHD), nameof(MaSP))] // Định nghĩa khóa chính phức hợp (Composite Key)
    public class CTHoaDon : IAuditable
    {
        [StringLength(20)]
        [Display(Name = "Mã hóa đơn")]
        public string MaHD { get; set; } = null!;

        [StringLength(20)]
        [Display(Name = "Mã sản phẩm")]
        public string MaSP { get; set; } = null!;

        [Display(Name = "Số lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Đơn giá")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn hoặc bằng 0")]
        public decimal DonGia { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Thành tiền")]
        [Range(0, double.MaxValue, ErrorMessage = "Thành tiền phải lớn hơn hoặc bằng 0")]
        public decimal ThanhTien { get; set; }

        //  (Khóa ngoại)
        [ForeignKey("MaHD")]
        public virtual HoaDon? HoaDon { get; set; }

        [ForeignKey("MaSP")]
        public virtual SanPham? SanPham { get; set; }

        // Audit
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
    }
}