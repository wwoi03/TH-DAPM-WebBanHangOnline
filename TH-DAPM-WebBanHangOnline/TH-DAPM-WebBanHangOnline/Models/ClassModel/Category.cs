using System.ComponentModel.DataAnnotations;

namespace TH_DAPM_WebBanHangOnline.Models.ClassModel
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
