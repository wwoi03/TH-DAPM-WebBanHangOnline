using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks.Dataflow;
using TH_DAPM_WebBanHangOnline.Models;
using TH_DAPM_WebBanHangOnline.Models.ClassModel;
using TH_DAPM_WebBanHangOnline.Models.ViewModel;

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
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Customer");
            } 
            else
            {
                HttpContext.Session.SetInt32("countCart", dbHelper.GetCountMyCart((int)HttpContext.Session.GetInt32("CustomerId")));
                ViewBag.categories = dbHelper.GetCategories();
                ViewBag.carts = dbHelper.GetMyCartByCustomerId(HttpContext.Session.GetInt32("CustomerId"));

                return View();
            }
        }

        // xóa sản phẩm trong giỏ hàng
        public IActionResult Delete(int cartId)
        {
            dbHelper.DeleteProductInCart(cartId);


            return RedirectToAction("Index");
        }
		
		public IActionResult AddToCart(int quantity = 1, int productid = -1)
        {
            if (HttpContext.Session.GetInt32("CustomerId") == null)
            {
                return RedirectToAction("Login", "Customer");
            }

            int customerid = (int) HttpContext.Session.GetInt32("CustomerId");
            DateTime dateTime = DateTime.Now;

            Cart cart = new Cart
            {
                ProductId = productid,
                CustomerId = customerid,
                Quantity = quantity,
            };

            dbHelper.AddItemToCart(cart);

            HttpContext.Session.SetInt32("countCart", dbHelper.GetCountMyCart((int)HttpContext.Session.GetInt32("CustomerId")));

            return RedirectToAction("Index");
        }

		[Route("/CartCustomer/AddToCartToProductDetals/{productid}/{quantity}")]
		public IActionResult AddToCartToProductDetals(int quantity = 1, int productid = -1)
        {
            if (HttpContext.Session.GetInt32("CustomerId") == null)
            {
                return Json(new { redirectUrl = Url.Action("Login", "Customer") });
            }

            int customerid = (int)HttpContext.Session.GetInt32("CustomerId");
            DateTime dateTime = DateTime.Now;

            Cart cart = new Cart
            {
                ProductId = productid,
                CustomerId = customerid,
                Quantity = quantity,
            };

            dbHelper.AddItemToCart(cart);

            HttpContext.Session.SetInt32("countCart", dbHelper.GetCountMyCart((int)HttpContext.Session.GetInt32("CustomerId")));

            return Json(new { redirectUrl = Url.Action("Index", "CartCustomer") });
        }


		//Edit Quantity
		[Route("/CartCustomer/EditQuantityPro/{cartId}/{quantity}")]
		[HttpGet]
        public IActionResult EditQuantityPro(int cartId, int quantity)
        {
            double total;
            if(cartId!=0)
            {
				dbHelper.EditQuantityPro(cartId, quantity,out total);
				return Json(total.ToString("0.00"));
			}
            return Json("null");
        }

        // Thanh toán
        public IActionResult Checkout()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Customer");
            }

            // lấy danh sách loại sản phẩm
            ViewBag.categories = dbHelper.GetCategories();

            // lấy sản phẩm trong giỏ hàng
            ViewBag.listCartCheckout = dbHelper.GetMyCartByCustomerId((int)HttpContext.Session.GetInt32("CustomerId"));

            if (ViewBag.listCartCheckout.Count == 0)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Checkout(OrderViewModel orderViewModel)
        {
            ModelState.Remove("OrderId");
            ModelState.Remove("CustomerId");
            ModelState.Remove("Customer");
            ModelState.Remove("OrderDay");
            ModelState.Remove("Status");

            // lấy danh sách loại sản phẩm
            ViewBag.categories = dbHelper.GetCategories();

            // lấy sản phẩm trong giỏ hàng
            ViewBag.listCartCheckout = dbHelper.GetMyCartByCustomerId((int)HttpContext.Session.GetInt32("CustomerId"));

            if (ModelState.IsValid)
            {
                orderViewModel.CustomerId = (int)HttpContext.Session.GetInt32("CustomerId");

                Order order = orderViewModel.ConvertOrder();

                dbHelper.CreateOrder(order, ViewBag.listCartCheckout);

                return RedirectToAction("Index", "OrderCustomer");
            }
            else
            {
                return View(orderViewModel);
            }
        }
    }
}
