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

            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                userDataService.UserDataFileCheck();
                if (!userDataService.CheckAdminExists())
                {
                    MainPage = new RegistrationView(); // Navigate to RegistrationView
                }
                else
                {
                    MainPage = new FrontPageView(); // An admin exists so app will start normally at the frontpage
                }
            }

            else
            {
                MainPage = new FrontPageView();
            }
        }
    }
}