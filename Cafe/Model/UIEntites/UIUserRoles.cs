using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIEntites
{
    public class UIUserRoles
    {
        public AppUser User { get; set; }
        public List<Role> Roles { get; set; }
        public UIUserRoles()
        {
            Roles = new List<Role>();
        }
    }
}
