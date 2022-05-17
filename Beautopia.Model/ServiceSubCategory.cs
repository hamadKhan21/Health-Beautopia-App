using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;


namespace Beautopia.Model
{
	public class ServiceSubCategory
	{
		public int ID { get; set; }
		public string SubCategoryNameEn { get; set; }
		public string SubCategoryNameAr { get; set; }
		public string ServicePrice { get; set; }
		public int CategoryID { get; set; }
		public string IsActiveChecked { get; set; }
		public bool IsActive { get; set; }
		public IFormFile SubServiceImage { get; set; }
		public string SubServiceImageName { get; set; }

		public IFormFile SubServiceImageAr { get; set; }
		public string SubServiceImageNameAr { get; set; }
		public string Description { get; set; }
		public string DescriptionAr { get; set; }
		public int SorOrder { get; set; }

		public string CategoryNameEn { get; set; }
		public string CategoryNameAr { get; set; }
	}
}
