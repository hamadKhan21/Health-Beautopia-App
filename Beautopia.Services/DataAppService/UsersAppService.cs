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
	public class UsersAppService : IUsers
    {
		public static string SQLconnectionString = ConnectionStrings.GetConnectionString("BeautopiaConnectionString");


        public int InsertUpdateUserRole(UserRole param)
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
                dbCommand.CommandText = "SP_InsertUpdateUserRole";
                dbCommand.Parameters.Add("@ID ", SqlDbType.Int).Value = param.ID;
                dbCommand.Parameters.Add("@RoleNameAr", SqlDbType.NVarChar, 500).Value = param.RoleNameAr;
                dbCommand.Parameters.Add("@RoleNameEn", SqlDbType.NVarChar, 500).Value = param.RoleNameEn;
                dbCommand.Parameters.Add("@IsActive", SqlDbType.Bit).Value = param.IsActive;
                dbCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 500).Value = param.CreatedBy;
              
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
        public int InsertRoleMenu(RoleMenuMapping param)
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
                dbCommand.CommandText = "SP_InsertRoleMenuMapping";
                dbCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = param.RoleID;
                dbCommand.Parameters.Add("@MenuID", SqlDbType.Int).Value = param.MenuID;
                

                //dbCommand.ExecuteNonQuery();



                dbCommand.ExecuteNonQuery();
               // ReturnID = (int)dbCommand.Parameters["@ReturnID"].Value;
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
        public List<UserRole> GetUserRoles()
        {
            List<UserRole> Lisobj = new List<UserRole>();

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
                dbCommand.CommandText = "SP_GetUserRoles";
               // dbCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = CreatedBy;
               // dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar, 250).Value = RoleID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    UserRole obj = new UserRole();

                    //
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.RoleNameEn = Convert.ToString(reader["RoleNameEn"]);
                    obj.RoleNameAr = Convert.ToString(reader["RoleNameAr"]);
                    obj.IsActive = Convert.ToBoolean(reader["IsActive"]);

                    obj.RoleMenuMappings = GetRoleMenuMapping(obj.ID);

                    
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
        public List<RoleMenuMapping> GetRoleMenuMapping(int RoleID)
        {
            List<RoleMenuMapping> Lisobj = new List<RoleMenuMapping>();

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
                dbCommand.CommandText = "SP_GetRoleMenuMapping";
                dbCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleID;
               // dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar, 250).Value = RoleID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    RoleMenuMapping obj = new RoleMenuMapping();

                    //
                   // obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.RoleID = Convert.ToInt32(reader["RoleID"]);
                    obj.MenuID = Convert.ToInt32(reader["MenuID"]);
                    obj.RoleNameEn = Convert.ToString(reader["RoleNameEn"]);
                    obj.MenuNameAr = Convert.ToString(reader["MenuNameAr"]);
                   

                    
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

        public List<UserLogin> GetUsers()
        {
            List<UserLogin> Lisobj = new List<UserLogin>();

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
                dbCommand.CommandText = "Sp_GetUsers";
              //  dbCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleID;
                // dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar, 250).Value = RoleID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    UserLogin obj = new UserLogin();

                    //
                    // obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.FullName = Convert.ToString(reader["FullName"]);
                    obj.UserName = Convert.ToString(reader["UserName"]);
                    obj.Password = Convert.ToString(reader["Password"]);
                    obj.RoleID = Convert.ToString(reader["RoleID"]);
                    obj.EmailID = Convert.ToString(reader["EmailID"]);
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


        public int InsertUpdateUsers(UserLogin param)
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
                dbCommand.CommandText = "SP_InsertUpdateUsers";
                dbCommand.Parameters.Add("@ID", SqlDbType.Int).Value = param.ID;
                dbCommand.Parameters.Add("@FullName", SqlDbType.NVarChar,250).Value = param.FullName;
                dbCommand.Parameters.Add("@UserName", SqlDbType.NVarChar,250).Value = param.UserName;
                dbCommand.Parameters.Add("@Password", SqlDbType.NVarChar,250).Value = param.Password;
                dbCommand.Parameters.Add("@EmailID", SqlDbType.NVarChar,250).Value = param.EmailID;
                dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar,250).Value = param.RoleID;
                dbCommand.Parameters.Add("@IsActive", SqlDbType.Bit).Value = param.IsActive;
                dbCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = param.CreatedBy;


                //dbCommand.ExecuteNonQuery();



                dbCommand.ExecuteNonQuery();
                // ReturnID = (int)dbCommand.Parameters["@ReturnID"].Value;
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

        public List<UserMenu> GetAllMenus()
        {
            List<UserMenu> Lisobj = new List<UserMenu>();

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
                dbCommand.CommandText = "Sp_GetAllMenus";
                //  dbCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleID;
                // dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar, 250).Value = RoleID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    UserMenu obj = new UserMenu();

                    //
                    // obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.MenuID = Convert.ToInt32(reader["MenuID"]);
                    obj.SubMenuId = Convert.ToInt32(reader["SubMenuId"]);
                    obj.MenuNameAr = Convert.ToString(reader["MenuNameAr"]);
                    obj.MenuNameEn = Convert.ToString(reader["MenuNameEn"]);
                    obj.SubMenus = GetSubMenusByMenuID(obj.MenuID);


                    if (obj.SubMenus.Count > 0) { 
                    Lisobj.Add(obj);
                    }
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


        public List<UserMenu> GetSubMenusByMenuID(int MenuID)
        {
            List<UserMenu> Lisobj = new List<UserMenu>();

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
                dbCommand.CommandText = "Sp_GetSubMenusByMenuID";
                  dbCommand.Parameters.Add("@MenuID", SqlDbType.Int).Value = MenuID;
                // dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar, 250).Value = RoleID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    UserMenu obj = new UserMenu();

                    //
                    // obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.MenuID = Convert.ToInt32(reader["MenuID"]);
                    obj.SubMenuId = Convert.ToInt32(reader["SubMenuId"]);
                    obj.MenuNameAr = Convert.ToString(reader["MenuNameAr"]);
                    obj.MenuNameEn = Convert.ToString(reader["MenuNameEn"]);




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





        public List<Slider> GetSliders()
        {
            List<Slider> Lisobj = new List<Slider>();

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
                dbCommand.CommandText = "SP_GetSliders";
                //  dbCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleID;
                // dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar, 250).Value = RoleID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    Slider obj = new Slider();

                    //
                    // obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.SliderImage = Convert.ToString(reader["SliderImage"]);
                    obj.SliderImageAr = Convert.ToString(reader["SliderImageAr"]);
                    obj.title1 = Convert.ToString(reader["title1"]);
                    obj.title2 = Convert.ToString(reader["title2"]);
                    obj.title3 = Convert.ToString(reader["title3"]);

                    obj.title1Ar = Convert.ToString(reader["title1Ar"]);
                    obj.title2Ar = Convert.ToString(reader["title2Ar"]);
                    obj.title3Ar = Convert.ToString(reader["title3Ar"]);

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


        public int InsertUpdateSliders(Slider param)
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
                dbCommand.CommandText = "SP_InsertUpdateSliders";
                dbCommand.Parameters.Add("@ID", SqlDbType.Int).Value = param.ID;
                dbCommand.Parameters.Add("@SliderImage", SqlDbType.NVarChar, 400000).Value = param.SliderImage;
                dbCommand.Parameters.Add("@SliderImageAr", SqlDbType.NVarChar, 400000).Value = param.SliderImageAr;
                dbCommand.Parameters.Add("@title1", SqlDbType.NVarChar, 250).Value = param.title1;
                dbCommand.Parameters.Add("@title2", SqlDbType.NVarChar, 250).Value = param.title2;
                dbCommand.Parameters.Add("@title3", SqlDbType.NVarChar, 250).Value = param.title3;

                dbCommand.Parameters.Add("@title1Ar", SqlDbType.NVarChar, 250).Value = param.title1Ar;
                dbCommand.Parameters.Add("@title2Ar", SqlDbType.NVarChar, 250).Value = param.title2Ar;
                dbCommand.Parameters.Add("@title3Ar", SqlDbType.NVarChar, 250).Value = param.title3Ar;

                dbCommand.Parameters.Add("@IsActive", SqlDbType.Bit).Value = param.IsActive;
                dbCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = param.CreatedBy;


                //dbCommand.ExecuteNonQuery();



                dbCommand.ExecuteNonQuery();
                // ReturnID = (int)dbCommand.Parameters["@ReturnID"].Value;
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


        public List<Offer> GetOffers()
        {
            List<Offer> Lisobj = new List<Offer>();

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
                dbCommand.CommandText = "SP_GetAllOffers";
                //  dbCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleID;
                // dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar, 250).Value = RoleID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    Offer obj = new Offer();

                    //
                    // obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.OfferImage = Convert.ToString(reader["OfferImage"]);
                    obj.OfferImageAr = Convert.ToString(reader["OfferImageAr"]);
                    obj.Title = Convert.ToString(reader["Title"]);
                    obj.TitleAr = Convert.ToString(reader["TitleAr"]);
                    obj.Description = Convert.ToString(reader["Description"]);
                    obj.DescriptionAr = Convert.ToString(reader["DescriptionAr"]);
                   

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


        public int InsertUpdateOffer(Offer param)
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
                dbCommand.CommandText = "SP_InsertUpdateOffers";
                dbCommand.Parameters.Add("@ID", SqlDbType.Int).Value = param.ID;
                dbCommand.Parameters.Add("@OfferImage", SqlDbType.NVarChar, 400000).Value = param.OfferImage;
                dbCommand.Parameters.Add("@OfferImageAr", SqlDbType.NVarChar, 400000).Value = param.OfferImageAr;
                dbCommand.Parameters.Add("@Title", SqlDbType.NVarChar, 250).Value = param.Title;
                dbCommand.Parameters.Add("@TitleAr", SqlDbType.NVarChar, 250).Value = param.TitleAr;
                dbCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 4000).Value = param.Description;
                dbCommand.Parameters.Add("@DescriptionAr", SqlDbType.NVarChar, 4000).Value = param.DescriptionAr;
             

                dbCommand.Parameters.Add("@IsActive", SqlDbType.Bit).Value = param.IsActive;
                dbCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = param.CreatedBy;


                //dbCommand.ExecuteNonQuery();



                dbCommand.ExecuteNonQuery();
                // ReturnID = (int)dbCommand.Parameters["@ReturnID"].Value;
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



        public List<Doctor> GetDoctor()
        {
            List<Doctor> Lisobj = new List<Doctor>();

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
                dbCommand.CommandText = "SP_GetDoctors";
                //  dbCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleID;
                // dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar, 250).Value = RoleID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    Doctor obj = new Doctor();

                    //
                    // obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.DoctorImage = Convert.ToString(reader["DoctorImage"]);
                    obj.DoctorImageAr = Convert.ToString(reader["DoctorImageAr"]);
                    obj.DoctorName = Convert.ToString(reader["DoctorName"]);
                    obj.DoctorNameAr = Convert.ToString(reader["DoctorNameAr"]);
                    obj.Designation = Convert.ToString(reader["Designation"]);
                    obj.DesignationAr = Convert.ToString(reader["DesignationAr"]);
                    obj.Description = Convert.ToString(reader["Description"]);
                    obj.DescriptionAr = Convert.ToString(reader["DescriptionAr"]);
                    obj.DoctorsCategoryID = Convert.ToInt32(reader["DoctorsCategoryID"]);
                    obj.DoctorsCategoryEn = Convert.ToString(reader["DoctorsCategoryEn"]);
                    obj.DoctorsCategoryAr = Convert.ToString(reader["DoctorsCategoryAr"]);
                    obj.InstagramLink = Convert.ToString(reader["InstagramLink"]);
                    

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


        public int InsertUpdateDoctor(Doctor param)
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
                dbCommand.CommandText = "SP_InsertUpdateDoctor";
                dbCommand.Parameters.Add("@ID", SqlDbType.Int).Value = param.ID;
                dbCommand.Parameters.Add("@DoctorImage", SqlDbType.NVarChar, 400000).Value = param.DoctorImage;
                dbCommand.Parameters.Add("@DoctorImageAr", SqlDbType.NVarChar, 400000).Value = param.DoctorImageAr;
                dbCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 4000).Value = param.Description;
                dbCommand.Parameters.Add("@Designation", SqlDbType.NVarChar, 250).Value = param.Designation;
                dbCommand.Parameters.Add("@DoctorName", SqlDbType.NVarChar, 250).Value = param.DoctorName;

                dbCommand.Parameters.Add("@DescriptionAr", SqlDbType.NVarChar, 4000).Value = param.DescriptionAr;
                dbCommand.Parameters.Add("@DesignationAr", SqlDbType.NVarChar, 250).Value = param.DesignationAr;
                dbCommand.Parameters.Add("@DoctorNameAr", SqlDbType.NVarChar, 250).Value = param.DoctorNameAr;
                dbCommand.Parameters.Add("@InstagramLink", SqlDbType.NVarChar, 250).Value = param.InstagramLink;
                
                dbCommand.Parameters.Add("@IsActive", SqlDbType.Bit).Value = param.IsActive;
                dbCommand.Parameters.Add("@DoctorsCategoryID", SqlDbType.Int).Value = param.DoctorsCategoryID;
                dbCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = param.CreatedBy;


                //dbCommand.ExecuteNonQuery();



                dbCommand.ExecuteNonQuery();
                // ReturnID = (int)dbCommand.Parameters["@ReturnID"].Value;
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

        public List<SmileGillary> GetSmileGillary()
        {
            List<SmileGillary> Lisobj = new List<SmileGillary>();

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
                dbCommand.CommandText = "SP_GetAllSmileGillary";
                //  dbCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleID;
                // dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar, 250).Value = RoleID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    SmileGillary obj = new SmileGillary();

                    //
                    // obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.SmileImage = Convert.ToString(reader["SmileImage"]);
                    obj.SmileImageAr = Convert.ToString(reader["SmileImageAr"]);
                    obj.Title = Convert.ToString(reader["Title"]);


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


        public int InsertUpdateSmileGillary(SmileGillary param)
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
                dbCommand.CommandText = "SP_InsertUpdateSmileGillary";
                dbCommand.Parameters.Add("@ID", SqlDbType.Int).Value = param.ID;
                dbCommand.Parameters.Add("@SmileImage", SqlDbType.NVarChar, 400000).Value = param.SmileImage;
                dbCommand.Parameters.Add("@SmileImageAr", SqlDbType.NVarChar, 400000).Value = param.SmileImageAr;
                dbCommand.Parameters.Add("@Title", SqlDbType.NVarChar, 250).Value = param.Title;


                dbCommand.Parameters.Add("@IsActive", SqlDbType.Bit).Value = param.IsActive;
                dbCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = param.CreatedBy;


                //dbCommand.ExecuteNonQuery();



                dbCommand.ExecuteNonQuery();
                // ReturnID = (int)dbCommand.Parameters["@ReturnID"].Value;
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



        public List<Equipment> GetEquipment()
        {
            List<Equipment> Lisobj = new List<Equipment>();

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
                dbCommand.CommandText = "SP_GetEquipment";
                //  dbCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleID;
                // dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar, 250).Value = RoleID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    Equipment obj = new Equipment();

                    //
                    // obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.Title = Convert.ToString(reader["Title"]);
                    obj.TitleAr = Convert.ToString(reader["TitleAr"]);
                    obj.DescriptionAr = Convert.ToString(reader["DescriptionAr"]);
                    obj.Description = Convert.ToString(reader["Description"]);
                    obj.EquipmentImage = Convert.ToString(reader["EquipmentImage"]);
                    obj.EquipmentImageAr = Convert.ToString(reader["EquipmentImageAr"]);
                    obj.EquipmentIcon = Convert.ToString(reader["EquipmentIcon"]);


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


        public int InsertUpdateEquipment(Equipment param)
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
                dbCommand.CommandText = "SP_InsertUpdateEquipment";
                dbCommand.Parameters.Add("@ID", SqlDbType.Int).Value = param.ID;
                dbCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 400000).Value = param.Description;
                dbCommand.Parameters.Add("@DescriptionAr", SqlDbType.NVarChar, 400000).Value = param.DescriptionAr;
                dbCommand.Parameters.Add("@EquipmentIcon", SqlDbType.NVarChar, 250).Value = param.EquipmentIcon;
                dbCommand.Parameters.Add("@Title", SqlDbType.NVarChar, 250).Value = param.Title;
                dbCommand.Parameters.Add("@TitleAr", SqlDbType.NVarChar, 250).Value = param.TitleAr;
                dbCommand.Parameters.Add("@EquipmentImage", SqlDbType.NVarChar, 400000).Value = param.EquipmentImage;
                dbCommand.Parameters.Add("@EquipmentImageAr", SqlDbType.NVarChar, 400000).Value = param.EquipmentImageAr;


                dbCommand.Parameters.Add("@IsActive", SqlDbType.Bit).Value = param.IsActive;
                dbCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = param.CreatedBy;


                //dbCommand.ExecuteNonQuery();



                dbCommand.ExecuteNonQuery();
                // ReturnID = (int)dbCommand.Parameters["@ReturnID"].Value;
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



        public AboutUs GetAboutUs()
        {
            AboutUs obj = new AboutUs();

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
                dbCommand.CommandText = "SP_GetAboutUs";
                 // dbCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                // dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar, 250).Value = RoleID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                   // Equipment obj = new Equipment();

                    //
                    // obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.AboutUSText = Convert.ToString(reader["AboutUSText"]);
                    obj.AboutUSTextAr = Convert.ToString(reader["AboutUSTextAr"]);
                    obj.MessageFromCEOAr = Convert.ToString(reader["MessageFromCEOAr"]);
                    obj.MessageFromCEOEn = Convert.ToString(reader["MessageFromCEOEn"]);
              

                    //obj.IsActive = Convert.ToBoolean(reader["IsActive"]);



                   // Lisobj.Add(obj);
                }
            }
            catch (Exception exception)
            {

                return obj;
            }
            finally
            {
                dbConnection.Close();
            }

            return obj;
        }


        public int InsertUpdateAboutUs(AboutUs param)
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
                dbCommand.CommandText = "SP_InsertUpdateAboutUs";
                dbCommand.Parameters.Add("@ID", SqlDbType.Int).Value = param.ID;
                dbCommand.Parameters.Add("@AboutUSText", SqlDbType.NVarChar, 400000).Value = param.AboutUSText;
                dbCommand.Parameters.Add("@AboutUSTextAr", SqlDbType.NVarChar, 400000).Value = param.AboutUSTextAr;
                dbCommand.Parameters.Add("@MessageFromCEOAr", SqlDbType.NVarChar, 400000).Value = param.MessageFromCEOAr;
                dbCommand.Parameters.Add("@MessageFromCEOEn", SqlDbType.NVarChar, 400000).Value = param.MessageFromCEOEn;
                dbCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = param.CreatedBy;

                dbCommand.Parameters.Add("@ReturnID", SqlDbType.VarChar, 500).Direction = ParameterDirection.ReturnValue;
                // dbCommand.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 250).Value = Obj.CreatedBy;

                //dbCommand.ExecuteNonQuery();



                dbCommand.ExecuteNonQuery();
                ReturnID = (int)dbCommand.Parameters["@ReturnID"].Value;
              
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



        public SiteInfo GetSiteInfo()
        {
            SiteInfo obj = new SiteInfo();

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
                dbCommand.CommandText = "SP_GetSiteInfo";
                // dbCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                // dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar, 250).Value = RoleID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    // Equipment obj = new Equipment();

                    //
                    // obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.Address = Convert.ToString(reader["Address"]);
                    obj.AddressAr = Convert.ToString(reader["AddressAr"]);
                    obj.Contact = Convert.ToString(reader["Contact"]);
                    obj.ContactAr = Convert.ToString(reader["ContactAr"]);
                    obj.CreatedBy = Convert.ToString(reader["CreatedBy"]);
                    obj.Email= Convert.ToString(reader["Email"]);
                    obj.Facebook = Convert.ToString(reader["Facebook"]);
                    obj.GooglePlus = Convert.ToString(reader["GooglePlus"]);
                    obj.Instagram = Convert.ToString(reader["Instagram"]);
                    obj.SnapChat = Convert.ToString(reader["SnapChat"]);
                    obj.TikTok = Convert.ToString(reader["TikTok"]);
                    obj.Twitter = Convert.ToString(reader["Twitter"]);
                    obj.LogoImage = Convert.ToString(reader["LogoImage"]);
                    obj.LogoImageAr = Convert.ToString(reader["LogoImageAr"]);
                    obj.IsArabicByDefault = Convert.ToBoolean(reader["IsArabicByDefault"]);
                    obj.ServicesTagEn = Convert.ToString(reader["ServicesTagEn"]);
                    obj.ServicesTagAr = Convert.ToString(reader["ServicesTagAr"]);
                    obj.DevicesTagEn = Convert.ToString(reader["DevicesTagEn"]);
                    obj.DevicesTagAr = Convert.ToString(reader["DevicesTagAr"]);
                    obj.DoctorTagEn = Convert.ToString(reader["DoctorTagEn"]);
                    obj.DoctorTagAr = Convert.ToString(reader["DoctorTagAr"]);
                    obj.OfferTagEn = Convert.ToString(reader["OfferTagEn"]);
                    obj.OfferTagAr = Convert.ToString(reader["OfferTagAr"]);
                    obj.GillaryTagEn = Convert.ToString(reader["GillaryTagEn"]);
                    obj.GillaryTagAr = Convert.ToString(reader["GillaryTagAr"]);
                    obj.GoogleMapLocation = Convert.ToString(reader["GoogleMapLocation"]);



                    //obj.IsActive = Convert.ToBoolean(reader["IsActive"]);



                    // Lisobj.Add(obj);
                }
            }
            catch (Exception exception)
            {

                return obj;
            }
            finally
            {
                dbConnection.Close();
            }

            return obj;
        }


        public int InsertUpdateSiteInfo(SiteInfo param)
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
                dbCommand.CommandText = "SP_InsertUpdateSIteInfo";
                dbCommand.Parameters.Add("@ID", SqlDbType.Int).Value = param.ID;
                dbCommand.Parameters.Add("@Address", SqlDbType.NVarChar, 500).Value = param.Address;
                dbCommand.Parameters.Add("@AddressAr", SqlDbType.NVarChar, 500).Value = param.AddressAr;
                dbCommand.Parameters.Add("@ContactAr", SqlDbType.NVarChar, 500).Value = param.ContactAr;
                dbCommand.Parameters.Add("@Contact", SqlDbType.NVarChar, 500).Value = param.Contact;
                dbCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 500).Value = param.CreatedBy;
                dbCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 500).Value = param.Email;
                dbCommand.Parameters.Add("@Facebook", SqlDbType.NVarChar, 500).Value = param.Facebook;
                dbCommand.Parameters.Add("@GooglePlus", SqlDbType.NVarChar, 500).Value = param.GooglePlus;
                dbCommand.Parameters.Add("@Instagram", SqlDbType.NVarChar, 500).Value = param.Instagram;
                dbCommand.Parameters.Add("@SnapChat", SqlDbType.NVarChar, 500).Value = param.SnapChat;
                dbCommand.Parameters.Add("@TikTok", SqlDbType.NVarChar, 500).Value = param.TikTok;
                dbCommand.Parameters.Add("@Twitter", SqlDbType.NVarChar, 500).Value = param.Twitter;
                dbCommand.Parameters.Add("@LogoImage", SqlDbType.NVarChar, 40000).Value = param.LogoImage;
                dbCommand.Parameters.Add("@LogoImageAr", SqlDbType.NVarChar, 40000).Value = param.LogoImageAr;
                dbCommand.Parameters.Add("@IsArabicByDefault", SqlDbType.Bit).Value = param.IsArabicByDefault;
                dbCommand.Parameters.Add("@ServicesTagEn", SqlDbType.NVarChar, 500).Value = param.ServicesTagEn;
                dbCommand.Parameters.Add("@ServicesTagAr", SqlDbType.NVarChar, 500).Value = param.ServicesTagAr;
                dbCommand.Parameters.Add("@DevicesTagEn", SqlDbType.NVarChar, 500).Value = param.DevicesTagEn;
                dbCommand.Parameters.Add("@DevicesTagAr", SqlDbType.NVarChar, 500).Value = param.DevicesTagAr;
                dbCommand.Parameters.Add("@DoctorTagEn", SqlDbType.NVarChar, 500).Value = param.DoctorTagEn;
                dbCommand.Parameters.Add("@DoctorTagAr", SqlDbType.NVarChar, 500).Value = param.DoctorTagAr;
                dbCommand.Parameters.Add("@OfferTagEn", SqlDbType.NVarChar, 500).Value = param.OfferTagEn;
                dbCommand.Parameters.Add("@OfferTagAr", SqlDbType.NVarChar, 500).Value = param.OfferTagAr;
                dbCommand.Parameters.Add("@GillaryTagEn", SqlDbType.NVarChar, 500).Value = param.GillaryTagEn;
                dbCommand.Parameters.Add("@GillaryTagAr", SqlDbType.NVarChar, 500).Value = param.GillaryTagAr;
                dbCommand.Parameters.Add("@GoogleMapLocation", SqlDbType.NVarChar, 500).Value = param.GoogleMapLocation;

                dbCommand.Parameters.Add("@ReturnID", SqlDbType.VarChar, 500).Direction = ParameterDirection.ReturnValue;
                // dbCommand.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 250).Value = Obj.CreatedBy;

                //dbCommand.ExecuteNonQuery();



                dbCommand.ExecuteNonQuery();
                ReturnID = (int)dbCommand.Parameters["@ReturnID"].Value;

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

        public List<ServicesReport> GetServicesReport()
        {
            List<ServicesReport> Lisobj = new List<ServicesReport>();

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
                dbCommand.CommandText = "SP_GetServicesCount";
                //  dbCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleID;
                // dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar, 250).Value = RoleID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    ServicesReport obj = new ServicesReport();

                    //
                    // obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.Name = Convert.ToString(reader["Name"]);
                    obj.NameCount = Convert.ToString(reader["NameCount"]);
                  





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

        public List<ServicesReport> GetServicesReportBySource()
        {
            List<ServicesReport> Lisobj = new List<ServicesReport>();

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
                dbCommand.CommandText = "SP_GetServicesCountBySocialMedia";
                //  dbCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleID;
                // dbCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar, 250).Value = RoleID;




                reader = dbCommand.ExecuteReader();
                //
                while (reader.Read())
                {
                    ServicesReport obj = new ServicesReport();

                    //
                    obj.Name = Convert.ToString(reader["Name"]);
                    obj.NameCount = Convert.ToString(reader["NameCount"]);



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

      
    }
}
