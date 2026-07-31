using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreDatabase.Models
{
    [Table("LoaiSP")]
    public class LoaiSP
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã loại")]
        public string MaLoai { get; set; } = null!;

        [Required(ErrorMessage = "Tên loại không được để trống")]
        [StringLength(100)]
        [Display(Name = "Tên loại")]
        public string TenLoai { get; set; } = null!;

        [StringLength(500)]
        [Display(Name = "Mô tả")]
        public string? MoTa { get; set; }

        [Display(Name = "Trạng thái")]
        public bool TT { get; set; } = true;

        // Một Loại SP có nhiều Sản phẩm
        public virtual ICollection<SanPham>? SanPhams { get; set; }
    }
}
