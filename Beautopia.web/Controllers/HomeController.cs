using Beautopia.Model;
using Beautopia.Model.Area;
using Beautopia.Services.DataAppService;
using Beautopia.Services.IDataService;
using Beautopia.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Beautopia.web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private static IServiceRequest _serviceRequest;
		private static IUsers _users;
		//private static ServiceRequestAppService serviceRequest=new ServiceRequestAppService();
		public HomeController(ILogger<HomeController> logger, IServiceRequest serviceRequest, IUsers users)
		{
			_serviceRequest = serviceRequest;
			_logger = logger;
			_users = users;
		}

		public IActionResult Index()
		{
			HomeViewModel hmModel = new HomeViewModel();

			hmModel.Sliders=_users.GetSliders().Where(a => a.IsActive == true).ToList();
			hmModel.Offers = _users.GetOffers().Where(a => a.IsActive == true).ToList();
			hmModel.Doctors = _users.GetDoctor().Where(a => a.IsActive == true).ToList();

			return View(hmModel);
		}
		[Route("Privacy")]
		public IActionResult Privacy()
		{
			return View();
		}
		[Route("ContactUs")]
		public IActionResult ContactUs()
		{
			return View();
		}
		[Route("AboutUs")]
		public IActionResult AboutUs()
		{
			return View();
		}
		[Route("SERVICES")]
		public IActionResult SERVICES()
		{
			return View();
		}
		[Route("GALLERY")]
		public IActionResult GALLERY()
		{
			return View();
		}
		[Route("OURSPECIALISTS")]
		public IActionResult OURSPECIALISTS()
		{
			HomeViewModel hmModel = new HomeViewModel();

			//hmModel.Sliders = _users.GetSliders().Where(a => a.IsActive == true).ToList();
			//hmModel.Offers = _users.GetOffers().Where(a => a.IsActive == true).ToList();
			hmModel.Doctors = _users.GetDoctor().Where(a => a.IsActive == true).ToList();

			return View(hmModel);
		}
		[Route("TESTIMONIALS")]
		public IActionResult TESTIMONIALS()
		{
			return View();
		}
		[Route("RequestService")]
		public IActionResult RequestService(int CategoryID)
		
		
		{
			
			
			return View();
		}


		
		//[ValidateAntiForgeryToken]
		public JsonResult GetServiceCategory()
		{
			var data=_serviceRequest.GetServiceCategory();


			return Json(data);
		}

		
		//[ValidateAntiForgeryToken]
		public JsonResult GetServiceSubCategory(int CategoryID)
		{
			var data = _serviceRequest.GetServiceSubCategory(CategoryID);


			return Json(data);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult SaveRequestService( [Bind("Service,Name,Mobile,Email,Comments,Source,_listOfServices")] RequestService requestService)
		{
			requestService.SType = "RequestService";
			var listOfServices = new List<string>();

			    listOfServices =(requestService._listOfServices == null? listOfServices : JsonConvert.DeserializeObject<List<string>>(requestService._listOfServices));


			int returnServoceID=_serviceRequest.InsertServiceRequest(requestService);

			foreach (var item in listOfServices) {
				RequestServiceAndSubCategoryMapping map = new RequestServiceAndSubCategoryMapping();
				map.RequestServiceID = returnServoceID;
				map.ServiceSubCategoryID =Convert.ToInt32(item);

				_serviceRequest.InsertRequestServiceAndSubCategoryMapping(map);
			}

			return Json(requestService);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult SaveRequestForm([Bind("requestname,requestmessages,requestemail,requestservice,requestphone")] RequestForm requestForm)
		{
			RequestService requestService = new RequestService();
			requestService.Name = requestForm.requestname;
			requestService.Mobile = requestForm.requestphone;
			requestService.Email = requestForm.requestemail;
			requestService.Comments = requestForm.requestmessages;
			requestService.Service = requestForm.requestservice;

			_serviceRequest.InsertServiceRequest(requestService);


			return Json(requestService);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult SaveBookingorm([Bind("bookingname,bookingphone,bookingemail,bookingmessage,bookingservice")] BookingForm bookingForm)
		{
			RequestService requestService = new RequestService();
			requestService.Name = bookingForm.bookingname;
			requestService.Mobile = bookingForm.bookingphone;
			requestService.Email = bookingForm.bookingemail;
			requestService.Comments = bookingForm.bookingmessage;
			requestService.Service = bookingForm.bookingservice;

			_serviceRequest.InsertServiceRequest(requestService);


			return Json(bookingForm);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
