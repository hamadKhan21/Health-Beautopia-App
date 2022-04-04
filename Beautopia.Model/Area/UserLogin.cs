using System;
using System.Collections.Generic;
using System.Text;

namespace Beautopia.Model.Area
{
	public class UserLogin
	{
		public UserLogin()
		{
			Menus = new List<UserMenu>();
		}
		public int ID { get; set; }
		public string FullName { get; set; }
		public string UserName { get; set; }
		public string RoleID { get; set; }
		public string EmployeeNumber { get; set; }
		public string EmailID { get; set; }
		public string Password { get; set; }
		public bool IsAdhock { get; set; }
		
		public bool IsActive { get; set; }
		public string IsActiveChecked { get; set; }
		public string CompanyNameEn { get; set; }
		public string CompanyID { get; set; }
		public string Supervisor_Number { get; set; }
		public string SupervisorEmail { get; set; }
		public string Director_Number { get; set; }
		public string DirectorEmail { get; set; }
		public string CreatedBy { get; set; }
		public List<UserMenu> Menus { get; set; }
	}

	public class UserMenu
	{
		public int MenuID { get; set; }
		public int SubMenuId { get; set; }
		public int RoleID { get; set; }
		public string RoleNameAr { get; set; }
		public string RoleNameEn { get; set; }
		public string MenuNameEn { get; set; }
		public string MenuNameAr { get; set; }
		public string MenuUrl { get; set; }
		public string customIcon { get; set; }
		public int Orders { get; set; }

	}
}
