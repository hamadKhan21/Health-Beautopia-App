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
    }
}
