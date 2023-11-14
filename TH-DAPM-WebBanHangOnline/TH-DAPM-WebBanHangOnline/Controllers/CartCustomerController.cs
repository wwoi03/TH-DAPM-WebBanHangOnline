using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks.Dataflow;
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

        // hiển thị danh sách sản phẩm trong giỏ hàng
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("countCart", dbHelper.GetCountMyCart((int)HttpContext.Session.GetInt32("CustomerId")));
            ViewBag.categories = dbHelper.GetCategories();
            ViewBag.carts = dbHelper.GetMyCartByCustomerId(HttpContext.Session.GetInt32("CustomerId"));
            return View();
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
        public IActionResult Checkout(string productCheckout)
        {
            // lấy danh sách loại sản phẩm
            ViewBag.categories = dbHelper.GetCategories();

            // chuyển đổi chuỗi Json thành mảng
            List<string> listCartId = JsonConvert.DeserializeObject<List<string>>(productCheckout);

            // lấy các sản phẩm trong giỏ hàng cần thanh toán
            List<Cart> listCart = new List<Cart>();

            for (int i = 0; i < listCartId.Count; i++)
            {
                listCart.Add(dbHelper.GetCartById(int.Parse(listCartId[i])));
            }

            ViewBag.listCart = listCart;

            return View();
        }
    }
}
