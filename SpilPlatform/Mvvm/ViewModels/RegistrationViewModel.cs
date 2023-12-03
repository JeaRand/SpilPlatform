using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SpilPlatform.Services;
using System.Windows.Input;
using SpilPlatform.Mvvm.Models;

namespace SpilPlatform.Mvvm.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private readonly UserDataService userDataService = new();
        private string username;
        private string password;
        private string confirmPassword;

        // Bind these properties to your Entry fields in the XAML
        public string Username
        {
            get => username;
            set { username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => password;
            set { password = value; OnPropertyChanged(); }
        }

        public string ConfirmPassword
        {
            get => confirmPassword;
            set { confirmPassword = value; OnPropertyChanged(); }
        }

        public ICommand RegisterCommand { get; }

        public RegistrationViewModel()
        {
            RegisterCommand = new Command(RegisterUser, CanRegister);
        }

        public bool CanRegister()
        {
            return !string.IsNullOrWhiteSpace(Username) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   Password == ConfirmPassword;
        }


        public async void RegisterUser()
        {
            var users = await userDataService.LoadUsersAsync();

            if (users.Any(x => x.Username == username))
            {
                OnUsernameExists();
            }
            else
            {
                User newUser = new User
                {
                    Username = username,
                    IsAdmin = true
                };
                
                await userDataService.SaveUserDataAsync(newUser, password);
            }
        }

        public event Action UsernameExists;

        private void OnUsernameExists()
        {
            UsernameExists?.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
