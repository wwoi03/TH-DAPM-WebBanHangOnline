﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            ViewBag.categories = dbHelper.GetCategories();
            ViewBag.carts = dbHelper.GetMyCartByCustomerId(HttpContext.Session.GetInt32("CustomerId"));
            return View();
        }
    }
}
