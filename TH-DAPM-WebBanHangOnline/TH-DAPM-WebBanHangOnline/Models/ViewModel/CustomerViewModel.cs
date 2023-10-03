using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace TH_DAPM_WebBanHangOnline.Models.ViewModel
{
    public class CustomerViewModel
    {
        [Display(Name = "Họ và tên")]
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

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu xác nhận.")]
        [StringLength(30, ErrorMessage = "Chiều dài vượt quá 30 ký tự.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{6,}$", ErrorMessage = "Mật khẩu phải chứa ít nhất một chữ cái viết hoa, một chữ cái viết thường và một số.")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string VerifyPassword { get; set; }
    }
}
