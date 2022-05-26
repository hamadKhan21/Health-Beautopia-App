using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beautopia.Model.Area
{
	public class SmileGillary
	{
		public int ID { get; set; }
		public string Title { get; set; }
		public string TitleAr { get; set; }
		public string DepartmentID { get; set; }
		public string DepartmentNameEn { get; set; }
		public string DepartmentNameAr { get; set; }
		public string SmileImage { get; set; }
		
		public IFormFile SmileImageFile { get; set; }

		public string SmileImageAr { get; set; }

		public IFormFile SmileImageFileAr { get; set; }
		public bool IsActive { get; set; }
		public string IsActiveChecked { get; set; }
		public string CreatedBy { get; set; }
	}
}
