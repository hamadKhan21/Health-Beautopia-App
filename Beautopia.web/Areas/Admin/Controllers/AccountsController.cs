using Beautopia.Model.Area;
using Beautopia.Services.IDataService;
using Beautopia.web.Areas.Admin.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beautopia.web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AccountsController : Controller
	{
		private static IServiceRequest _serviceRequest;
		public AccountsController( IServiceRequest serviceRequest)
		{
			_serviceRequest = serviceRequest;
			
		}
		[Route("Admin")]
		public IActionResult Login()
		{
			var login= HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			if (login != null) {
				HttpContext.Session.SetSessionNull();
			}

			return View();
		}
		[ValidateAntiForgeryToken]
		public JsonResult Validate([Bind("UserName,Password")] string UserName, string Password)

		{
			var UsrName = "0";
			try
			{
			

				var login = _serviceRequest.UserLogin(UserName, Password);

				if (login.UserName == null)
				{
					//Session["UserLogin"] = login;
					UsrName = "0";

				}
				else
				{

					HttpContext.Session.SetObjectAsJson("Login", login);
					UsrName = "1";


				}
			}
			catch (Exception ex) {

				UsrName = "-1";
			}
			return Json(UsrName);
			// return Json(data);
		}
	}
}
