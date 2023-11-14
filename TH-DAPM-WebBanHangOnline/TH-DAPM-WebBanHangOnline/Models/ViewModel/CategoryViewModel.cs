using System.ComponentModel.DataAnnotations;
using TH_DAPM_WebBanHangOnline.Models.ClassModel;

namespace TH_DAPM_WebBanHangOnline.Models.ViewModel
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên loại sản phẩm.")]
        [StringLength(50, ErrorMessage = "Chiều dài vượt quá 50 ký tự")]
        [Display(Name = "Tên loại sản phẩm")]
        public string Name { get; set; }

        public Category ConvertClassModel()
        {
            Category category = new Category()
            {
                Name = Name
            };

            return category;
        }
    }
}
