using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beautopia.Model.Area
{
	public class Doctor
	{
		public int ID { get; set; }

		public string DoctorImage { get; set; }
		public IFormFile DoctorImageFile { get; set; }


		public string DoctorImageAr { get; set; }
		public IFormFile DoctorImageFileAr { get; set; }

		public string DoctorName { get; set; }
		public string DoctorNameAr { get; set; }
		public string Designation { get; set; }
		public string DesignationAr { get; set; }
		public string Description { get; set; }
		public string DescriptionAr { get; set; }
		public string CreatedBy { get; set; }
		public string IsActiveChecked { get; set; }
		
		public bool IsActive { get; set; }
		public int? DoctorsCategoryID { get; set; }

		public string DoctorsCategoryEn { get; set; }
		public string DoctorsCategoryAr { get; set; }

	}
}
