using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TH_DAPM_WebBanHangOnline.Models;
using TH_DAPM_WebBanHangOnline.Models.ClassModel;
using TH_DAPM_WebBanHangOnline.Models.ViewModel;

namespace TH_DAPM_WebBanHangOnline.Controllers
{
    public class CategoriesAdminController : Controller
    {
        DBHelper dBHelper;

        public CategoriesAdminController(DatabaseContext context)
        {
            dBHelper = new DBHelper(context);
        }

        // danh sách loại sản phẩm
        public IActionResult Index()
        {
            // trang
            ViewBag.pageTitle = "Loại sản phẩm";

            // lấy danh sách loại sản phẩm
            ViewBag.listCategory = dBHelper.GetCategories();
            CategoryViewModel categoryViewModel = new CategoryViewModel()
            {
                CategoryId = 1,
            };
            return View();
        }

        // Thêm
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel categoryViewModel)
        {
            ModelState.Remove("CategoryId");

            if (ModelState.IsValid)
            {
                Category category = categoryViewModel.ConvertClassModel();

                dBHelper.CreateCategory(category);

                return RedirectToAction("Index");
            }

            return PartialView("Create", categoryViewModel);
        }
    }
}
