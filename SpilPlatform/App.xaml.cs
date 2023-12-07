using SpilPlatform.Mvvm.Views;
using System.Threading;
using SpilPlatform.Services;

namespace SpilPlatform
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }    
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            ServiceProvider = serviceProvider;
            InitializeAppDataAsync();
        }
        private void InitializeAppDataAsync()
        {
            UserDataService userDataService = new();

            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                userDataService.UserDataFileCheck();
                if (!userDataService.CheckAdminExists())
                {
                    MainPage = new NavigationPage(new RegistrationView()); // Navigate to RegistrationView
                }
                else
                {
                    MainPage = new NavigationPage(new FrontPageView()); // An admin exists so app will start normally at the frontpage
                }
            }

            else
            {
                MainPage = new NavigationPage(new FrontPageView());
            }
        }
    }
}