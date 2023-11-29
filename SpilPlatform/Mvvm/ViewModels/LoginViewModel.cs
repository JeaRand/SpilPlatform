using SpilPlatform.Mvvm.Models;
using SpilPlatform.Mvvm.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SpilPlatform.Services;

namespace SpilPlatform.Mvvm.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private UserDataService userDataService;
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
                ((Command)AuthenticateCommand).ChangeCanExecute(); // Opdater knappens tilstand, når brugernavnet ændres.
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
                ((Command)AuthenticateCommand).ChangeCanExecute(); // Opdater knappens tilstand, når adgangskoden ændres.
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
            userDataService = new UserDataService();
            // Initialiser eventuelle yderligere egenskaber her, hvis det er nødvendigt.
        }

        /// <summary>
        /// Bestemmer, om brugeren kan logge ind baseret på indtastet brugernavn og adgangskode.
        /// </summary>
        public bool CanLogin()
        {
            // Tjek om både brugernavn og adgangskode er udfyldt.
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        /// <summary>
        /// Forsøger at godkende brugeren ved at sammenligne brugernavn og adgangskode med superbrugeroplysninger.
        /// </summary>
        public async void Authenticate()
        {
            try
            {
                var users = await userDataService.LoadUsersAsync(); // Method to load all users
                var user = users.FirstOrDefault(u => u.Username == Username);

                if (user != null && userDataService.VerifyPassword(Password, user.PasswordHash))
                {
                    IsAuthenticated = true;
                    // Proceed with authenticated user
                }
                else
                {
                    IsAuthenticated = false;
                    // Handle authentication failure (e.g., show error message)
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log error, show error message)
                IsAuthenticated = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
