using System;
using System.Collections.Generic;
using System.Text;

namespace Beautopia.Model.Area
{
	public class DoctorsCategory
	{
		public int ID { get; set; }
		public string DoctorsCategoryEn { get; set; }
		public string DoctorsCategoryAr { get; set; }
		public string CreatedBy { get; set; }
		public string IsActiveChecked { get; set; }
		public bool IsActive { get; set; }
	}
}
