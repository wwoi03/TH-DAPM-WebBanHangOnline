using Microsoft.AspNetCore.Mvc;
using TH_DAPM_WebBanHangOnline.Models.ClassModel;
using TH_DAPM_WebBanHangOnline.Models.ViewModel;
using TH_DAPM_WebBanHangOnline.Models;

namespace TH_DAPM_WebBanHangOnline.Controllers
{
    public class ProductAdminController : Controller
    {		
			DBHelper dbHelper;

			public ProductAdminController(DatabaseContext context)
			{
				dbHelper = new DBHelper(context);
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
					CategoryName=producer.Category.Name,
					ProducerName=producer.Producer.Name,
				};

				return PartialView("ViewDetails", producerView);
			}

			[HttpPost]
			public IActionResult EditProducer(ProducerViewModel producerView)
			{
				ModelState.Remove("ProducerId");
				if (ModelState.IsValid)
				{
					Producer producer = new Producer
					{
						ProducerId = producerView.ProducerId,
						Phone = producerView.Phone,
						Name = producerView.Name,
						Address = producerView.Address,
					};

					dbHelper.UpdateProducer(producer);
					return RedirectToAction("Index");
				}



				return RedirectToAction("Index");


			}
			[HttpGet]
			public IActionResult Create()
			{
				ViewBag.titleAction = "Tạo";
				return PartialView("Create");
			}
			[HttpPost]
			public IActionResult Create(ProducerViewModel producerView)
			{

				Producer producer = new Producer
				{

					Phone = producerView.Phone,
					Name = producerView.Name,
					Address = producerView.Address,
				};
				dbHelper.CreateProducer(producer);
				return RedirectToAction("Index");
			}


			public IActionResult Delete(int id)
			{

				dbHelper.DeleteProducer(id);

				return RedirectToAction("Index");
			}
		}
	}

