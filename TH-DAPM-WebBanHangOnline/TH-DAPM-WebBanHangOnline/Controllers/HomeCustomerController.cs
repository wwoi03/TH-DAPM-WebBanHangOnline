using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TH_DAPM_WebBanHangOnline.Models;
using TH_DAPM_WebBanHangOnline.Models.ClassModel;
using TH_DAPM_WebBanHangOnline.Models.ViewModel;

namespace TH_DAPM_WebBanHangOnline.Controllers
{
    public class HomeCustomerController : Controller
    {
        DBHelper dbHelper;

        public HomeCustomerController(DatabaseContext context)
        {
            dbHelper = new DBHelper(context);
        }
        //trang chủ sản phẩm khách thấy
        public IActionResult HomePage()
        {
            List<Product> listpro = dbHelper.getProducts();
            // Chuyển đổi danh sách sản phẩm sang danh sách ViewModel
            List<ProductsViewModel> productsViewModel = new List<ProductsViewModel>();
            foreach (var pro in listpro)
            {
                var productViewModel = new ProductsViewModel
                {
                    ProductId = pro.ProductId,
                    Name = pro.Name,
                    Price = pro.Price,
                    Description = pro.Description,
                    Image = pro.Image,
                    CategoryName = pro.Category?.Name
                };
                productsViewModel.Add(productViewModel);
            }

            ViewBag.categories = dbHelper.GetCategories();
            ViewData["listpro"] = productsViewModel;
            return View();
        }

        // Chi tiết sản phẩm
        public IActionResult ProductDetails(int? productId)
        {
            ViewBag.categories = dbHelper.GetCategories();
            ViewBag.PageHeader = "Chi tiết sản phẩm";
            ViewBag.productDetails = dbHelper.GetProductDetails(productId);
            return View();
        }

        // Danh sách sản phẩm theo loại
        public IActionResult ListProductByCategory(int id)
        {
            List<Product> listpro = dbHelper.GetProductsByType(id);
            // Chuyển đổi danh sách sản phẩm sang danh sách ViewModel
            List<ProductsViewModel> productsViewModel = new List<ProductsViewModel>();
            foreach (var pro in listpro)
            {
                var productViewModel = new ProductsViewModel
                {
                    ProductId = pro.ProductId,
                    Name = pro.Name,
                    Price = pro.Price,
                    Description = pro.Description,
                    Image = pro.Image,
                    CategoryName = pro.Category?.Name
                };
                productsViewModel.Add(productViewModel);
            }

            ViewBag.categories = dbHelper.GetCategories();
            ViewData["listprotype"] = productsViewModel;
            return View();
        }
    }
}