using Beautopia.Model;
using Beautopia.Model.Area;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beautopia.Services.IDataService
{
	public interface IServiceRequest
	{

		int InsertServiceRequest(RequestService param);
		List<RequestService> GetRequestServiceIfActivity(string CreatedBy, string RoleID);
		List<RequestService> GetRequestService(string Mobile);
		
		List<SubCategoryByServiceID> GetSubCategoryByServiceID(int RequestServiceID);

		void InsertRequestServiceAndSubCategoryMapping(RequestServiceAndSubCategoryMapping param);
		List<ServiceCategory> GetServiceCategory();
		List<ServiceCategory> GetSA_ServiceCategory();
		List<ServiceSubCategory> GetServiceSubCategory(int CategoryID);
		List<ServiceSubCategory> GetSA_ServiceSubCategory(int CategoryID);
		UserLogin UserLogin(string UserName, string Password);

		List<Source> GetSources();

		void InsertUpdateServiceCategory(ServiceCategory param,string CreatedBy);
		void InsertUpdateSA_ServiceCategory(ServiceCategory param,string CreatedBy);

		List<ServiceCategory> GetAllServiceCategory();
		List<ServiceCategory> GetAllSA_ServiceCategory();



		void InsertUpdateServiceSubCategory(ServiceSubCategory param, string CreatedBy);
		void InsertUpdateSA_ServiceSubCategory(ServiceSubCategory param, string CreatedBy);

		void RemoveEntityRecord(string Entity, string ID);

		List<ServiceSubCategory> GetAllServiceSubCategory();
		List<ServiceSubCategory> GetAllSA_ServiceSubCategory();

		List<ActivityType> GetActivityType();
		List<RS_Activity> GetRS_Activity(int RequestServiceID);
		void InsertRS_Activity(RS_Activity param);


		//Doctors Category
		List<DoctorsCategory> GetDoctorsCategory();

		void InsertUpdateDoctorsCategory(DoctorsCategory param);
	}
}
