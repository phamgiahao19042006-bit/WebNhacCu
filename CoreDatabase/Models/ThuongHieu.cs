using System;
using CoreDatabase.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreDatabase.Models
{
    [Table("ThuongHieu")]
    public class ThuongHieu : IAuditable, IMeta
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã thương hiệu")]
        public string MaTH { get; set; } = null!;

        [Required(ErrorMessage = "Tên thương hiệu không được để trống")]
        [StringLength(100)]
        [Display(Name = "Tên thương hiệu")]
        public string TenTH { get; set; } = null!;

        [StringLength(100)]
        [Display(Name = "Quốc gia")]
        public string? QuocGia { get; set; }

        [Display(Name = "Trạng thái")]
        public bool TT { get; set; } = true;

        // Một Thương hiệu có nhiều Sản phẩm
        public virtual ICollection<SanPham>? SanPhams { get; set; }

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
