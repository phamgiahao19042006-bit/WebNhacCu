using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDatabase.Models
{
    [Table("VaiTro")]
    public class VaiTro
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã vai trò")]
        public string MaVaiTro { get; set; } = null!;

        [Required(ErrorMessage = "Tên vai trò không được để trống")]
        [StringLength(50)]
        [Display(Name = "Tên vai trò")]
        public string TenVaiTro { get; set; } = null!;

        //  Một vai trò có thể gán cho nhiều Tài khoản
        public virtual ICollection<TaiKhoan>? TaiKhoans { get; set; }
    }
}
