using Microsoft.EntityFrameworkCore;
using TH_DAPM_WebBanHangOnline.Models.ClassModel;

namespace TH_DAPM_WebBanHangOnline.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyFavourite>()
                .HasKey(o => new { o.CustomerId, o.ProductId });
            modelBuilder.Entity<OrderDetails>()
                .HasKey(o => new { o.OrderId, o.ProductId });
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<OrderDetails> OrderDetails  { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MyFavourite> MyFavourites { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
