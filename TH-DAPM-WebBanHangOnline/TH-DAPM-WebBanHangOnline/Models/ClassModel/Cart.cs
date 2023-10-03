using System.ComponentModel.DataAnnotations;

namespace TH_DAPM_WebBanHangOnline.Models.ClassModel
{
    public class Cart
    {
        [Key]
        public int CustomerId { get; set; }

        [Key]
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public double Total { get; set; }
        public DateTime UpdateDay { get; set; }
    }
}
