using Microsoft.AspNetCore.Mvc;
using TH_DAPM_WebBanHangOnline.Models;

namespace TH_DAPM_WebBanHangOnline.Controllers
{
    public class OrderAdminController : Controller
    {
        DBHelper dBHelper;

        public OrderAdminController(DatabaseContext context)
        {
            dBHelper = new DBHelper(context);
        }

        // hiển thị danh sách
        public IActionResult Index()
        {
            ViewBag.pageTitle = "Đơn hàng";

            ViewBag.listOrder = dBHelper.GetListOrder();

            return View();
        }

        // chi tiết
        public IActionResult Details(int orderId)
        {
            ViewBag.pageTitle = "Chi tiết đơn hàng";

            ViewBag.titleAction = "Đơn hàng mã " + orderId;

            ViewBag.orderDetails = dBHelper.GetListOrderDetailsByOrderId(orderId);

            return View();
        }

        // chỉnh sửa
        public IActionResult Edit(int orderId)
        {
            ViewBag.pageTitle = "Chỉnh sửa đơn hàng";

            ViewBag.titleAction = "Đơn hàng mã " + orderId;

            ViewBag.orderDetails = dBHelper.GetListOrderDetailsByOrderId(orderId);

            return View();
        }

        [HttpPost]
        public IActionResult Edit(int orderId, string status)
        {
            dBHelper.UpdateStatusOrder(orderId, status);

            return RedirectToAction("Details", new { orderId = orderId});
        }
    }
}
