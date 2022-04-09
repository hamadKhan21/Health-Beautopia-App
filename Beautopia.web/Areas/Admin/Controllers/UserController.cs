using Beautopia.Model.Area;
using Beautopia.Services.IDataService;
using Beautopia.web.Areas.Admin.Extensions;
using Beautopia.web.Areas.Admin.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult SaveUpdateUsersRole([Bind("ID,RoleNameEn,RoleNameAr,IsActiveChecked,_listOfMenus")] UserRole obj)
		{
			var login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			obj.CreatedBy = login.UserName;
			obj.IsActive = (obj.IsActiveChecked == null ? false : true);

			var listOfServices = new List<string>();

			listOfServices = (obj._listOfMenus == null ? listOfServices : JsonConvert.DeserializeObject<List<string>>(obj._listOfMenus));


			int UserRoleID=_users.InsertUpdateUserRole(obj);


			foreach (var item in listOfServices)
			{
				var allMenus=_users.GetAllMenus();
				RoleMenuMapping map = new RoleMenuMapping();
				map.RoleID= UserRoleID;
				map.MenuID = Convert.ToInt32(item);
				_users.InsertRoleMenu(map);
				foreach (var checkParent in allMenus) {
					var getpMenu = checkParent.SubMenus.Where(a => a.MenuID == map.MenuID).SingleOrDefault();
					if (getpMenu != null) { 
					map.MenuID = getpMenu.SubMenuId;
					_users.InsertRoleMenu(map);
					}
				}
				//_serviceRequest.InsertRequestServiceAndSubCategoryMapping(map);
			}


			var servicecate = _users.GetUserRoles();



			return Json(servicecate);
		}

		public JsonResult GetUserRoles()
		{
			var data = _users.GetUserRoles();


			return Json(data);
		}

		public JsonResult GetAllMenus()
		{
			var data = _users.GetAllMenus();


			return Json(data);
		}

		[Route("Admin/ManageUsersRoleAccess")]
		public IActionResult UsersRoleAccess()
		{
			return View();
		}
	}
}
