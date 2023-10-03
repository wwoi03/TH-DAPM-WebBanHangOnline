using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TH_DAPM_WebBanHangOnline.Models;

namespace TH_DAPM_WebBanHangOnline.Controllers
{
    public class HomeCustomerController : Controller
    {
        DBHelper dbHelper;

        public HomeCustomerController(DatabaseContext context)
        {
            dbHelper = new DBHelper(context);
        }

        public IActionResult HomePage()
        {
            return View();
        }

        // Chi tiết sản phẩm
        public IActionResult ProductDetails(int? productId)
        {
            ViewBag.PageHeader = "Chi tiết sản phẩm";
            ViewBag.productDetails = dbHelper.GetProductDetails(productId);
            return View();
        }

        // Danh sách sản phẩm theo loại
        public IActionResult ListProductByCategory(int? categoryId)
        {
            return View();
        }
    }
}