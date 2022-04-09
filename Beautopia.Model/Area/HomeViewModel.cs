using System;
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

		}

		public List<Slider>Sliders { get; set; }
		public List<Offer> Offers { get; set; }
		public List<Doctor> Doctors { get; set; }


	}
}
