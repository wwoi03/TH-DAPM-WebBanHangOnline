using Microsoft.AspNetCore.Mvc;
using TH_DAPM_WebBanHangOnline.Models;
using TH_DAPM_WebBanHangOnline.Models.ClassModel;
using TH_DAPM_WebBanHangOnline.Models.ViewModel;

namespace TH_DAPM_WebBanHangOnline.Controllers
{
    public class CustomerController : Controller
    {
        DBHelper dbHelper;

        public CustomerController(DatabaseContext context)
        {
            dbHelper = new DBHelper(context);
        }

        public IActionResult Index()
        {

            return View();
        }

        // Đăng nhập
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("HomePage", "HomeCustomer");
            }
        }

        [HttpPost]
        public IActionResult Login(CustomerViewModel customerVM)
        {
            ModelState.Remove("Name");
            ModelState.Remove("VerifyPassword");

            if (ModelState.IsValid)
            {
                Customer customer = dbHelper.GetCustomerByEmail(customerVM.Email);

                // kiểm tra tên đăng nhập đã có trong hệ thông
                if (customer != null)
                {
                    // Kiểm tra mật khậu
                    if (customer.Password.Equals(customerVM.Password))
                    {
                        HttpContext.Session.SetString("Email", customer.Email);
                        HttpContext.Session.SetString("Name", customer.Name);
                        HttpContext.Session.SetInt32("CustomerId", customer.CustomerId);
                        HttpContext.Session.SetInt32("countOrder", dbHelper.GetOrderByCustomerId(customer.CustomerId).Count);
                        HttpContext.Session.SetInt32("countCart", dbHelper.GetMyCartByCustomerId(customer.CustomerId).Count);

                        return RedirectToAction("HomePage", "HomeCustomer");
                    }
                    ViewBag.messageError = "Mật khẩu không chính xác!";

                    return View();
                }

                ViewBag.messageError = "Tên đăng nhập hoặc mật khẩu không chính xác!";
            }
            return View();
        }


        // Đăng ký tài khoản
        [HttpGet]
        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(CustomerViewModel customerVM)
        {
            ModelState.Remove("Name");

            if (ModelState.IsValid)
            {
                Customer customerExists = dbHelper.GetCustomerByEmail(customerVM.Email);

                // kiểm tra tài khoản đã tồn tài
                if (customerExists == null) // chưa tồn tại
                {
                    // kiểm tra xác nhận mật khẩu
                    if (customerVM.Password.Equals(customerVM.VerifyPassword))
                    {

                        Customer customer = new Customer()
                        {
                            Email = customerVM.Email,
                            Name = customerVM.Email.Split("@")[0],
                            Password = customerVM.Password
                        };

                        dbHelper.CreateCustomer(customer);

                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.messageError = "Mật khẩu xác nhận không khớp";
                    }
                }
                else // đã tồn tại
                {
                    ViewBag.messageError = "Tài khoản đã tồn tại";
                }
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("HomePage", "HomeCustomer");
        }
    }
}
