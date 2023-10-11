using Microsoft.AspNetCore.Mvc;
using TH_DAPM_WebBanHangOnline.Models;
using TH_DAPM_WebBanHangOnline.Models.ClassModel;

namespace TH_DAPM_WebBanHangOnline.Controllers
{
    public class CartCustomerController : Controller
    {
        DBHelper dbHelper;

        public CartCustomerController(DatabaseContext context)
        {
            dbHelper = new DBHelper(context);
        }

        public IActionResult Index()
        {
            ViewBag.carts = dbHelper.GetMyCartByCustomerId(HttpContext.Session.GetInt32("CustomerId"));
            return View();
        }
        public IActionResult AddToCart(int quantity = 1 ,  int productid = -1)
        {   
            if(HttpContext.Session.GetInt32("CustomerId") == null)
            {
                return RedirectToAction("Login","Customer");
            }
            int customerid = (int)HttpContext.Session.GetInt32("CustomerId");
            DateTime dateTime = DateTime.Now;

            Cart cart = new Cart
            {
                ProductId = productid,
                
                CustomerId = customerid,
                Quantity = quantity,
                UpdateDay = dateTime.Day+"/" + dateTime.Month +"/"+ dateTime.Year.ToString()
            };

            dbHelper.AddItemToCart(cart);
            return View("Index");
        }
    }
}
