using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Beautopia.Model
{
	public class RequestService
	{
		public RequestService()
		{
			SubCategoryByServiceID = new List<SubCategoryByServiceID>();
		}
		
		public int ID { get; set; }
		public string RequestServiceID { get; set; }
		//[Display(Name = "Release Date")]
		//[Display(Name = "Release Date")]
		//[Required]
		public string Service { get; set; }
		//[Required(ErrorMessage = "Name is Required")]
		public string Name { get; set; }
		//[Required]
		public string Mobile { get; set; }

		public string Email { get; set; }
		public string Comments { get; set; }
		public string Source { get; set; }
		public string SType { get; set; }
		public string CreatedOn { get; set; }
		public string _listOfServices { get; set; }
		public List<SubCategoryByServiceID> SubCategoryByServiceID { get; set; }
	}
}
