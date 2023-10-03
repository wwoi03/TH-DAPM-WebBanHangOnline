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
            if (HttpContext.Session.GetString("Username") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "HomeCustomer");
            }
        }

        [HttpPost]
        public IActionResult Login(CustomerViewModel customerVM)
        {
            ModelState.Remove("Name");
            ModelState.Remove("VerifyPassword");

            if (HttpContext.Session.GetString("Email") == null)
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
                // kiểm tra xác nhận mật khẩu
                if (customerVM.Password.Equals(customerVM.VerifyPassword)) {
                    Customer customerExists = dbHelper.GetCustomerByEmail(customerVM.Email);

                    // kiểm tra tài khoản đã tồn tài
                    if (customerExists == null) // chưa tồn tại
                    {
                        Customer customer = new Customer()
                        {
                            Email = customerVM.Email,
                            Name = customerVM.Email.Split("@")[0],
                            Password = customerVM.Password
                        };

                        dbHelper.CreateCustomer(customer);

                        return RedirectToAction("Login");
                    } else // đã tồn tại
                    {
                        ViewBag.messageError = "Tài khoản đã tồn tại";
                    }
                } else 
                {
                    ViewBag.messageError = "Mật khẩu xác nhận không khớp";
                }
            }

            return View();
        }
    }
}
