using CoreDatabase.Interfaces;
using CoreDatabase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDatabase.Models
{
    [Table("HoaDon")]
    public class HoaDon : IAuditable
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã hóa đơn")]
        public string MaHD { get; set; } = null!;

        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày lập")]
        public DateTime NgayLap { get; set; } = DateTime.Now;

        [StringLength(20)]
        [Display(Name = "Mã khách hàng")]
        public string? MaKH { get; set; }

        [Required(ErrorMessage = "Mã nhân viên lập hóa đơn là bắt buộc")]
        [StringLength(20)]
        [Display(Name = "Mã nhân viên")]
        public string MaNV { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Tổng tiền")]
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền phải lớn hơn hoặc bằng 0")]
        public decimal TongTien { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Giảm giá")]
        [Range(0, double.MaxValue, ErrorMessage = "Giảm giá phải lớn hơn hoặc bằng 0")]
        public decimal GiamGia { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Thành tiền")]
        [Range(0, double.MaxValue, ErrorMessage = "Thành tiền phải lớn hơn hoặc bằng 0")]
        public decimal ThanhTien { get; set; }

        [StringLength(50)]
        [Display(Name = "Phương thức thanh toán")]
        public string? PhuongThucTT { get; set; }

        [Display(Name = "Trạng thái")]
        public bool TT { get; set; } = true;

        // Relationships (Khóa ngoại)
        [ForeignKey("MaKH")]
        public virtual KhachHang? KhachHang { get; set; }

        [ForeignKey("MaNV")]
        public virtual NhanVien? NhanVien { get; set; }

        // Navigation Property: Một hóa đơn chứa nhiều chi tiết hóa đơn
        public virtual ICollection<CTHoaDon>? CTHoaDons { get; set; }

        // Audit
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
    }
}