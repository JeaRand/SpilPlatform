
using SpilPlatform.Mvvm.Models;
using SpilPlatform.Mvvm.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace SpilPlatform.Mvvm.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public User SuperUser { get; } = new User { Username = "Admin", Password = "Admin123" };

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

        public ICommand AuthenticateCommand => new Command(Authenticate, CanLogin);

        public LoginViewModel()
        {
            // Initialize any additional properties here if necessary
        }

        public bool CanLogin()
        {
            // Check if both username and password are filled.
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        public void Authenticate()
        {
            IsAuthenticated = SuperUser.Username == Username && SuperUser.Password == Password;
           
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
