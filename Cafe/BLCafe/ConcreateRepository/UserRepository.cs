using Model.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BLCafe.ConcreateRepository
{
    public class UserRepository : CRUD<AppUser>
    {
        public List<UserRole> getUserRoles(int id)
        {
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                string q = $"select * from UserRole u Where u.AppUserId={id}";
                var ur = connection.ExecuteReader<UserRole>(q);
                return ur;
            }
        }
        public bool IsUserRole(int userId, int roleId)
        {
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                string q = $"select * from UserRole u Where u.AppUserId={userId} and u.RoleId={roleId}";
                var ur = connection.ExecuteReader<UserRole>(q);
                if (ur.Count > 0) return true;
                else return false;
            }
        }

        public bool DeleteRole(int userId, int roleId)
        {
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                string q = $"DELETE FROM UserRole WHERE AppUserId={userId} and RoleId={roleId}";
                var ur = connection.ExecuteQuery(q, null);
                return ur;
            }
        }
        public bool AddRole(int userId, int roleId)
        {
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                string q = $"INSERT INTO UserRole (AppUserId , RoleId) VALUES({userId} , {roleId} )";
                var ur = connection.ExecuteQuery(q, null);
                return ur;
            }
        }
        
    }
}
