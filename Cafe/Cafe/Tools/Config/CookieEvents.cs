using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cafe.Tools.Config
{
    public class CookieEvents:CookieAuthenticationEvents
    {
        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            
            OnlineUsers.AddUser(context.Principal.Identity.Name);
            return base.ValidatePrincipal(context);
        }
    }

    

}
