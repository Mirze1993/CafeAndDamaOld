using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Tools.Config
{
    public class OnlineUserFilterAttribute : Attribute,IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            OnlineUsers.AddUser(context.HttpContext.User.Identity.Name);
        }
    }

   
}
