namespace TH_DAPM_WebBanHangOnline.Models.ClassModel
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string CommentContent { get; set; }
        public double StarRating { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
