using Beautopia.Model.Area;
using Beautopia.Services.IDataService;
using Beautopia.web.Areas.Admin.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Beautopia.web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AppInfoController : Controller
	{
		private static IUsers _users;
		private static UserLogin login = new UserLogin();
		private readonly IHostEnvironment _hostEnvironment;
		public AppInfoController(IUsers users, IHostEnvironment hostEnvironment)
		{
			//login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			_users = users;
			//_hostEnvironment = hostEnvironment;
			_hostEnvironment = hostEnvironment;

		}
		[Route("Admin/ManageSliders")]
		public IActionResult ManageSliders()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<JsonResult> SaveUpdateSlider ([Bind("ID,SliderImage,title1,title2,title3,SliderImageFile,SliderImage,IsActiveChecked")] Slider obj)
		{
			var login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			List<Slider> Sliders = new List<Slider>();
			obj.IsActive = (obj.IsActiveChecked == null ? false : true);
			obj.CreatedBy = login.UserName;
			try
			{
				// Code to upload image if not null
				if (obj.SliderImageFile != null)
				{
					var extention = Path.GetExtension(obj.SliderImageFile.FileName);
					if (extention == ".jpg" || extention == ".jpeg" || extention == ".png")
					{
						// Create a File Info 
						FileInfo fi = new FileInfo(obj.SliderImageFile.FileName);

						// This code creates a unique file name to prevent duplications 
						// stored at the file location
						var newFilename = "Slider" + "_" + String.Format("{0:d}",
										  (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
						var webPath = _hostEnvironment.ContentRootPath;
						var path = Path.Combine("", webPath + @"\wwwroot\medlab\images\content\slider\" + newFilename);

						// IMPORTANT: The pathToSave variable will be save on the column in the database
						//var pathToSave = @"/images/" + newFilename;
						obj.SliderImage = newFilename;
						//info.IncomingCreatedBy = login.EmployeeNumber;
						// This stream the physical file to the allocate wwwroot/ImageFiles folder
						using (var stream = new FileStream(path, FileMode.Create))
						{
							await obj.SliderImageFile.CopyToAsync(stream);
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



				_users.InsertUpdateSliders(obj);
				Sliders = _users.GetSliders();
			}

			catch (Exception ex)
			{

			}



			return Json(Sliders);
		}

		public JsonResult GetAllSliders()
		{
			var data = _users.GetSliders();


			return Json(data);
		}


		[Route("Admin/ManageOffers")]
		public IActionResult ManageOffers()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<JsonResult> SaveUpdateOffer([Bind("ID,Title,OfferImage,OfferImageFile,IsActiveChecked")] Offer obj)
		{
			var login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			List<Offer> Offers = new List<Offer>();
			obj.IsActive = (obj.IsActiveChecked == null ? false : true);
			obj.CreatedBy = login.UserName;
			try
			{
				// Code to upload image if not null
				if (obj.OfferImageFile != null)
				{
					var extention = Path.GetExtension(obj.OfferImageFile.FileName);
					if (extention == ".jpg" || extention == ".jpeg" || extention == ".png")
					{
						// Create a File Info 
						FileInfo fi = new FileInfo(obj.OfferImageFile.FileName);

						// This code creates a unique file name to prevent duplications 
						// stored at the file location
						var newFilename = "Offer" + "_" + String.Format("{0:d}",
										  (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
						var webPath = _hostEnvironment.ContentRootPath;
						var path = Path.Combine("", webPath + @"\wwwroot\medlab\images\content\Offers\" + newFilename);

						// IMPORTANT: The pathToSave variable will be save on the column in the database
						//var pathToSave = @"/images/" + newFilename;
						obj.OfferImage = newFilename;
						//info.IncomingCreatedBy = login.EmployeeNumber;
						// This stream the physical file to the allocate wwwroot/ImageFiles folder
						using (var stream = new FileStream(path, FileMode.Create))
						{
							await obj.OfferImageFile.CopyToAsync(stream);
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



				_users.InsertUpdateOffer(obj);
				Offers = _users.GetOffers();
			}

			catch (Exception ex)
			{

			}



			return Json(Offers);
		}

		public JsonResult GetOffers()
		{
			var data = _users.GetOffers();


			return Json(data);
		}


		[Route("Admin/ManageDoctors")]
		public IActionResult ManageDoctors()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<JsonResult> SaveUpdateDoctor([Bind("ID,DoctorImage,DoctorImageFile,DoctorName,Designation,Description,IsActiveChecked")] Doctor obj)
		{
			var login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			List<Doctor> listObj = new List<Doctor>();
			obj.IsActive = (obj.IsActiveChecked == null ? false : true);
			obj.CreatedBy = login.UserName;
			try
			{
				// Code to upload image if not null
				if (obj.DoctorImageFile != null)
				{
					var extention = Path.GetExtension(obj.DoctorImageFile.FileName);
					if (extention == ".jpg" || extention == ".jpeg" || extention == ".png")
					{
						// Create a File Info 
						FileInfo fi = new FileInfo(obj.DoctorImageFile.FileName);

						// This code creates a unique file name to prevent duplications 
						// stored at the file location
						var newFilename = "Doctor" + "_" + String.Format("{0:d}",
										  (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
						var webPath = _hostEnvironment.ContentRootPath;
						var path = Path.Combine("", webPath + @"\wwwroot\medlab\images\content\doctors\" + newFilename);

						// IMPORTANT: The pathToSave variable will be save on the column in the database
						//var pathToSave = @"/images/" + newFilename;
						obj.DoctorImage = newFilename;
						//info.IncomingCreatedBy = login.EmployeeNumber;
						// This stream the physical file to the allocate wwwroot/ImageFiles folder
						using (var stream = new FileStream(path, FileMode.Create))
						{
							await obj.DoctorImageFile.CopyToAsync(stream);
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



				_users.InsertUpdateDoctor(obj);
				listObj = _users.GetDoctor();
			}

			catch (Exception ex)
			{

			}



			return Json(listObj);
		}

		public JsonResult GetAllDoctors()
		{
			var data = _users.GetDoctor();


			return Json(data);
		}


		[Route("Admin/ManageSmileGillary")]
		public IActionResult ManageSmileGillary()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<JsonResult> SaveUpdateSmileGillary([Bind("ID,Title,SmileImage,SmileImageFile,IsActiveChecked")] SmileGillary obj)
		{
			var login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			List<SmileGillary> Offers = new List<SmileGillary>();
			obj.IsActive = (obj.IsActiveChecked == null ? false : true);
			obj.CreatedBy = login.UserName;
			try
			{
				// Code to upload image if not null
				if (obj.SmileImageFile != null)
				{
					var extention = Path.GetExtension(obj.SmileImageFile.FileName);
					if (extention == ".jpg" || extention == ".jpeg" || extention == ".png")
					{
						// Create a File Info 
						FileInfo fi = new FileInfo(obj.SmileImageFile.FileName);

						// This code creates a unique file name to prevent duplications 
						// stored at the file location
						var newFilename = "SmileGillary" + "_" + String.Format("{0:d}",
										  (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
						var webPath = _hostEnvironment.ContentRootPath;
						var path = Path.Combine("", webPath + @"\wwwroot\medlab\images\content\gallery\" + newFilename);

						// IMPORTANT: The pathToSave variable will be save on the column in the database
						//var pathToSave = @"/images/" + newFilename;
						obj.SmileImage = newFilename;
						//info.IncomingCreatedBy = login.EmployeeNumber;
						// This stream the physical file to the allocate wwwroot/ImageFiles folder
						using (var stream = new FileStream(path, FileMode.Create))
						{
							await obj.SmileImageFile.CopyToAsync(stream);
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



				_users.InsertUpdateSmileGillary(obj);
				Offers = _users.GetSmileGillary();
			}

			catch (Exception ex)
			{

			}



			return Json(Offers);
		}

		public JsonResult GetSmileGillary()
		{
			var data = _users.GetSmileGillary();


			return Json(data);
		}




		[Route("Admin/ManageEquipments")]
		public IActionResult ManageEquipments()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<JsonResult> SaveUpdateEquipments([Bind("ID,Title,Description,EquipmentIcon,EquipmentImage,EquipmentImageFile,IsActiveChecked")] Equipment obj)
		{
			var login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			List<Equipment> Offers = new List<Equipment>();
			obj.IsActive = (obj.IsActiveChecked == null ? false : true);
			obj.CreatedBy = login.UserName;
			try
			{
				// Code to upload image if not null
				if (obj.EquipmentImageFile != null)
				{
					var extention = Path.GetExtension(obj.EquipmentImageFile.FileName);
					if (extention == ".jpg" || extention == ".jpeg" || extention == ".png")
					{
						// Create a File Info 
						FileInfo fi = new FileInfo(obj.EquipmentImageFile.FileName);

						// This code creates a unique file name to prevent duplications 
						// stored at the file location
						var newFilename = "Equipment" + "_" + String.Format("{0:d}",
										  (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
						var webPath = _hostEnvironment.ContentRootPath;
						var path = Path.Combine("", webPath + @"\wwwroot\medlab\images\content\devices\" + newFilename);

						// IMPORTANT: The pathToSave variable will be save on the column in the database
						//var pathToSave = @"/images/" + newFilename;
						obj.EquipmentImage = newFilename;
						//info.IncomingCreatedBy = login.EmployeeNumber;
						// This stream the physical file to the allocate wwwroot/ImageFiles folder
						using (var stream = new FileStream(path, FileMode.Create))
						{
							await obj.EquipmentImageFile.CopyToAsync(stream);
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



				_users.InsertUpdateEquipment(obj);
				Offers = _users.GetEquipment();
			}

			catch (Exception ex)
			{

			}



			return Json(Offers);
		}

		public JsonResult GetEquipments()
		{
			var data = _users.GetEquipment();


			return Json(data);
		}




		[Route("Admin/ManageAboutUs")]
		public IActionResult ManageAboutUs()
		{
			return View();
		}

		[HttpPost]
		//[ValidateAntiForgeryToken]
		public JsonResult SaveUpdateAboutUs([Bind("ID,AboutUSText")] AboutUs obj)
		{
			var login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			AboutUs aboutus = new AboutUs();
			//obj.IsActive = (obj.IsActiveChecked == null ? false : true);
			int ReturnID = 0;
			obj.CreatedBy = login.UserName;
			try
			{




				ReturnID= _users.InsertUpdateAboutUs(obj);
				aboutus = _users.GetAboutUs();
			}

			catch (Exception ex)
			{

			}



			return Json(aboutus);
		}

		public JsonResult GetAboutUs()
		{
			var data = _users.GetAboutUs();


			return Json(data);
		}


		[Route("Admin/ManageSiteInfo")]
		public IActionResult ManageSiteInfo()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<JsonResult> SaveUpdateSiteInfo([Bind("ID,Address,Contact,Email,Facebook,Twitter,Instagram,GooglePlus,SnapChat,TikTok,LogoImage,LogoImageFile")] SiteInfo obj)
		{
			var login = HttpContext.Session.GetObjectFromJson<UserLogin>("Login");
			SiteInfo objec = new SiteInfo();
			int ReturnID = 0;
			obj.CreatedBy = login.UserName;
			try
			{
				if (obj.LogoImageFile != null)
				{
					var extention = Path.GetExtension(obj.LogoImageFile.FileName);
					if (extention == ".jpg" || extention == ".jpeg" || extention == ".png" || extention == ".JPG" || extention == ".JPEG" || extention == ".PNG")
					{
						// Create a File Info 
						FileInfo fi = new FileInfo(obj.LogoImageFile.FileName);

						// This code creates a unique file name to prevent duplications 
						// stored at the file location
						var newFilename = "logo" + "_" + String.Format("{0:d}",
										  (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
						var webPath = _hostEnvironment.ContentRootPath;
						var path = Path.Combine("", webPath + @"\wwwroot\medlab\images\content\logo\" + newFilename);

						// IMPORTANT: The pathToSave variable will be save on the column in the database
						//var pathToSave = @"/images/" + newFilename;
						obj.LogoImage = newFilename;
						//info.IncomingCreatedBy = login.EmployeeNumber;
						// This stream the physical file to the allocate wwwroot/ImageFiles folder
						using (var stream = new FileStream(path, FileMode.Create))
						{
							await obj.LogoImageFile.CopyToAsync(stream);
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



				ReturnID = _users.InsertUpdateSiteInfo(obj);
				objec = _users.GetSiteInfo();
			}

			catch (Exception ex)
			{

			}



			return Json(objec);
		}

		public JsonResult GetSiteInfo()
		{
			var data = _users.GetSiteInfo();


			return Json(data);
		}
	}
}
