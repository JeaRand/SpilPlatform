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
