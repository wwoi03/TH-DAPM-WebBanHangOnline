using Microsoft.EntityFrameworkCore;
using TH_DAPM_WebBanHangOnline.Models.ClassModel;

namespace TH_DAPM_WebBanHangOnline.Models
{
    public class DBHelper
    {
        DatabaseContext dbContext;

        public DBHelper(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /* -------------------------- Khách hàng -------------------------- */
        // Tìm khách hàng theo Email
        public Customer GetCustomerByEmail(string email)
        {
            return dbContext.Customers.Where(item => item.Email == email).FirstOrDefault();
        }

        // Tạo khách hàng
        public void CreateCustomer(Customer customer)
        {
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
        }
        
        /* -------------------------- Giỏ hàng -------------------------- */
        // lấy danh sách giỏ hàng theo mã khách hàng
        public List<Cart> GetMyCartById(int? customerId)
        {
            List<Cart> carts = dbContext.Carts.Where(c => c.CustomerId == customerId).ToList();
            return carts;
        }

        /* -------------------------- Chi tiết sản phẩm -------------------------- */
        // lấy danh sách giỏ hàng theo mã khách hàng
        public Product GetProductDetails (int? productId)
        {
            return dbContext.Products.Include(p => p.Producer).Include(p => p.Category).FirstOrDefault(p => p.ProductId == productId);
        }
    }
}
