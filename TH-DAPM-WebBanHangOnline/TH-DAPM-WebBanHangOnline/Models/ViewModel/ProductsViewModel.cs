namespace TH_DAPM_WebBanHangOnline.Models.ViewModel
{
    public class ProductsViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        public string CategoryName { get; set; }

        public string ProducerName { get; set; }
    }
}
