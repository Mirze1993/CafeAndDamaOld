using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLCafe.Interface;
using Model.Entities;

namespace Cafe.Tools.Services
{
    public class UserService : IUserService
    {
        IUserRepository repository; 

        public UserService(IUserRepository userRepository)
        {
            repository = userRepository;
        }

        public Task<(bool, AppUser)> ValidateUserCredentialsAsync(string username, string password)
        {
            var u = repository.Reader<AppUser>($"Select *  from AppUser Where Username='{username}'");
            if (!(u.Count > 0)) return Task.FromResult((false, default(AppUser)));
            var b = new HashCreate().Verify(password, u.FirstOrDefault().Password);
            return Task.FromResult((b, b?u.FirstOrDefault():null));
        }
    }
}
