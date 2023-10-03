using System.ComponentModel.DataAnnotations;

namespace TH_DAPM_WebBanHangOnline.Models.ClassModel
{
    public class AdminUser
    {
        [Key]
        public int AdminUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Role { get; set; }
    }
}
