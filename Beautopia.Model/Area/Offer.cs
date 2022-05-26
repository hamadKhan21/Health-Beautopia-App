using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beautopia.Model.Area
{
	public class Offer
	{
		public int ID { get; set; }
		public string OfferImage { get; set; }
		public IFormFile OfferImageFile { get; set; }

		public string OfferImageAr { get; set; }
		public IFormFile OfferImageFileAr { get; set; }

		public string Title { get; set; }
		public string TitleAr { get; set; }
		public string Description { get; set; }
		public string DescriptionAr { get; set; }
		public string CreatedBy { get; set; }
		public string IsActiveChecked { get; set; }
		public string DepartmentID { get; set; }
		public string DepartmentNameEn { get; set; }
		public string DepartmentNameAr { get; set; }
		public bool IsActive { get; set; }
	}
}
