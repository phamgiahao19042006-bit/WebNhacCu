using System;
using CoreDatabase.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDatabase.Models
{
    [Table("VaiTro")]
    public class VaiTro : IAuditable
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

        // Audit
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
