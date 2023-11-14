using Microsoft.AspNetCore.Mvc;
using TH_DAPM_WebBanHangOnline.Models;
using TH_DAPM_WebBanHangOnline.Models.ViewModel;
using TH_DAPM_WebBanHangOnline.Models.ClassModel;

namespace TH_DAPM_WebBanHangOnline.Controllers
{
    public class OrderCustomerController : Controller
    {
        DBHelper dbHelper;

        public OrderCustomerController(DatabaseContext context)
        {
            dbHelper = new DBHelper(context);
        }

        // danh sách đơn hàng
        public IActionResult Index(int orderId)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Customer");
            } 
            else
            {
                // lấy danh sách loại sản phẩm
                ViewBag.categories = dbHelper.GetCategories();

                // lấy danh sách order
                ViewBag.listOrder = dbHelper.GetOrderByCustomerId((int)HttpContext.Session.GetInt32("CustomerId"));

                if (orderId != 0)
                {
                    ViewBag.listOrderDetails = dbHelper.GetListOrderDetailsByOrderId(orderId);
                }

                return View();
            }
        }

        // chi tiết đơn hàng
        public IActionResult Details(int orderId)
        {
            return RedirectToAction("Index", new { orderId = orderId });
        }
    }
}
