using System;
using System.Collections.Generic;
using System.Text;

namespace Beautopia.Model.Area
{
	public class UserRole
	{
		public UserRole() {
			RoleMenuMappings = new List<RoleMenuMapping>();


		}
		public int ID { get; set; }
		public string	RoleNameEn { get; set; }
		public string RoleNameAr { get; set; }
		public bool IsActive { get; set; }
		public int MunueID { get; set; }
		public string CreatedBy { get; set; }
		public string IsActiveChecked { get; set; }
		public string _listOfMenus { get; set; }
		public List<RoleMenuMapping> RoleMenuMappings { get; set; }
	}


	public class RoleMenuMapping {


		public int RoleID { get; set; }
		public int MenuID { get; set; }
		public string RoleNameEn { get; set; }
		public string MenuNameAr { get; set; }
	}
}
