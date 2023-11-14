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
        public IActionResult Index(string titleAction)
        {
            // trang
            ViewBag.pageTitle = "Loại sản phẩm";

            // tiêu đề
            ViewBag.TitleAction = titleAction;

            // lấy danh sách loại sản phẩm
            ViewBag.listCategory = dBHelper.GetCategories();

            CategoryViewModel categoryViewModel = null;

            // Kiểm tra xem TempData có chứa dữ liệu không
            if (TempData.ContainsKey("categoryViewModel"))
            {
                string serializedData = TempData["categoryViewModel"] as string;

                if (!string.IsNullOrEmpty(serializedData))
                {
                    // Chuyển đổi JSON thành ChucNangViewModel
                    categoryViewModel = JsonConvert.DeserializeObject<CategoryViewModel>(serializedData);

                    // Chạy lại xác thực
                    if (!TryValidateModel(categoryViewModel, nameof(categoryViewModel)))
                    {
                        if (ModelState.IsValid == false)
                        {
                            AddModelError();
                        }
                    }
                }
            }

            return View(categoryViewModel);
        }

        private void AddModelError()
        {
            var errorList = new List<(string key, string errorMessage)>();

            foreach (var key in ModelState.Keys)
            {
                var errors = ModelState[key].Errors;

                foreach (var error in errors)
                {
                    errorList.Add((key, error.ErrorMessage));
                }
            }

            foreach (var error in errorList)
            {
                if (error.key.Split('.').Length == 2)
                {
                    ModelState.AddModelError(error.key.Split('.')[1], error.errorMessage);
                }
            }
        }

        // Thêm
        [HttpPost]
        public IActionResult Create(CategoryViewModel categoryViewModel)
        {
            ModelState.Remove("CategoryId");

            if (ModelState.IsValid ==  true)
            {
                Category category = categoryViewModel.ConvertClassModel();

                dBHelper.CreateCategory(category);
            }
            else
            {
                // Chuyển đổi ChucNangViewModel thành chuỗi JSON và lưu vào TempData
                TempData["categoryViewModel"] = JsonConvert.SerializeObject(categoryViewModel);
            }

            return RedirectToAction("Index");
        }
    }
}
