using System.ComponentModel.DataAnnotations;

namespace TH_DAPM_WebBanHangOnline.Models.ClassModel
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
