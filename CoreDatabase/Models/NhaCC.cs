using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDatabase.Models
{
    [Table("NhaCC")]
    public class NhaCC
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã nhà cung cấp")]
        public string MaNCC { get; set; } = null!;

        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
        [StringLength(150)]
        [Display(Name = "Tên nhà cung cấp")]
        public string TenNCC { get; set; } = null!;

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

        [Display(Name = "Trạng thái")]
        public bool TT { get; set; } = true;

        //  Một Nhà cung cấp có thể cung cấp nhiều Phiếu nhập
        public virtual ICollection<PhieuNhap>? PhieuNhaps { get; set; }
    }
}