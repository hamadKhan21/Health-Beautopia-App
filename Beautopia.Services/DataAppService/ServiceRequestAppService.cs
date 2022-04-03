using Beautopia.Model;
using Beautopia.Model.Area;
using Beautopia.Services.IDataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Beautopia.Services.DataAppService
{
	public class ServiceRequestAppService : IServiceRequest
	{
		public static string SQLconnectionString = ConnectionStrings.GetConnectionString("BeautopiaConnectionString");


        public int InsertServiceRequest(RequestService param)
        {
            int ReturnID = -1;
            //IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            dbConnection.Open();
            SqlTransaction trn = dbConnection.BeginTransaction();
            try
            {
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.Transaction = trn;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Sp_InsertRequestService";
                dbCommand.Parameters.Add("@Name ", SqlDbType.NVarChar, 250).Value = param.Name;
                dbCommand.Parameters.Add("@Mobile", SqlDbType.NVarChar, 250).Value = param.Mobile;
                dbCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 4000).Value = param.Email;
                dbCommand.Parameters.Add("@RequestedService", SqlDbType.NVarChar, 1000).Value = param.Service;
                dbCommand.Parameters.Add("@Comments", SqlDbType.NVarChar, 1000).Value = param.Comments;
                dbCommand.Parameters.Add("@Source", SqlDbType.NVarChar, 250).Value = param.Source;
                dbCommand.Parameters.Add("@SType", SqlDbType.NVarChar, 250).Value = param.SType;
                dbCommand.Parameters.Add("@ReturnID", SqlDbType.VarChar, 500).Direction = ParameterDirection.ReturnValue;
               // dbCommand.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 250).Value = Obj.CreatedBy;

                //dbCommand.ExecuteNonQuery();



                dbCommand.ExecuteNonQuery();
                ReturnID = (int)dbCommand.Parameters["@ReturnID"].Value;
                //ReturnEmpID = (int)dbCommand.ExecuteScalar();
                trn.Commit();

                //ReturnEmpID = (int)dbCommand.Parameters["@Result"].Value;
            }
            catch (SqlException exception)
            {
                // _trace.App_Trace(exception.Message, "Error", "Sp_InsertOrUpdateXXDAAREmployeesFromOracle()");
                trn.Rollback();
               // return ReturnEmpID;
            }
            finally
            {

                dbConnection.Close();

            }
            return ReturnID;
            //
        }
        public List<RequestService> GetRequestServiceIfActivity(string CreatedBy, string RoleID)
        {
            List<RequestService> Lisobj = new List<RequestService>();

            string ReturnEmpID = "";
            IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            //SqlTransaction trn = dbConnection.BeginTransaction();
            dbConnection.Open();
            try
            {


                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                //dbCommand.Transaction = trn;
                //dbCommand.CommandType = CommandType.;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Sp_GetRequestServiceIfActivity";
                dbCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = CreatedBy;
                dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar, 250).Value = RoleID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    RequestService obj = new RequestService();

                    //
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.RequestServiceID = Convert.ToString(reader["ID"]);
                    obj.Name = Convert.ToString(reader["Name"]);
                    obj.Mobile = Convert.ToString(reader["Mobile"]);
                    obj.Mobile = Convert.ToString(reader["Mobile"]);
                    obj.Email = Convert.ToString(reader["Email"]);
                    obj.Service = Convert.ToString(reader["RequestedService"]);
                    obj.Comments = Convert.ToString(reader["Comments"]);
                    obj.Source = Convert.ToString(reader["Source"]);
                    obj.SType = Convert.ToString(reader["SType"]);
                    obj.CreatedOn = Convert.ToString(reader["CreatedOn"]);
                    var services = GetSubCategoryByServiceID(obj.ID);

                    foreach (var item in services)
                    {

                        obj._listOfServices += (obj._listOfServices == null || obj._listOfServices == "" ? item.SubCategoryNameAr : "," + item.SubCategoryNameAr);
                    }

                    obj.SubCategoryByServiceID = services;
                    Lisobj.Add(obj);
                }
            }
            catch (Exception exception)
            {

                return Lisobj;
            }
            finally
            {
                dbConnection.Close();
            }

            return Lisobj;
        }
        public List<RequestService> GetRequestService(string Mobile)
        {
            List<RequestService> Lisobj = new List<RequestService>();

            string ReturnEmpID = "";
            IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            //SqlTransaction trn = dbConnection.BeginTransaction();
            dbConnection.Open();
            try
            {


                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                //dbCommand.Transaction = trn;
                //dbCommand.CommandType = CommandType.;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Sp_GetRequestService";
                dbCommand.Parameters.Add("@Mobile", SqlDbType.NVarChar, 250).Value = Mobile;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    RequestService obj = new RequestService();

//
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.RequestServiceID = Convert.ToString(reader["ID"]);
                    obj.Name = Convert.ToString(reader["Name"]);
                    obj.Mobile = Convert.ToString(reader["Mobile"]);
                    obj.Mobile = Convert.ToString(reader["Mobile"]);
                    obj.Email = Convert.ToString(reader["Email"]);
                    obj.Service = Convert.ToString(reader["RequestedService"]);
                    obj.Comments = Convert.ToString(reader["Comments"]);
                    obj.Source = Convert.ToString(reader["Source"]);
                    obj.SType = Convert.ToString(reader["SType"]);
                    obj.CreatedOn = Convert.ToString(reader["CreatedOn"]);
                    var services = GetSubCategoryByServiceID(obj.ID);

                    foreach (var item in services) {

                        obj._listOfServices += (obj._listOfServices == null || obj._listOfServices=="" ? item.SubCategoryNameAr : "," + item.SubCategoryNameAr);
                    }

                    obj.SubCategoryByServiceID = services;
                    Lisobj.Add(obj);
                }
            }
            catch (Exception exception)
            {

                return Lisobj;
            }
            finally
            {
                dbConnection.Close();
            }

            return Lisobj;
        }

        
        public List<SubCategoryByServiceID> GetSubCategoryByServiceID(int RequestServiceID)
        {
            List<SubCategoryByServiceID> Lisobj = new List<SubCategoryByServiceID>();

            string ReturnEmpID = "";
            IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            //SqlTransaction trn = dbConnection.BeginTransaction();
            dbConnection.Open();
            try
            {


                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                //dbCommand.Transaction = trn;
                //dbCommand.CommandType = CommandType.;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Sp_GetSubCategoryByServiceID";
                dbCommand.Parameters.Add("@RequestServiceID", SqlDbType.NVarChar, 250).Value = RequestServiceID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    SubCategoryByServiceID obj = new SubCategoryByServiceID();

                    //
                    // obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.ServicePrice = Convert.ToString(reader["ServicePrice"]);
                    obj.SubCategoryNameAr = Convert.ToString(reader["SubCategoryNameAr"]);
                    obj.SubCategoryNameEn = Convert.ToString(reader["SubCategoryNameEn"]);
                    obj.RequestServiceID = Convert.ToString(reader["RequestServiceID"]);
               

                    Lisobj.Add(obj);
                }
            }
            catch (Exception exception)
            {

                return Lisobj;
            }
            finally
            {
                dbConnection.Close();
            }

            return Lisobj;
        }


        public void InsertRequestServiceAndSubCategoryMapping(RequestServiceAndSubCategoryMapping param)
        {
            int ReturnEmpID = -1;
            //IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            dbConnection.Open();
            SqlTransaction trn = dbConnection.BeginTransaction();
            try
            {
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.Transaction = trn;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "SP_InsertServiceAndCategoryMapping";
                dbCommand.Parameters.Add("@ServiceSubCategoryID ", SqlDbType.Int).Value = param.ServiceSubCategoryID;
                dbCommand.Parameters.Add("@RequestServiceID", SqlDbType.Int).Value = param.RequestServiceID;

                dbCommand.ExecuteNonQuery();
                //ReturnEmpID = (int)dbCommand.Parameters["@ReturnID"].Value;
                //ReturnEmpID = (int)dbCommand.ExecuteScalar();
                trn.Commit();

                //ReturnEmpID = (int)dbCommand.Parameters["@Result"].Value;
            }
            catch (SqlException exception)
            {
                // _trace.App_Trace(exception.Message, "Error", "Sp_InsertOrUpdateXXDAAREmployeesFromOracle()");
                trn.Rollback();
                // return ReturnEmpID;
            }
            finally
            {

                dbConnection.Close();

            }
            // return ReturnEmpID;
            //
        }

        public List<ServiceCategory> GetServiceCategory()
        {
            List<ServiceCategory> Lisobj = new List<ServiceCategory>();

            string ReturnEmpID = "";
            IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            //SqlTransaction trn = dbConnection.BeginTransaction();
            dbConnection.Open();
            try
            {


                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                //dbCommand.Transaction = trn;
                //dbCommand.CommandType = CommandType.;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Sp_GetServiceCategory";
               // dbCommand.Parameters.Add("@Mobile", SqlDbType.NVarChar, 250).Value = Mobile;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    ServiceCategory obj = new ServiceCategory();

                    //
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.CategoryNameEn = Convert.ToString(reader["CategoryNameEn"]);
                    obj.CategoryNameAr = Convert.ToString(reader["CategoryNameAr"]);
                 

                    Lisobj.Add(obj);
                }
            }
            catch (Exception exception)
            {

                return Lisobj;
            }
            finally
            {
                dbConnection.Close();
            }

            return Lisobj;
        }

        public List<ServiceCategory> GetAllServiceCategory()
        {
            List<ServiceCategory> Lisobj = new List<ServiceCategory>();

            string ReturnEmpID = "";
            IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            //SqlTransaction trn = dbConnection.BeginTransaction();
            dbConnection.Open();
            try
            {


                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                //dbCommand.Transaction = trn;
                //dbCommand.CommandType = CommandType.;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Sp_GetAllServiceCategory";
                // dbCommand.Parameters.Add("@Mobile", SqlDbType.NVarChar, 250).Value = Mobile;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    ServiceCategory obj = new ServiceCategory();

                    //
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.CategoryNameEn = Convert.ToString(reader["CategoryNameEn"]);
                    obj.CategoryNameAr = Convert.ToString(reader["CategoryNameAr"]);
                    obj.IsActive = Convert.ToBoolean(reader["IsActive"]);


                    Lisobj.Add(obj);
                }
            }
            catch (Exception exception)
            {

                return Lisobj;
            }
            finally
            {
                dbConnection.Close();
            }

            return Lisobj;
        }
        public List<ServiceSubCategory> GetServiceSubCategory(int CategoryID)
        {
            List<ServiceSubCategory> Lisobj = new List<ServiceSubCategory>();

            string ReturnEmpID = "";
            IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            //SqlTransaction trn = dbConnection.BeginTransaction();
            dbConnection.Open();
            try
            {


                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                //dbCommand.Transaction = trn;
                //dbCommand.CommandType = CommandType.;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Sp_GetServiceSubCategory";
                 dbCommand.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    ServiceSubCategory obj = new ServiceSubCategory();

                    //
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.SubCategoryNameEn = Convert.ToString(reader["SubCategoryNameEn"]);
                    obj.SubCategoryNameAr = Convert.ToString(reader["SubCategoryNameAr"]);
                    obj.ServicePrice = Convert.ToString(reader["ServicePrice"]);
                    obj.CategoryID = Convert.ToInt32(reader["CategoryID"]);
                    obj.SubServiceImageName = Convert.ToString(reader["SubServiceImageName"]);


                    Lisobj.Add(obj);
                }
            }
            catch (Exception exception)
            {

                return Lisobj;
            }
            finally
            {
                dbConnection.Close();
            }

            return Lisobj;
        }

        public UserLogin UserLogin(string UserName, string Password)
        {
            UserLogin getLogin = new UserLogin();
            var IsloginEsists = false;
            string ReturnEmpID = "";
            IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            //SqlTransaction trn = dbConnection.BeginTransaction();
            dbConnection.Open();
            try
            {


                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                //dbCommand.Transaction = trn;
                //dbCommand.CommandType = CommandType.;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Sp_Login";
                dbCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 250).Value = UserName;
                dbCommand.Parameters.Add("@Password", SqlDbType.NVarChar, 250).Value = Password;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    getLogin.FullName = Convert.ToString(reader["FullName"]);
                    getLogin.UserName = Convert.ToString(reader["UserName"]);
                    getLogin.RoleID = Convert.ToString(reader["RoleID"]);
                    getLogin.EmployeeNumber = Convert.ToString(reader["EmployeeNumber"]);
                    getLogin.EmailID = Convert.ToString(reader["EmailID"]);
                   // getLogin.IsAdhock = Convert.ToBoolean(reader["IsAdhock"]);
                   // getLogin.CompanyNameEn = Convert.ToString(reader["CompanyNameEn"]);
                   // getLogin.CompanyID = Convert.ToString(reader["CompanyID"]);
                   // getLogin.Supervisor_Number = Convert.ToString(reader["Supervisor_Number"]);
                    //getLogin.SupervisorEmail = Convert.ToString(reader["Supervisor_Email"]);
                   // getLogin.DirectorEmail = Convert.ToString(reader["Director_Email"]);
                    //getLogin.IsAdhock = Convert.ToBoolean(reader["IsAdhock"]);
                    IsloginEsists = true;
                }
                if (IsloginEsists)
                {

                    getLogin.Menus = GetMenusOnRole(getLogin.RoleID);
                }


            }
            catch (Exception exception)
            {

                return getLogin;
            }
            finally
            {
                dbConnection.Close();
            }

            return getLogin;
        }

        public List<UserMenu> GetMenusOnRole(string RoleID)
        {
            List<UserMenu> menus = new List<UserMenu>();

            string ReturnEmpID = "";
            IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            //SqlTransaction trn = dbConnection.BeginTransaction();
            dbConnection.Open();
            try
            {


                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                //dbCommand.Transaction = trn;
                //dbCommand.CommandType = CommandType.;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Sp_GetMenuOnRole";
                dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar, 250).Value = RoleID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    UserMenu obj = new UserMenu();



                    obj.MenuID = Convert.ToInt32(reader["MenuID"]);
                    obj.SubMenuId = Convert.ToInt32(reader["SubMenuId"]);
                    obj.RoleID = Convert.ToInt32(reader["RoleID"]);
                    obj.RoleNameAr = Convert.ToString(reader["RoleNameAr"]);
                    obj.RoleNameEn = Convert.ToString(reader["RoleNameEn"]);
                    obj.MenuNameAr = Convert.ToString(reader["MenuNameAr"]);
                    obj.MenuNameEn = Convert.ToString(reader["MenuNameEn"]);
                    obj.MenuUrl = Convert.ToString(reader["MenuUrl"]);
                    obj.customIcon = Convert.ToString(reader["customIcon"]);
                    obj.Orders = Convert.ToInt32(reader["Orders"]);

                    menus.Add(obj);
                }
            }
            catch (Exception exception)
            {

                return menus;
            }
            finally
            {
                dbConnection.Close();
            }

            return menus;
        }

        public List<Source> GetSources()
        {
            List<Source> Lisobj = new List<Source>();

            string ReturnEmpID = "";
            IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            //SqlTransaction trn = dbConnection.BeginTransaction();
            dbConnection.Open();
            try
            {


                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                //dbCommand.Transaction = trn;
                //dbCommand.CommandType = CommandType.;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Sp_GetSources";
                //dbCommand.Parameters.Add("@Mobile", SqlDbType.NVarChar, 250).Value = Mobile;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    Source obj = new Source();

                    //
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.SourceNameEn = Convert.ToString(reader["SourceNameEn"]);
                    obj.SourceNameAr = Convert.ToString(reader["SourceNameAr"]);
                    
                    Lisobj.Add(obj);
                }
            }
            catch (Exception exception)
            {

                return Lisobj;
            }
            finally
            {
                dbConnection.Close();
            }

            return Lisobj;
        }


        public void InsertUpdateServiceCategory(ServiceCategory param,string CreatedBy)
        {
            int ReturnEmpID = -1;
            //IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            dbConnection.Open();
            SqlTransaction trn = dbConnection.BeginTransaction();
            try
            {
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.Transaction = trn;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "SP_InsertUpdateServiceCategory";
                dbCommand.Parameters.Add("@ID ", SqlDbType.Int).Value = param.ID;
                dbCommand.Parameters.Add("@CategoryNameEn", SqlDbType.NVarChar,500).Value = param.CategoryNameEn;
                dbCommand.Parameters.Add("@CategoryNameAr", SqlDbType.NVarChar,500).Value = param.CategoryNameAr;
                dbCommand.Parameters.Add("@IsActive", SqlDbType.Bit).Value = param.IsActive;
                dbCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 500).Value = CreatedBy;

                dbCommand.ExecuteNonQuery();
                //ReturnEmpID = (int)dbCommand.Parameters["@ReturnID"].Value;
                //ReturnEmpID = (int)dbCommand.ExecuteScalar();
                trn.Commit();

                //ReturnEmpID = (int)dbCommand.Parameters["@Result"].Value;
            }
            catch (SqlException exception)
            {
                // _trace.App_Trace(exception.Message, "Error", "Sp_InsertOrUpdateXXDAAREmployeesFromOracle()");
                trn.Rollback();
                // return ReturnEmpID;
            }
            finally
            {

                dbConnection.Close();

            }
            // return ReturnEmpID;
            //
        }


        public void InsertUpdateServiceSubCategory(ServiceSubCategory param, string CreatedBy)
        {
            int ReturnEmpID = -1;
            //IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            dbConnection.Open();
            SqlTransaction trn = dbConnection.BeginTransaction();
            try
            {
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.Transaction = trn;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "SP_InsertUpdateServiceSubCategory";
                dbCommand.Parameters.Add("@ID ", SqlDbType.Int).Value = param.ID;
                dbCommand.Parameters.Add("@SubCategoryNameEn", SqlDbType.NVarChar, 500).Value = param.SubCategoryNameEn;
                dbCommand.Parameters.Add("@SubCategoryNameAr", SqlDbType.NVarChar, 500).Value = param.SubCategoryNameAr;
                dbCommand.Parameters.Add("@ServicePrice", SqlDbType.NVarChar, 500).Value = param.ServicePrice;
                dbCommand.Parameters.Add("@CategoryID", SqlDbType.Int).Value = param.CategoryID;
                dbCommand.Parameters.Add("@IsActive", SqlDbType.Bit).Value = param.IsActive;
                dbCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 500).Value = CreatedBy;
                dbCommand.Parameters.Add("@SubServiceImageName", SqlDbType.NVarChar, 1000).Value = param.SubServiceImageName;

                dbCommand.ExecuteNonQuery();
                //ReturnEmpID = (int)dbCommand.Parameters["@ReturnID"].Value;
                //ReturnEmpID = (int)dbCommand.ExecuteScalar();
                trn.Commit();

                //ReturnEmpID = (int)dbCommand.Parameters["@Result"].Value;
            }
            catch (SqlException exception)
            {
                // _trace.App_Trace(exception.Message, "Error", "Sp_InsertOrUpdateXXDAAREmployeesFromOracle()");
                trn.Rollback();
                // return ReturnEmpID;
            }
            finally
            {

                dbConnection.Close();

            }
            // return ReturnEmpID;
            //
        }

        public List<ServiceSubCategory> GetAllServiceSubCategory()
        {
            List<ServiceSubCategory> Lisobj = new List<ServiceSubCategory>();

            string ReturnEmpID = "";
            IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            //SqlTransaction trn = dbConnection.BeginTransaction();
            dbConnection.Open();
            try
            {


                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                //dbCommand.Transaction = trn;
                //dbCommand.CommandType = CommandType.;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Sp_GetAllServiceSubCategory";
                // dbCommand.Parameters.Add("@Mobile", SqlDbType.NVarChar, 250).Value = Mobile;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    ServiceSubCategory obj = new ServiceSubCategory();

                    //
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.SubCategoryNameAr = Convert.ToString(reader["SubCategoryNameAr"]);
                    obj.SubCategoryNameEn = Convert.ToString(reader["SubCategoryNameEn"]);
                    obj.ServicePrice = Convert.ToString(reader["ServicePrice"]);
                    obj.CategoryID = Convert.ToInt32(reader["CategoryID"]);
                    obj.IsActive = Convert.ToBoolean(reader["IsActive"]);
                    obj.SubServiceImageName = Convert.ToString(reader["SubServiceImageName"]);


                    Lisobj.Add(obj);
                }
            }
            catch (Exception exception)
            {

                return Lisobj;
            }
            finally
            {
                dbConnection.Close();
            }

            return Lisobj;
        }

        public List<ActivityType> GetActivityType()
        {
            List<ActivityType> Lisobj = new List<ActivityType>();

            string ReturnEmpID = "";
            IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            //SqlTransaction trn = dbConnection.BeginTransaction();
            dbConnection.Open();
            try
            {


                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                //dbCommand.Transaction = trn;
                //dbCommand.CommandType = CommandType.;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Sp_GetActivityType";
               // dbCommand.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    ActivityType obj = new ActivityType();

                    //
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.ActivityTypeAr = Convert.ToString(reader["ActivityTypeAr"]);
                    obj.ActivityTypeEn = Convert.ToString(reader["ActivityTypeEn"]);
                    


                    Lisobj.Add(obj);
                }
            }
            catch (Exception exception)
            {

                return Lisobj;
            }
            finally
            {
                dbConnection.Close();
            }

            return Lisobj;
        }


        public List<RS_Activity> GetRS_Activity(int RequestServiceID)
        {
            List<RS_Activity> Lisobj = new List<RS_Activity>();

            string ReturnEmpID = "";
            IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            //SqlTransaction trn = dbConnection.BeginTransaction();
            dbConnection.Open();
            try
            {


                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                //dbCommand.Transaction = trn;
                //dbCommand.CommandType = CommandType.;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Sp_GetRS_Activity";
                 dbCommand.Parameters.Add("@RequestServiceID", SqlDbType.Int).Value = RequestServiceID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    RS_Activity obj = new RS_Activity();

                    //
                    //obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.ActivityTypeAr = Convert.ToString(reader["ActivityTypeAr"]);
                    obj.ActivityTypeEn = Convert.ToString(reader["ActivityTypeEn"]);
                    obj.RequestServiceID = Convert.ToInt32(reader["RequestServiceID"]);
                    obj.Comments = Convert.ToString(reader["Comments"]);
                    obj.CreatedBy = Convert.ToString(reader["CreatedBy"]);
                    obj.CreatedOn = Convert.ToString(reader["CreatedOn"]);



                    Lisobj.Add(obj);
                }
            }
            catch (Exception exception)
            {

                return Lisobj;
            }
            finally
            {
                dbConnection.Close();
            }

            return Lisobj;
        }


        public void  InsertRS_Activity(RS_Activity param)
        {
            int ReturnEmpID = -1;
            //IDataReader reader = null;
            SqlConnection dbConnection = null;
            dbConnection = new SqlConnection(SQLconnectionString);
            dbConnection.Open();
            SqlTransaction trn = dbConnection.BeginTransaction();
            try
            {
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.Transaction = trn;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Sp_InsertRS_Activity";
               // dbCommand.Parameters.Add("@ID ", SqlDbType.Int).Value = param.ID;
                dbCommand.Parameters.Add("@ActivityTypeID", SqlDbType.Int).Value = param.ActivityTypeID;
                dbCommand.Parameters.Add("@RequestServiceID", SqlDbType.Int).Value = param.RequestServiceID;
                dbCommand.Parameters.Add("@Comments", SqlDbType.NVarChar, 500).Value = param.Comments;
                dbCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 500).Value = param.CreatedBy;
               
                dbCommand.ExecuteNonQuery();
                //ReturnEmpID = (int)dbCommand.Parameters["@ReturnID"].Value;
                //ReturnEmpID = (int)dbCommand.ExecuteScalar();
                trn.Commit();

                //ReturnEmpID = (int)dbCommand.Parameters["@Result"].Value;
            }
            catch (SqlException exception)
            {
                // _trace.App_Trace(exception.Message, "Error", "Sp_InsertOrUpdateXXDAAREmployeesFromOracle()");
                trn.Rollback();
                // return ReturnEmpID;
            }
            finally
            {

                dbConnection.Close();

            }
            // return ReturnEmpID;
            //
        }
    }
}
