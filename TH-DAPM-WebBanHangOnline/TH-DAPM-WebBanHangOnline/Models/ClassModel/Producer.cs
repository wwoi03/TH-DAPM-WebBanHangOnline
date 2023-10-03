using System.ComponentModel.DataAnnotations;

namespace TH_DAPM_WebBanHangOnline.Models.ClassModel
{
    public class Producer
    {
        [Key]
        public int ProducerId { get; set; }
	    public string Name   { get; set; }
	    public string Phone { get; set; }
	    public string Address { get; set; }
    }
}
