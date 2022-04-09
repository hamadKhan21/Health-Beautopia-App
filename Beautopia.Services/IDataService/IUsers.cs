using Beautopia.Model;
using Beautopia.Model.Area;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beautopia.Services.IDataService
{
	public interface IUsers
	{

		int InsertUpdateUserRole(UserRole param);
		int InsertRoleMenu(RoleMenuMapping param);
		List<UserRole> GetUserRoles();
		List<RoleMenuMapping> GetRoleMenuMapping(int RoleID);

		List<UserLogin> GetUsers();
		

		int InsertUpdateUsers(UserLogin param);

		List<UserMenu> GetAllMenus();

		List<UserMenu> GetSubMenusByMenuID(int MenuID);


		//Slider
		List<Slider> GetSliders();

		int InsertUpdateSliders(Slider param);


		//Offers
		List<Offer> GetOffers();

		int InsertUpdateOffer(Offer param);


		//Doctor
		List<Doctor> GetDoctor();

		int InsertUpdateDoctor(Doctor param);

	}
}
