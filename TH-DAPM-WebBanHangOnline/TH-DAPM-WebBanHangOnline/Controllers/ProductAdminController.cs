using Microsoft.AspNetCore.Mvc;
using TH_DAPM_WebBanHangOnline.Models.ClassModel;
using TH_DAPM_WebBanHangOnline.Models.ViewModel;
using TH_DAPM_WebBanHangOnline.Models;
using Microsoft.Extensions.Hosting;

namespace TH_DAPM_WebBanHangOnline.Controllers
{
	public class ProductAdminController : Controller
	{
		DBHelper dbHelper;
		private readonly IWebHostEnvironment _hostEnvironment;

		public ProductAdminController(DatabaseContext context, IWebHostEnvironment hostEnvironment)
		{
			dbHelper = new DBHelper(context);
			_hostEnvironment = hostEnvironment;
		}
		public IActionResult Index()
		{
			ViewBag.pageTitle = "Sản Phẩm";

			List<Product> prO = dbHelper.getProducts();
			ViewData["ListPro"] = prO.Select(item => new ProductsViewModel
			{
				ProductId = item.ProductId,
				Name = item.Name,
				Price = item.Price,
				Description = item.Description,
				Image = item.Image,
				CategoryName = item.Category.Name,
				ProducerName = item.Producer.Name,
				ProducerId = item.Producer.ProducerId,
				CategoryId = item.Category.CategoryId
			});
			ViewBag.NotificationMessage = "Thành công!";
			return View();
		}

		[HttpGet]
		public IActionResult ViewDetails(int id)
		{
			ViewBag.titleAction = "Chi Tiết";
			Product producer = dbHelper.getProductById(id);
			ProductsViewModel producerView = new ProductsViewModel
			{
				ProductId = producer.ProductId,
				Name = producer.Name,
				Price = producer.Price,
				Description = producer.Description,
				Image = producer.Image,
				CategoryName = producer.Category.Name,
				ProducerName = producer.Producer.Name,
			};

			return PartialView("ViewDetails", producerView);
		}

		[HttpPost]
		public IActionResult EditProducer(ProductsViewModel producerView)
		{
			ModelState.Remove("ProducerId");
			if (ModelState.IsValid)
			{
				//Product producer = new Product
				//{

				//	ProductId = productsView.ProductId,
				//	Name = productsView.Name,
				//	Price = productsView.Price,
				//	Description = productsView.Description,
				//	Image = productsView.Image,
				//	CategoryId = productsView.CategoryId,
				//	ProducerId = productsView.ProducerId,
				//};

				//dbHelper.UpdateProducer(producer);
				return RedirectToAction("Index");
			}



			return RedirectToAction("Index");


		}
		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.titleAction = "Tạo";
			ViewBag.ListCate = dbHelper.GetCategories();
			ViewBag.ListProducer = dbHelper.GetProducers();
			return PartialView("Create");
		}
		[HttpPost]
		public IActionResult Create(ProductsViewModel productsView)
		{
			IFormFile imageFile = productsView.ImageFile;

			if (imageFile != null && imageFile.Length > 0)
			{
				// Đảm bảo đường dẫn thư mục image
				var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "img");

				// Tạo tên tệp ảnh duy nhất bằng cách sử dụng Guid và đuôi tệp ảnh ban đầu
				var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

				// Kết hợp đường dẫn đến thư mục image và tên tệp ảnh để có đường dẫn đầy đủ
				var filePath = Path.Combine(imagePath, fileName);

				// Lưu tệp ảnh vào thư mục image
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					imageFile.CopyTo(stream);
				}

				// Lưu đường dẫn tệp ảnh vào thuộc tính Image của sản phẩm
				productsView.Image = fileName;
			}

			Product product = new Product
			{
				ProductId = productsView.ProductId,
				Name = productsView.Name,
				Price = productsView.Price,
				Description = productsView.Description,
				Image = productsView.Image,
				CategoryId = productsView.CategoryId,
				ProducerId = productsView.ProducerId,
			};

			dbHelper.CreatePro(product);
			return RedirectToAction("Index");
		}


		public IActionResult Delete(int id)
		{

			dbHelper.DeletePro(id);

			return RedirectToAction("Index");
		}
	}
}
