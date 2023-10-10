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
        public List<Cart> GetMyCartByCustomerId(int? customerId)
        {
            List<Cart> carts = dbContext.Carts.Include(c => c.Product).Include(c => c.Customer).Where(c => c.CustomerId == customerId).ToList();
            return carts;
        }

        public Cart GetCartById(int cartId)
        {
            return dbContext.Carts.Include(c => c.Product).Include(c => c.Customer).FirstOrDefault(c => c.CartId == cartId);
        }

        // xóa sản phẩm trong giỏ hàng
        public void DeleteProductInCart(int cardId)
        {
            dbContext.Carts.Remove(GetCartById(cardId));
            dbContext.SaveChanges();
        }

        /* -------------------------- Chi tiết sản phẩm -------------------------- */
        // lấy danh sách giỏ hàng theo mã khách hàng
        public Product GetProductDetails (int? productId)
        {
            return dbContext.Products.Include(p => p.Producer).Include(p => p.Category).FirstOrDefault(p => p.ProductId == productId);
        }

        /* -------------------------- SẢN PHẨM TRANG BÁN -------------------------- */
        //lấy danh sách sản phẩm
        public List<Product> getProducts()
        {
            List<Product> products = dbContext.Products.Include(p => p.Producer).Include(p => p.Category).OrderByDescending(p => p.ProductId).ToList();
            return products;
        }


        //lấy sản phẩm thông qua id
        public Product getProductById(int id)
        {
            Product product = new Product();
            return product;
        }


        //lấy sản phẩm thông qua tên
        public Product GetProductsByName(string name)
        {
            Product product = new Product();
            return product;
        }


        //lấy sản phẩm thông qua loại
        public List<Product> GetProductsByType(int id)
        {
            List<Product> products = dbContext.Products.Include(p => p.Producer).Include(p => p.Category).Where(P => P.CategoryId == id).ToList();
            return products;
        }

        /* -------------------------- Danh mục sản phẩm -------------------------- */
        // Lấy danh mục sản phẩm
        public List<Category> GetCategories()
        {
            List<Category> categories = dbContext.Categories.ToList();
            return categories;
        }


    }
}
