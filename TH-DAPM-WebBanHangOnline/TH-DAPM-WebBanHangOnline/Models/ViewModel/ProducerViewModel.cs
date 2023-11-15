using System.ComponentModel.DataAnnotations;

namespace TH_DAPM_WebBanHangOnline.Models.ViewModel
{
	public class ProducerViewModel
	{
		public int ProducerId { get; set; }

		[Required(ErrorMessage = "Tên không được để trống")]
		[StringLength(255, ErrorMessage = "Tên không được vượt quá 255 ký tự")]
		public string Name { get; set; }


		public string Phone { get; set; }

		[Required(ErrorMessage = "Địa chỉ không được để trống")]
		public string Address { get; set; }
	}

}