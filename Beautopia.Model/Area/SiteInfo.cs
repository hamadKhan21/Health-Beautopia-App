using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beautopia.Model.Area
{
	public class SiteInfo
	{
		public int ID { get; set; }
		public string Address { get; set; }
		public string Contact { get; set; }
		public string Email { get; set; }
		public string Facebook { get; set; }
		public string Twitter { get; set; }
		public string Instagram { get; set; }
		public string GooglePlus { get; set; }
		public string SnapChat { get; set; }
		public string TikTok { get; set; }
		public string CreatedBy { get; set; }

		public string LogoImage { get; set; }
		public IFormFile LogoImageFile { get; set; }

	}
}
