﻿using Microsoft.EntityFrameworkCore;
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

        // tính số lưởng sản phẩm trong giỏ hàng
        public int GetCountMyCart(int? customerId)
        {
            int countCart = dbContext.Carts.Include(c => c.Product).Include(c => c.Customer).Where(c => c.CustomerId == customerId).ToList().Count;
            return countCart;
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

        //chỉnh sửa số lượng sản phẩm trong giỏ hàng
        public void EditQuantityPro(int cartId,int quanti , out double total)
        {
            total = 0;
           
			var cart = dbContext.Carts.Include(p=>p.Product).FirstOrDefault(c => c.CartId == cartId);
			if (cart != null)
			{
				cart.Quantity = quanti;
                total =  cart.Product.Price * quanti;
				dbContext.SaveChanges();
			}
		}

        // lấy danh sách order
        public List<Order> GetOrderByCustomerId(int customerId)
        {
            return dbContext.Orders.Include(item => item.Customer).Where(item => item.CustomerId == customerId).OrderByDescending(item => item.OrderId).ToList();
        }

        // lấy danh sách chi tiết đơn hàng theo orderid
        public List<OrderDetails> GetListOrderDetailsByOrderId(int orderId)
        {
            return dbContext.OrderDetails.Include(item => item.Product).Include(item => item.Order).Where(item => item.OrderId == orderId).ToList();
        }

        // đặt hàng
        public void CreateOrder(Order order, List<Cart> listCart) 
        {
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();

            foreach (var item in listCart)
            {
                OrderDetails orderDetails = new OrderDetails()
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Price = item.Product.Price,
                    Quantity = item.Quantity,
                    Total = item.Product.Price * item.Quantity
                };

                CreateOrderDetails(orderDetails);
            }

            DeleteAllCart(listCart);
        }

        // tạo chi tiết đơn hàng
        public void CreateOrderDetails(OrderDetails orderDetails)
        {
            dbContext.OrderDetails.Add(orderDetails);
            dbContext.SaveChanges();
        }

        // xóa sản phẩm trong giỏ hàng
        public void DeleteAllCart(List<Cart> carts)
        {
            dbContext.Carts.RemoveRange(carts);
            dbContext.SaveChanges();
        }

		/* -------------------------- Chi tiết sản phẩm -------------------------- */
		// lấy danh sách giỏ hàng theo mã khách hàng
		public Product GetProductDetails (int? productId)
        {
            return dbContext.Products.Include(p => p.Producer).Include(p => p.Category).FirstOrDefault(p => p.ProductId == productId);
        }

        //------------------------------------------------------------THANH LỌC TỤI NÓ-----------------------------
        //tìm kiếm sản phẩm
        public List<Product> GetListProductByName(string name)
        {
            var query = dbContext.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name) || p.Category.Name.Contains(name));
            }

            return query.OrderByDescending(p => p.ProductId).ToList();
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

        // lấy danh sách comment trên sản phẩm
        public List<Comment> GetCommentsByProductId(int? id)
        {
            List<Comment> comments = dbContext.Comments.Include(p => p.Product).Include(p => p.Customer).Where(P => P.ProductId == id).OrderByDescending(p => p.CommentDate).ToList();
            return comments;
        }

        // thêm bình luộn
        public void AddComment(Comment comment)
        {
            dbContext.Comments.Add(comment);
            dbContext.SaveChanges();
        }

        /* -------------------------- Danh mục sản phẩm -------------------------- */
        // Lấy danh mục sản phẩm
        public List<Category> GetCategories()
        {
            List<Category> categories = dbContext.Categories.ToList();
            return categories;
        }

        // thêm loại sản phẩm
        public void CreateCategory(Category category)
        {
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
        }

        public void AddItemToCart(Cart cart)
        {
            dbContext.Carts.Add(cart);
            dbContext.SaveChanges();
        }

        /* -------------------------- Admin -------------------------- */
        public AdminUser LoginAdmin(string email, string password)
        {
            return dbContext.AdminUsers.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
        }

        // lấy danh sách khách hàng
        public List<Customer> GetListCustomer()
        {
            return dbContext.Customers.ToList();
        }

        // lấy danh sách đơn hàng
        public List<Order> GetListOrder()
        {
            return dbContext.Orders.Include(item => item.Customer).OrderByDescending(item => item.OrderId).ToList();
        }

        // lấy danh sách đơn hàng
        public List<Order> GetListOrderByCurrentDay()
        {
            DateTime currentDate = DateTime.Now;
            int currentMonth = currentDate.Month;
            int currentYear = currentDate.Year;

            return dbContext.Orders
                .Include(item => item.Customer)
                .Where(item => item.OrderDay.Month == currentMonth && item.OrderDay.Year == currentYear)
                .OrderByDescending(item => item.OrderId)
                .ToList();
        }

        // lấy đơn hàng
        public Order GetOrderById(int orderId)
        {
            return dbContext.Orders.Include(item => item.Customer).FirstOrDefault(item => item.OrderId == orderId);
        }

        // cập nhật đơn hàn
        public void UpdateStatusOrder(int orderId, string status)
        {
            Order order = GetOrderById(orderId);
            order.Status = status;

            dbContext.Update(order);
            dbContext.SaveChanges();
        }
    }
}
