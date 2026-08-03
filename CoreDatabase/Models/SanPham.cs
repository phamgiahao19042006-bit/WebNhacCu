using System;
using CoreDatabase.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreDatabase.Models
{
    [Table("SanPham")]
    public class SanPham : IAuditable, IMeta
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã sản phẩm")]
        public string MaSP { get; set; } = null!;

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150)]
        [Display(Name = "Tên sản phẩm")]
        public string TenSP { get; set; } = null!;

        [Required(ErrorMessage = "Mã loại là bắt buộc")]
        [StringLength(20)]
        [Display(Name = "Mã loại")]
        public string MaLoai { get; set; } = null!;

        [Required(ErrorMessage = "Mã thương hiệu là bắt buộc")]
        [StringLength(20)]
        [Display(Name = "Mã thương hiệu")]
        public string MaTH { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Giá nhập")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá nhập phải lớn hơn hoặc bằng 0")]
        public decimal GiaNhap { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Đơn giá")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn hoặc bằng 0")]
        public decimal DonGia { get; set; }

        [Display(Name = "Số lượng tồn")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn phải lớn hơn hoặc bằng 0")]
        public int SoLuongTon { get; set; }

        [StringLength(255)]
        [Display(Name = "Hình ảnh")]
        public string? HinhAnh { get; set; }

        [Display(Name = "Mô tả")]
        public string? MoTa { get; set; }

        [Display(Name = "Trạng thái")]
        public bool TT { get; set; } = true;

        //  (Khoá ngoại)
        [ForeignKey("MaLoai")]
        public virtual LoaiSP? LoaiSP { get; set; }

        [ForeignKey("MaTH")]
        public virtual ThuongHieu? ThuongHieu { get; set; }

        // Meta
        public string MetaTitle { get; set; } = string.Empty;
        public string MetaKeyword { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;

        // Audit
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
