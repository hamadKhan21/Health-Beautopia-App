using Beautopia.Model.Area;
using Beautopia.Services.IDataService;
using Beautopia.web.Areas.Admin.Extensions;
using Beautopia.web.Areas.Admin.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beautopia.web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[JSONAuth]
	public class ServicesLeadsController : Controller
	{
		private static IServiceRequest _serviceRequest;

		public ServicesLeadsController(IServiceRequest serviceRequest) {

			_serviceRequest = serviceRequest;
		}
		[SessionTimeout]
		[Route("Dashboard")]
		public IActionResult Dashboard()
		{
			return View();
		}
		[SessionTimeout]
		[Route("Admin/ServicesLeads")]
		public IActionResult Index()
		{
			return View();
		}
		[SessionTimeout]
		[Route("Admin/LeadsManagement")]
		public IActionResult LeadsManagement()
		{
			return View();
		}

		public JsonResult GetRequestServices()
		{
			var login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			// login.UserName;


			var data = _serviceRequest.GetRequestServiceIfActivity(login.UserName,login.RoleID);


			return Json(data);
		}
		public JsonResult GetActivityType()
		{
			var data = _serviceRequest.GetActivityType();


			return Json(data);
		}

		public JsonResult GetRS_Activity(int RequestServiceID)
		{
			var data = _serviceRequest.GetRS_Activity(RequestServiceID);


			return Json(data);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult InsertRS_Activity([Bind("ActivityTypeID,RequestServiceID,Comments")] RS_Activity obj)
		{
			var login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			obj.CreatedBy = login.UserName;


			_serviceRequest.InsertRS_Activity(obj);
			var servicecate = _serviceRequest.GetRS_Activity(obj.RequestServiceID);



			return Json(servicecate);
		}
	}
}
