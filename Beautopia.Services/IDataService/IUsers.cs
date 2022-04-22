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

		//SmileGillary
		List<SmileGillary> GetSmileGillary();

		int InsertUpdateSmileGillary(SmileGillary param);


		//Equipment
		List<Equipment> GetEquipment();

		int InsertUpdateEquipment(Equipment param);

		//AboutUs
		AboutUs GetAboutUs();

		int InsertUpdateAboutUs(AboutUs param);

		//SiteInfo
		SiteInfo GetSiteInfo();

		int InsertUpdateSiteInfo(SiteInfo param);

		//Report
		List<ServicesReport> GetServicesReport();
		List<ServicesReport> GetServicesReportBySource();


	

	}
}
