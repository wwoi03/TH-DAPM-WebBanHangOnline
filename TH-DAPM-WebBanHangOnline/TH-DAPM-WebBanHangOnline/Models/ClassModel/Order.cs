using System.ComponentModel.DataAnnotations;

namespace TH_DAPM_WebBanHangOnline.Models.ClassModel
{
    public class Order
    {
        [Key]
        public int OrderId  { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string RecipientName { get; set; }
        public string Phone { get; set; }
        public string AddressDeliverry { get; set; }
        public double FeeShipping { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public DateTime OrderDay { get; set; }
        public string Status { get; set; }
    }
}
