using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLCafe.Interface
{
    public interface IUserRepository : ICRUD<AppUser>
    {
        List<UserRole> getUserRoles(int id);

        bool IsUserRole(int userId, int roleId);

        bool DeleteRole(int userId, int roleId);

        bool AddRole(int userId, int roleId);
    }
}
