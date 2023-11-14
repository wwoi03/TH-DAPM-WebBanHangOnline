using Microsoft.AspNetCore.Mvc;
using TH_DAPM_WebBanHangOnline.Models;
using TH_DAPM_WebBanHangOnline.Models.ClassModel;
using TH_DAPM_WebBanHangOnline.Models.ViewModel;

namespace TH_DAPM_WebBanHangOnline.Controllers
{
    public class AdminController : Controller
    {
        DBHelper dbHelper;

        public AdminController(DatabaseContext context)
        {
            dbHelper = new DBHelper(context);
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        // Đăng nhập
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("EmailAdmin") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Dashboard");
            }
        }

        [HttpPost]
        public IActionResult Login(AdminUserViewModel adminUserViewModel)
        {
            ModelState.Remove("Name");
            ModelState.Remove("AdminUserId");
            ModelState.Remove("Role");

            if (ModelState.IsValid)
            {
                AdminUser adminUser = dbHelper.LoginAdmin(adminUserViewModel.Email, adminUserViewModel.Password);

                if (adminUser != null)
                {
                    HttpContext.Session.SetString("EmailAdmin", adminUser.Email);
                    HttpContext.Session.SetString("NameAdmin", adminUser.Name);
                    HttpContext.Session.SetInt32("AdminId", adminUser.AdminUserId);

                    return RedirectToAction("Dashboard");
                }

                ViewBag.messageError = "Tên đăng nhập hoặc mật khẩu không chính xác!";
            }
            return View();
        }
    }
}
