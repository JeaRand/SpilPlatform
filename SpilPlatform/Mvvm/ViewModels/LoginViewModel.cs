using Microsoft.Identity.Client;
using SpilPlatform.Mvvm.Models;
using SpilPlatform.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpilPlatform.Mvvm.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IServiceProvider _serviceProvider;
        private TaskCompletionSource<bool> authenticateCompletionSource;
        private bool isAuthenticated;

        private string username;
        private string password;

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
                ((Command)AuthenticateCommand).ChangeCanExecute(); // Update the button state when the username changes.
            }
        }
        
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
                ((Command)AuthenticateCommand).ChangeCanExecute(); // Update the button state when the password changes.
            }
        }

        public bool IsAuthenticated
        {
            get => isAuthenticated;
            set
            {
                isAuthenticated = value;
                OnPropertyChanged();
            }
        }

        public ICommand AuthenticateCommand { get; }

        public LoginViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            AuthenticateCommand = new Command(ExecuteAuthenticateCommand, CanLogin);
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Username) && 
                   !string.IsNullOrEmpty(Password);
        }

        private void ExecuteAuthenticateCommand()
        {
            authenticateCompletionSource = new TaskCompletionSource<bool>();
            Authenticate();
        }

        private async void Authenticate()
        {
            try
            {
                var sessionManagementService = _serviceProvider.GetService<SessionManagementService>();
                var userDataService = _serviceProvider.GetService<UserDataService>();

                var users = await userDataService.LoadUsersAsync();
                var user = users.FirstOrDefault(u => u.Username == Username);

                if (user != null && UserDataService.VerifyPassword(Password, user.PasswordHash))
                {
                    IsAuthenticated = true;
                    sessionManagementService.LogIn(user);
                }
                else
                {
                    IsAuthenticated = false;
                }
            }
            catch (Exception ex)
            {
                IsAuthenticated = false;
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                authenticateCompletionSource.SetResult(IsAuthenticated);
            }
        }

        public Task<bool> WaitForAuthenticationAsync()
        {
            return authenticateCompletionSource.Task;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}