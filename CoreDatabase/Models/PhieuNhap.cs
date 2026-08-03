using System;
using CoreDatabase.Interfaces;
using CoreDatabase.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDatabase.Models
{
    [Table("PhieuNhap")]
    public class PhieuNhap : IAuditable
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã phiếu nhập")]
        public string MaPN { get; set; } = null!;

        [Required(ErrorMessage = "Mã nhà cung cấp là bắt buộc")]
        [StringLength(20)]
        [Display(Name = "Mã nhà cung cấp")]
        public string MaNCC { get; set; } = null!;

        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày nhập")]
        public DateTime NgayNhap { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Tổng tiền")]
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền phải lớn hơn hoặc bằng 0")]
        public decimal TongTien { get; set; }

        [Display(Name = "Trạng thái")]
        public bool TT { get; set; } = true;

        // Relationship (Khóa ngoại)
        [ForeignKey("MaNCC")]
        public virtual NhaCC? NhaCC { get; set; }

        //  Một phiếu nhập có nhiều chi tiết phiếu nhập
        public virtual ICollection<CTPhieuNhap>? CTPhieuNhaps { get; set; }

        // Audit
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
    }
}