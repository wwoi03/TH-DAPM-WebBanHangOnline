using Microsoft.AspNetCore.Mvc;

namespace TH_DAPM_WebBanHangOnline.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
