using System;
using CoreDatabase.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDatabase.Models
{
    [Table("KhachHang")]
    public class KhachHang : IAuditable
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã khách hàng")]
        public string MaKH { get; set; } = null!;

        [Required(ErrorMessage = "Họ tên khách hàng không được để trống")]
        [StringLength(100)]
        [Display(Name = "Họ và tên")]
        public string HoTen { get; set; } = null!;

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(15)]
        [Display(Name = "Số điện thoại")]
        public string? SDT { get; set; }

        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [StringLength(255)]
        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }

        [Display(Name = "Điểm tích lũy")]
        [Range(0, int.MaxValue, ErrorMessage = "Điểm tích lũy phải lớn hơn hoặc bằng 0")]
        public int DiemTichLuy { get; set; } = 0;

        [Display(Name = "Trạng thái")]
        public bool TT { get; set; } = true;

        // Audit
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
    }
}