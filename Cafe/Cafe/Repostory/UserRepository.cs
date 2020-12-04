using Cafe.Tools;
using MicroORM;
using Model.Entities;
using Model.UIEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Repostory
{
    public class UserRepository:CRUD<AppUser>
    {
        public (bool, AppUser) ValidateUserCredentials(string username, string password)
        {
            var u = GetByColumName("Username", username);
            
            if (!(u.Count > 0)) return (false, default(AppUser));
            var b = new HashCreate().Verify(password, u.FirstOrDefault().Password);
            return (b, b ? u.FirstOrDefault() : null);
        }


        public List<UIUserRoles> GetUserRoles(int id)
        {
            string q = $"select * from UserRole u join Role r on u.RoleId=r.Id Where u.AppUserId={id}";
            using (CommanderBase commander = DBContext.CreateCommander())
            {
                var ur = commander.Reader<UIUserRoles>(q);
                return ur;
            }
        }
        public bool IsUserRole(int userId, int roleId)
        {
            string q = $"select * from UserRole u Where u.AppUserId={userId} and u.RoleId={roleId}";
            using (CommanderBase commander = DBContext.CreateCommander())
            {
                var ur = commander.Reader<UserRole>(q);
                if (ur.Count > 0) return true;
                else return false;
            }

        }

        public bool DeleteRole(int userId, int roleId)
        {
            string q = $"DELETE FROM UserRole WHERE AppUserId={userId} and RoleId={roleId}";
            using (CommanderBase commander = DBContext.CreateCommander())
            {
                var ur = commander.NonQuery(q);
                return ur;
            }

        }
        public bool AddRole(int userId, int roleId)
        {
            string q = $"INSERT INTO UserRole (AppUserId , RoleId) VALUES({userId} , {roleId} )";
            using (CommanderBase commander = DBContext.CreateCommander())
            {
                var ur = commander.NonQuery(q);
                return ur;
            }     
        }


    }
}
