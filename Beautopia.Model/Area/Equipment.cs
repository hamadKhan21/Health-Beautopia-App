using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beautopia.Model.Area
{
	public class Equipment
	{
		public int ID { get; set; }
		public string Title { get; set; }
		public string TitleAr { get; set; }
		public string Description { get; set; }
		public string DescriptionAr { get; set; }
		public string EquipmentIcon { get; set; }
		public string EquipmentImage { get; set; }

		public IFormFile EquipmentImageFile { get; set; }
		public bool IsActive { get; set; }
		public string IsActiveChecked { get; set; }
		public string CreatedBy { get; set; }
	}
}
