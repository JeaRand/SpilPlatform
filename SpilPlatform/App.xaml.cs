using SpilPlatform.Mvvm.Views;
using System.Threading;
using SpilPlatform.Services;

namespace SpilPlatform
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitializeAppDataAsync();
        }
        private async void InitializeAppDataAsync()
        {
            UserDataService userDataService = new UserDataService();

            if (!await userDataService.CheckAdminExists())
            {
                MainPage = new RegistrationView(); // Navigate to RegistrationView
            }
            else
            {
                MainPage = new FrontPageView(); // Or your usual main page
            }
        }
    }
}