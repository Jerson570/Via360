using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CloudinaryDotNet.Core;
using Firebase.Auth;
using Firebase.Auth.Providers;

namespace Via360.App.Services
{
    public class FirebaseAuthService : IAuthService
    {
        private readonly FirebaseAuthClient _authClient;
        private readonly ConfiguracionService _config;

        public FirebaseAuthService(ConfiguracionService config)
        {
            _config = config;

            var firebaseConfig = new FirebaseAuthConfig
            {
                ApiKey = _config.FirebaseApiKey,
                AuthDomain = "via360-app.firebaseapp.com",
                Providers = new [] { new EmailProvider() }
            };
            _authClient=new FirebaseAuthClient(firebaseConfig);
        }
        public async Task<string> RegistroAsync(string email, string password, string nombre)
        {
            try
            {
                var userCredential = await _authClient.CreateUserWithEmailAndPasswordAsync(email, password, nombre);
                return userCredential.User.Uid;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en registro: {ex.Message}");
                return null;
            }
        }
        public async Task<string> LoginAsync(string email, string password)
        {
            try
            {
                var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
                return userCredential.User.Uid;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<string> GetActualUserId()
        {
            return await Task.FromResult(_authClient.User?.Uid);
        }
        public bool IsLoggedIn() => _authClient.User != null;
        public void Logout() => _authClient.SignOut();

    }
}
