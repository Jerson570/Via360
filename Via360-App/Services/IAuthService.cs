using System;
using System.Collections.Generic;
using System.Text;

namespace Via360.App.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string email, string password);
        Task<string> RegistroAsync(string email, string password, string nombreCompleto);
        void Logout();
        bool IsLoggedIn();  
    }
}
