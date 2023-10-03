using Microsoft.AspNetCore.Mvc;

namespace TH_DAPM_WebBanHangOnline.Controllers
{
    public class OrderCustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }
    }
}
