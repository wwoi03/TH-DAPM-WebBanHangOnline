using System.ComponentModel.DataAnnotations;

namespace TH_DAPM_WebBanHangOnline.Models.ViewModel
{
    public class AdminUserViewModel
    {
        public int AdminUserId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên của bạn.")]
        [StringLength(30, ErrorMessage = "Chiều dài vượt quá 30 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email của bạn.")]
        [StringLength(30, ErrorMessage = "Chiều dài vượt quá 30 ký tự")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [StringLength(30, ErrorMessage = "Chiều dài vượt quá 30 ký tự.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{6,}$", ErrorMessage = "Mật khẩu phải chứa ít nhất một chữ cái viết hoa, một chữ cái viết thường và một số.")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng thêm quyền người dùng.")]
        [StringLength(30, ErrorMessage = "Chiều dài vượt quá 30 ký tự.")]
        [Display(Name = "Quyền")]
        public int? Role { get; set; }
    }
}
