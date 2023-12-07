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

        private string username;
        private string password;
        private bool isAuthenticated;

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
            AuthenticateCommand = new Command(async () => await Authenticate(), CanLogin);
        }

        public bool CanLogin()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        public async Task Authenticate()
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
                    // Proceed with authenticated user
                }
                else
                {
                    IsAuthenticated = false;
                    System.Diagnostics.Debug.WriteLine("Could not authenticate - User and password combination doesn't exist");
                    // Handle authentication failure (e.g., show error message)
                }
            }
            catch (Exception ex)
            {
                IsAuthenticated = false;
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}