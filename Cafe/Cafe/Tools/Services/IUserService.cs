using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Tools.Services
{
    public interface IUserService
    {
        Task<(bool, AppUser)> ValidateUserCredentialsAsync(string username, string password);
    }
}
