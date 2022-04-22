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
using System.IO;
using Microsoft.Extensions.Hosting;

namespace Beautopia.web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[JSONAuth]
	public class SettingsController : Controller
	{

		private static IServiceRequest _serviceRequest;
		private static UserLogin login = new UserLogin();
		private readonly IHostEnvironment _hostEnvironment;

		//private static ServiceRequestAppService serviceRequest=new ServiceRequestAppService();
		public SettingsController(IServiceRequest serviceRequest, IHostEnvironment hostEnvironment)
		{
			 //login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			_serviceRequest = serviceRequest;
			_hostEnvironment = hostEnvironment;

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
		public async Task<JsonResult> SaveUpdateSubServices([Bind("ID,SubCategoryNameEn,SubCategoryNameAr" +
			",Description,DescriptionAr,IsActiveChecked,ServicePrice,CategoryID,SubServiceImage,SubServiceImageName")] ServiceSubCategory obj)
		{
			var login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			List<ServiceSubCategory> servicecate = new List<ServiceSubCategory>();

			obj.IsActive = (obj.IsActiveChecked == null ? false : true);
			try
			{
				// Code to upload image if not null
				if (obj.SubServiceImage != null)
				{
					var extention = Path.GetExtension(obj.SubServiceImage.FileName);
					if (extention == ".jpg" || extention == ".jpeg" || extention == ".png")
					{
						// Create a File Info 
						FileInfo fi = new FileInfo(obj.SubServiceImage.FileName);

						// This code creates a unique file name to prevent duplications 
						// stored at the file location
						var newFilename = login.UserName + "_" + String.Format("{0:d}",
										  (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
						var webPath = _hostEnvironment.ContentRootPath;
						var path = Path.Combine("", webPath + @"\wwwroot\admin-custom\Images\SubServices\" + newFilename);

						// IMPORTANT: The pathToSave variable will be save on the column in the database
						//var pathToSave = @"/images/" + newFilename;
						obj.SubServiceImageName = newFilename;
						//info.IncomingCreatedBy = login.EmployeeNumber;
						// This stream the physical file to the allocate wwwroot/ImageFiles folder
						using (var stream = new FileStream(path, FileMode.Create))
						{
							await obj.SubServiceImage.CopyToAsync(stream);
						}
						//_dispatchService.InsertUpdateOutdoing(info);
						//message = "Success";
					}
					else
					{
						//message = "File is not in correct format, only png,jpeg, pdf or jpg is allowed";
					}
					// This save the path to the record

				}



				_serviceRequest.InsertUpdateServiceSubCategory(obj, login.UserName);
				 servicecate = _serviceRequest.GetAllServiceSubCategory();
			}

			catch (Exception ex) { 
			
			}
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

		[SessionTimeout]
		[Route("Admin/ManageDoctorsCategory")]
		public IActionResult ManageDoctorsCategory()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult SaveUpdateDoctorsCategory([Bind("ID,DoctorsCategoryEn,DoctorsCategoryAr,IsActiveChecked")] DoctorsCategory obj)
		{
			var login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			obj.CreatedBy = login.UserName;
			obj.IsActive = (obj.IsActiveChecked == null ? false : true);

			_serviceRequest.InsertUpdateDoctorsCategory(obj);
			var servicecate = _serviceRequest.GetDoctorsCategory();

			//foreach (var item in listOfServices)
			//{
			//	RequestServiceAndSubCategoryMapping map = new RequestServiceAndSubCategoryMapping();
			//	map.RequestServiceID = returnServoceID;
			//	map.ServiceSubCategoryID = Convert.ToInt32(item);

			//	_serviceRequest.InsertRequestServiceAndSubCategoryMapping(map);
			//}

			return Json(servicecate);
		}

		public JsonResult GetAllDoctorsCategory()
		{
			var data = _serviceRequest.GetDoctorsCategory();


			return Json(data);
		}

	}
}
