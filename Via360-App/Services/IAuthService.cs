using System;
using System.Collections.Generic;
using System.Text;

namespace Via360.App.Services
{
    public interface IAuthService
    {
        Task<string> RegistroAsync(string email, string password);
        Task<string> LoginAsync(string email, string password);
        void Logout();
        bool IsLoggedIn();
    }
}
