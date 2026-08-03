using System;
using CoreDatabase.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDatabase.Models
{
    [Table("TaiKhoan")]
    public class TaiKhoan : IAuditable
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã tài khoản")]
        public string MaTK { get; set; } = null!;

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(50)]
        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        [StringLength(255)]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; } = null!;

        [Required(ErrorMessage = "Mã nhân viên là bắt buộc")]
        [StringLength(20)]
        [Display(Name = "Mã nhân viên")]
        public string MaNV { get; set; } = null!;

        [Required(ErrorMessage = "Mã vai trò là bắt buộc")]
        [StringLength(20)]
        [Display(Name = "Mã vai trò")]
        public string MaVaiTro { get; set; } = null!;

        [Display(Name = "Trạng thái")]
        public bool TT { get; set; } = true;

        //  (Khóa ngoại)
        [ForeignKey("MaNV")]
        public virtual NhanVien? NhanVien { get; set; }

        [ForeignKey("MaVaiTro")]
        public virtual VaiTro? VaiTro { get; set; }

        // Audit
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
