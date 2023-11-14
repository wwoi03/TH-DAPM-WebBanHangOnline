using System.ComponentModel.DataAnnotations;
using TH_DAPM_WebBanHangOnline.Models.ClassModel;

namespace TH_DAPM_WebBanHangOnline.Models.ViewModel
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên người nhận.")]
        [StringLength(30, ErrorMessage = "Chiều dài vượt quá 30 ký tự.")]
        public string RecipientName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [StringLength(30, ErrorMessage = "Chiều dài vượt quá 30 ký tự.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng.")]
        [StringLength(30, ErrorMessage = "Chiều dài vượt quá 30 ký tự.")]
        public string AddressDelivery { get; set; }
        public double FeeShipping { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public string OrderDay { get; set; }
        public string Status { get; set; }
    }
}
