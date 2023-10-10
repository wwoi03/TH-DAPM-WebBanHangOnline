using Microsoft.AspNetCore.Mvc;
using TH_DAPM_WebBanHangOnline.Models;

namespace TH_DAPM_WebBanHangOnline.Controllers
{
    public class CartCustomerController : Controller
    {
        DBHelper dbHelper;

        public CartCustomerController(DatabaseContext context)
        {
            dbHelper = new DBHelper(context);
        }

        // hiển thị danh sách sản phẩm trong giỏ hàng
        public IActionResult Index()
        {
            ViewBag.categories = dbHelper.GetCategories();
            ViewBag.carts = dbHelper.GetMyCartByCustomerId(HttpContext.Session.GetInt32("CustomerId"));
            return View();
        }

        // xóa sản phẩm trong giỏ hàng
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            dbHelper.DeleteProductInCart(int.Parse(id));
            return StatusCode(200);
        }
    }
}
