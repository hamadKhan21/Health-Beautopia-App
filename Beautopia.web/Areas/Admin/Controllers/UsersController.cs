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
	public class UsersController : Controller
	{
		private static IUsers __Users;
		public UsersController(IUsers Users)
		{
			__Users = Users;

		}
		[Route("ManageUsers")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
