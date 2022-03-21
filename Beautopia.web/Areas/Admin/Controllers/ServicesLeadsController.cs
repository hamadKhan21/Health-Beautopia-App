using Beautopia.Services.IDataService;
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
			var data = _serviceRequest.GetRequestService("");


			return Json(data);
		}
	}
}
