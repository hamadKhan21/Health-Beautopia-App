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
	//[JSONAuth]
	public class UserController : Controller
	{
		private static IUsers _users;
		private static UserLogin login = new UserLogin();
		public UserController(IUsers users)
		{
			//login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			_users = users;
			//_hostEnvironment = hostEnvironment;

		}

		[Route("Admin/ManageUsers")]
		public IActionResult Users()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult SaveUpdateUsers([Bind("ID,FullName,UserName,Password,EmailID,RoleID,IsActiveChecked")] UserLogin obj)
		{
			var login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");

			obj.IsActive = (obj.IsActiveChecked == null ? false : true);

			_users.InsertUpdateUsers(obj);
			var servicecate = _users.GetUsers();

			//foreach (var item in listOfServices)
			//{
			//	RequestServiceAndSubCategoryMapping map = new RequestServiceAndSubCategoryMapping();
			//	map.RequestServiceID = returnServoceID;
			//	map.ServiceSubCategoryID = Convert.ToInt32(item);

			//	_serviceRequest.InsertRequestServiceAndSubCategoryMapping(map);
			//}

			return Json(servicecate);
		}

		public JsonResult GetAllUsers()
		{
			var data = _users.GetUsers();


			return Json(data);
		}

		public JsonResult GetAllRoles()
		{
			var data = _users.GetUserRoles();


			return Json(data);
		}
		[Route("Admin/ManagerUsersRole")]
		public IActionResult UsersRole()
		{
			return View();
		}


		[Route("Admin/ManageUsersRoleAccess")]
		public IActionResult UsersRoleAccess()
		{
			return View();
		}
	}
}
