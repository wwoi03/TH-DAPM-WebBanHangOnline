using Microsoft.AspNetCore.Mvc;
using TH_DAPM_WebBanHangOnline.Models;
using TH_DAPM_WebBanHangOnline.Models.ClassModel;
using TH_DAPM_WebBanHangOnline.Models.ViewModel;

namespace TH_DAPM_WebBanHangOnline.Controllers
{
	public class ProducerAdminController : Controller
	{

		DBHelper dbHelper;

		public ProducerAdminController(DatabaseContext context)
		{
			dbHelper = new DBHelper(context);
		}
		public IActionResult Index()
		{
			ViewBag.pageTitle = "Nhà Sản Xuất";

			List<Producer> producers = dbHelper.GetProducers();
			ViewData["ListProducer"] = producers.Select(item => new ProducerViewModel
			{
				ProducerId = item.ProducerId,
				Name = item.Name,
				Address = item.Address,
				Phone = item.Phone,
			});
			ViewBag.NotificationMessage = "Thành công!";
			return View();
		}
		
		[HttpGet]
		public IActionResult ViewDetails(int id)
		{
			ViewBag.titleAction = "Chi Tiết";
			Producer producer = dbHelper.GetProducerById(id);
			ProducerViewModel producerView = new ProducerViewModel
			{
				ProducerId = producer.ProducerId,
				Name = producer.Name,
				Address = producer.Address,
				Phone = producer.Phone,
			};

			return PartialView("ViewDetails", producerView);
		}

		[HttpPost]
		public IActionResult EditProducer(ProducerViewModel producerView)
		{
			ModelState.Remove("ProducerId");
			if(ModelState.IsValid)
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
		public IActionResult Create(ProducerViewModel producerView) {

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
