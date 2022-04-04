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
	public class UsersController : Controller
	{
		private static IUsers __Users;
		public UsersController(IUsers Users)
		{
			__Users = Users;

		}
		[Route("ManageUsersRole")]
		public IActionResult UsersRole()
		{
			return View();
		}

		[Route("ManageUsers")]
		public IActionResult Users()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult SaveUpdateUsers([Bind("ID,FullName,UserName,Password,EmailID,RoleID,IsActiveChecked")] UserLogin obj)
		{
			var login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			obj.CreatedBy = login.UserName;
			obj.IsActive = (obj.IsActiveChecked == null ? false : true);

			__Users.InsertUpdateUsers(obj);
			var GetUsers = __Users.GetUsers();

			//foreach (var item in listOfServices)
			//{
			//	RequestServiceAndSubCategoryMapping map = new RequestServiceAndSubCategoryMapping();
			//	map.RequestServiceID = returnServoceID;
			//	map.ServiceSubCategoryID = Convert.ToInt32(item);

			//	_serviceRequest.InsertRequestServiceAndSubCategoryMapping(map);
			//}

			return Json(GetUsers);
		}
		public JsonResult GetAllUsers()
		{
			var data = __Users.GetUsers();


			return Json(data);
		}
	}
}
