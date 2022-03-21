using Beautopia.Model;
using Beautopia.Services.IDataService;
using Beautopia.web.Areas.Admin.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beautopia.web.Areas.Admin.Extensions;
using Beautopia.Model.Area;

namespace Beautopia.web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[JSONAuth]
	public class SettingsController : Controller
	{

		private static IServiceRequest _serviceRequest;
		private static UserLogin login = new UserLogin();
		//private static ServiceRequestAppService serviceRequest=new ServiceRequestAppService();
		public SettingsController(IServiceRequest serviceRequest)
		{
			 //login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			_serviceRequest = serviceRequest;
		
		}
		[SessionTimeout]
		[Route("Admin/GenerateURL")]
		public IActionResult Index()
		{
			return View();
		}


		[SessionTimeout]
		[Route("Admin/ManageServices")]
		public IActionResult ManageServices()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult SaveUpdateServices([Bind("ID,CategoryNameEn,CategoryNameAr,IsActiveChecked")] ServiceCategory obj)
		{
			var login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");

			obj.IsActive = (obj.IsActiveChecked == null ? false : true);

			 _serviceRequest.InsertUpdateServiceCategory(obj, login.UserName);
			var servicecate=_serviceRequest.GetAllServiceCategory();

			//foreach (var item in listOfServices)
			//{
			//	RequestServiceAndSubCategoryMapping map = new RequestServiceAndSubCategoryMapping();
			//	map.RequestServiceID = returnServoceID;
			//	map.ServiceSubCategoryID = Convert.ToInt32(item);

			//	_serviceRequest.InsertRequestServiceAndSubCategoryMapping(map);
			//}

			return Json(servicecate);
		}

		public JsonResult GetAllServiceCategory()
		{
			var data = _serviceRequest.GetAllServiceCategory();


			return Json(data);
		}


		[SessionTimeout]
		[Route("Admin/ManageSubServices")]
		public IActionResult ManageSubServices()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult SaveUpdateSubServices([Bind("ID,SubCategoryNameEn,SubCategoryNameAr,IsActiveChecked,ServicePrice,CategoryID")] ServiceSubCategory obj)
		{
			var login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");

			obj.IsActive = (obj.IsActiveChecked == null ? false : true);

			_serviceRequest.InsertUpdateServiceSubCategory(obj, login.UserName);
			var servicecate = _serviceRequest.GetAllServiceSubCategory();

			//foreach (var item in listOfServices)
			//{
			//	RequestServiceAndSubCategoryMapping map = new RequestServiceAndSubCategoryMapping();
			//	map.RequestServiceID = returnServoceID;
			//	map.ServiceSubCategoryID = Convert.ToInt32(item);

			//	_serviceRequest.InsertRequestServiceAndSubCategoryMapping(map);
			//}

			return Json(servicecate);
		}

		public JsonResult GetAllSubServiceCategory()
		{
			var data = _serviceRequest.GetAllServiceSubCategory();


			return Json(data);
		}


		public JsonResult GetServiceCategory()
		{
			var data = _serviceRequest.GetServiceCategory();


			return Json(data);
		}


		//[ValidateAntiForgeryToken]
		public JsonResult GetServiceSubCategory(int CategoryID)
		{
			var data = _serviceRequest.GetServiceSubCategory(CategoryID);


			return Json(data);
		}

		public JsonResult GetSources()
		{
			var data = _serviceRequest.GetSources();


			return Json(data);
		}
	}
}
