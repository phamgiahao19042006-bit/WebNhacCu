using System;
using CoreDatabase.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDatabase.Models
{
    [Table("KhuyenMai")]
    public class KhuyenMai : IAuditable, IMeta
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã khuyến mãi")]
        public string MaKM { get; set; } = null!;

        [Required(ErrorMessage = "Tên chương trình khuyến mãi không được để trống")]
        [StringLength(150)]
        [Display(Name = "Tên khuyến mãi")]
        public string TenKhuyenMai { get; set; } = null!;

        [StringLength(50)]
        [Display(Name = "Loại giảm")] // Ví dụ: Theo phần trăm (%) hoặc số tiền cố định (VNĐ)
        public string? LoaiGiam { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Giá trị giảm")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá trị giảm phải lớn hơn hoặc bằng 0")]
        public decimal GiaTriGiam { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày bắt đầu")]
        public DateTime NgayBatDau { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày kết thúc")]
        public DateTime NgayKetThuc { get; set; }

        [Display(Name = "Điều kiện áp dụng")]
        public string? DieuKienApDung { get; set; }

        [Display(Name = "Trạng thái")]
        public bool TT { get; set; } = true;

        // Một chương trình khuyến mãi áp dụng cho nhiều sản phẩm trong CTKhuyenMai
        public virtual ICollection<CTKhuyenMai>? CTKhuyenMais { get; set; }

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