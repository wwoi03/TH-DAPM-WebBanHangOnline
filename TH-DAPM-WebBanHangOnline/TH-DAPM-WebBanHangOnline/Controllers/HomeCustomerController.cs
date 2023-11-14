using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TH_DAPM_WebBanHangOnline.Models;
using TH_DAPM_WebBanHangOnline.Models.ClassModel;
using TH_DAPM_WebBanHangOnline.Models.ViewModel;

namespace TH_DAPM_WebBanHangOnline.Controllers
{
    public class HomeCustomerController : Controller
    {
        DBHelper dbHelper;

        public HomeCustomerController(DatabaseContext context)
        {
            dbHelper = new DBHelper(context);
        }
        //trang chủ sản phẩm khách thấy
        public IActionResult HomePage()
        {
            List<Product> listpro = dbHelper.getProducts();
            // Chuyển đổi danh sách sản phẩm sang danh sách ViewModel
            List<ProductsViewModel> productsViewModel = new List<ProductsViewModel>();
            foreach (var pro in listpro)
            {
                var productViewModel = new ProductsViewModel
                {
                    ProductId = pro.ProductId,
                    Name = pro.Name,
                    Price = pro.Price,
                    Description = pro.Description,
                    Image = pro.Image,
                    CategoryName = pro.Category?.Name
                };
                productsViewModel.Add(productViewModel);
            }

            ViewBag.categories = dbHelper.GetCategories();
            ViewData["listpro"] = productsViewModel;
            return View();
        }

        // Chi tiết sản phẩm
        public IActionResult ProductDetails(int? productId)
        {
            ViewBag.categories = dbHelper.GetCategories();
            ViewBag.PageHeader = "Chi tiết sản phẩm";
            ViewBag.productDetails = dbHelper.GetProductDetails(productId);
            ViewBag.commentsByProduct = dbHelper.GetCommentsByProductId(productId);
            return View();
        }

        // Danh sách sản phẩm theo loại
        public IActionResult ListProductByCategory(int id)
        {
            HttpContext.Session.Remove("Categoryid");
            HttpContext.Session.SetInt32("Categoryid", id);
            List<Product> listpro = dbHelper.GetProductsByType(id);
            // Chuyển đổi danh sách sản phẩm sang danh sách ViewModel

            List<ProductsViewModel> productsViewModel = new List<ProductsViewModel>();
            foreach (var pro in listpro)
            {
                var productViewModel = new ProductsViewModel
                {
                    ProductId = pro.ProductId,
                    Name = pro.Name,
                    Price = pro.Price,
                    Description = pro.Description,
                    Image = pro.Image,
                    CategoryName = pro.Category?.Name
                };
                productsViewModel.Add(productViewModel);
            }

            ViewBag.categories = dbHelper.GetCategories();
            ViewData["listprotype"] = productsViewModel;
            return View();
        }

        // bình luận trên sản phẩm
        [HttpPost]
        public IActionResult AddComment(CommentViewModel commentViewModel)
        {
            Comment comment = new Comment()
            {
                CustomerId = (int) HttpContext.Session.GetInt32("CustomerId"),
                ProductId = commentViewModel.ProductId,
                CommentContent = commentViewModel.CommentContent,
                StarRating = 5,
                CommentDate = DateTime.Now
            };

            dbHelper.AddComment(comment);

            return RedirectToAction("ProductDetails", new { productId = commentViewModel.ProductId });
        }
        //tìm kiếm sản phẩm
        public IActionResult SearchProduct(string valuesSearch)
        {
            HttpContext.Session.Remove("SearchProductResult");
            ViewBag.categories = dbHelper.GetCategories();
            List<Product> products = dbHelper.GetListProductByName(valuesSearch);
            HttpContext.Session.SetString("SearchProductResult", valuesSearch);
            ViewData["ResultSeacrchPro"] = products.Select(item => new ProductsViewModel
            {
                ProductId = item.ProductId,
                Name = item.Name,
                Price = item.Price,
                Description = item.Description,
                Image=item.Image,
                CategoryName=item.Category.Name
            }).ToList();
           
            return View();
        }



        //sap xep theo gia san pham
        [Route("/HomeCustomer/SortProByPrice/{price}")]
        public IActionResult SortProByPrice(int price)
        {
            List<Product> products= new List<Product>();
            ViewBag.categories = dbHelper.GetCategories();
            if (HttpContext.Session.GetString("SearchProductResult") != null)
            {
               products = dbHelper.GetListProductByName(HttpContext.Session.GetString("SearchProductResult"));
            }
            else
            {
                products = dbHelper.GetProductsByType((int)HttpContext.Session.GetInt32("Categoryid"));
            }               
                // Sort the product list by price         
                if (price == 1)
                products= products.Where(p => p.Price <= 30 && p.Price > 0).ToList();
                else if (price == 2)
                products = products.Where(p => p.Price <= 60 && p.Price >= 30).ToList();
                else if (price == 3)
                products = products.Where(p => p.Price <= 90 && p.Price >= 60).ToList();
                else
                products = products.Where(p => p.Price >= 90).ToList();

                ViewData["ResultSort"] = products.Select(item => new ProductsViewModel
                {
                    ProductId = item.ProductId,
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description,
                    Image = item.Image,
                    CategoryName = item.Category.Name
                }).ToList();

                return PartialView("SortProByPrice");
            
           
        }


    }
}