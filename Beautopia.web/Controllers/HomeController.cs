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
using Beautopia.web.Areas.Admin.Extensions;

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
			var Lang = HttpContext.Session.GetObjectFromJson<string>("Lang");
			HomeViewModel hmModel = new HomeViewModel();
			hmModel.SiteInfo = _users.GetSiteInfo();
			hmModel.Sliders=_users.GetSliders().Where(a => a.IsActive == true).ToList();
			hmModel.Offers = _users.GetOffers().Where(a => a.IsActive == true).ToList();
			hmModel.Doctors = _users.GetDoctor().Where(a => a.IsActive == true).ToList();
			hmModel.ServiceSubCategorys = _serviceRequest.GetAllServiceSubCategory().Where(a => a.IsActive == true).ToList();

			hmModel.Equipments = _users.GetEquipment().Where(a => a.IsActive == true).ToList();
			//hmModel.SiteInfo = _users.GetSiteInfo();
			if (Lang == null) { 
			HttpContext.Session.SetObjectAsJson("Lang", (hmModel.SiteInfo.IsArabicByDefault==true?"Ar":"En"));
			}
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
			var info= _users.GetSiteInfo();
			return View(info);
		}
		[Route("BeyondtheBrand")]
		public IActionResult BeyondtheBrand()
		
		{
						var aboutUS = _users.GetAboutUs();

			return View(aboutUS);
		}
		[Route("MessageFromCEO")]
		public IActionResult MessageFromCEO()

		{
			var aboutUS = _users.GetAboutUs();

			return View(aboutUS);
		}

		[Route("SERVICES")]
		public IActionResult SERVICES()
		{
			var equipments = _serviceRequest.GetAllServiceSubCategory().Where(a => a.IsActive == true).ToList();
			return View(equipments);
		}

		[Route("Devices")]
		public IActionResult Devices()
		{
			var equipments = _users.GetEquipment().Where(a => a.IsActive == true).ToList();
			return View(equipments);
		}
		[Route("GALLERY")]
		public IActionResult GALLERY()
		{
			var gallary=_users.GetSmileGillary();
			return View(gallary);
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


		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult SaveContactForm([Bind("name,message,email,phone")] ContactForm contactForm)
		{
			RequestService requestService = new RequestService();
			requestService.Name = contactForm.name;
			requestService.Mobile = contactForm.phone;
			requestService.Email = contactForm.email;
			requestService.Comments = contactForm.message;
			requestService.Service = "";

			_serviceRequest.InsertServiceRequest(requestService);


			return Json(contactForm);
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


		public JsonResult ChangeTheLanguage(string Lang)
		{
			 HttpContext.Session.SetObjectAsJson("Lang",Lang);


			return Json(Lang);
		}
	}
}
