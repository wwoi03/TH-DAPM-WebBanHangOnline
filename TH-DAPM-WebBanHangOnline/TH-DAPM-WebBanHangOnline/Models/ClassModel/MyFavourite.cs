using System.ComponentModel.DataAnnotations;

namespace TH_DAPM_WebBanHangOnline.Models.ClassModel
{
    public class MyFavourite
    {
        [Key]
        public int CustomerId { get; set; }

        [Key]
        public int  ProductId  { get; set; }
    }
}
