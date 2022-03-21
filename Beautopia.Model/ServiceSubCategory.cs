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
		public int SorOrder { get; set; }
	}
}
