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
		
	}
}
