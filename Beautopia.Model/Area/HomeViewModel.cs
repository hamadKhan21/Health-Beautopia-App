﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Beautopia.Model.Area
{
	public class HomeViewModel
	{
		public HomeViewModel()
		{
			Sliders = new List<Slider>();
			Offers = new List<Offer>();
			Doctors = new List<Doctor>();
			ServiceSubCategorys = new List<ServiceSubCategory>();
			Equipments = new List<Equipment>();
			SiteInfo = new SiteInfo();
			SA_ServiceSubCategorys = new List<ServiceSubCategory>();
			SmileGillary = new List<SmileGillary>();
		}

		public List<Slider>Sliders { get; set; }
		public List<Offer> Offers { get; set; }
		public List<Doctor> Doctors { get; set; }
		public List<ServiceSubCategory> ServiceSubCategorys { get; set; }
		public List<Equipment> Equipments { get; set; }
		public List<ServiceSubCategory> SA_ServiceSubCategorys { get; set; }
		public List<SmileGillary> SmileGillary { get; set; }

		public SiteInfo SiteInfo { get; set; }

	}
}
