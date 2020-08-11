using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLCafe.ConcreateRepository;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.UIEntites;
using Newtonsoft.Json;

namespace Cafe.Controllers.Admin
{
    public class UserController : Controller
    {
        UserRepository repository = new UserRepository();

        public IActionResult AllUser()
        {
            var count = repository.RowCount();
            ViewBag.Count = count;
            return View();
        }


        public string[] GetUsersForTable(int from, int to,string srcFor, string src)
        {
            List<AppUser> list;
            string[] result = new string[2];
            if (string.IsNullOrEmpty(src) || string.IsNullOrWhiteSpace(src))
            {
                result[0] = repository.RowCount().ToString();
                list = repository.getFromTo(from, to);
            }
            else
            {
                result[0] = repository.RowCountWithSrc(srcFor, src).ToString();
                list = repository.getFromToWithSrc(from, to, srcFor, src);
            }

            if (list != null) result[1]= JsonConvert.SerializeObject(list);
            else result[1]="";
            return result;
        }

        public async Task<IActionResult> UpdateUser(int id)
        {
            var u = (await repository.GetByIdAsync(id)).FirstOrDefault();
            ViewBag.UserRole = repository.getUserRoles(id);
            ViewBag.Roles = await new RoleRepository().GetAllAsync();
            return View(u);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(AppUser user)
        {
            await repository.UpdateAsync(user, user.Id);
            return RedirectToAction("AllUser");
        }

        public string AddAndRemoveRole(int roleId, int userId, bool value)
        {
            bool b = repository.IsUserRole(userId, roleId);
            if (value)
            {
                if (b) return "rol movcuddur";
                return repository.AddRole(userId, roleId).ToString();
            }
            else
            {
                if (!b) return "rol movcud deyil";
                return repository.DeleteRole(userId, roleId).ToString();
            }
        }
        
    }
}