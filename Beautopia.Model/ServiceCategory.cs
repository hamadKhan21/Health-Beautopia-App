using System;
using System.Collections.Generic;
using System.Text;

namespace Beautopia.Model
{
	public class ServiceCategory
	{
		public int ID { get; set; }
		public string CategoryNameEn { get; set; }
		public string CategoryNameAr { get; set; }
		public string IsActiveChecked { get; set; }
		public bool IsActive { get; set; }
		public int SortOrder { get; set; }
	}
}
