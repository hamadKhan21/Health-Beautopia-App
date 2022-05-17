using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beautopia.Model.Area
{
	public class Slider
	{
		public int ID { get; set; }
		
		public IFormFile SliderImageFile { get; set; }
		public string SliderImage { get; set; }

		public IFormFile SliderImageFileAr { get; set; }
		public string SliderImageAr { get; set; }

		public string title1 { get; set; }
		public string title2 { get; set; }
		public string title3 { get; set; }
		public string title1Ar { get; set; }
		public string title2Ar { get; set; }
		public string title3Ar { get; set; }
		public string CreatedBy { get; set; }
		public string IsActiveChecked { get; set; }
		public bool IsActive { get; set; }
	}
}
