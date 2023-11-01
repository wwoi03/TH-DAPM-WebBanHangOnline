﻿using Microsoft.AspNetCore.Mvc;
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
    }
}
