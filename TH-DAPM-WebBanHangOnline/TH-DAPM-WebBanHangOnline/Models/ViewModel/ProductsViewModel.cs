namespace TH_DAPM_WebBanHangOnline.Models.ViewModel
{
    public class ProductsViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public IFormFile ImageFile { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int ProducerId { get; set; }
        public string ProducerName { get; set; }
    }
}