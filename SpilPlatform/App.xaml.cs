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
            InitializeAppData();
        }
        private void InitializeAppData()
        {
            var userDataService = ServiceProvider.GetService<UserDataService>();
            var gameDataService = ServiceProvider.GetService<GameDataService>();
            var categoryDataService = ServiceProvider.GetService<CategoryDataService>();

            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                userDataService.UserDataFileCheck();
                gameDataService.GameDataFileCheck();
                categoryDataService.CategoryDataFileCheck();

                if (!userDataService.CheckAdminExists())
                {
                    MainPage = new NavigationPage(new RegistrationView(ServiceProvider)); // Navigate to RegistrationView
                }
                else
                {
                    MainPage = new NavigationPage(new FrontPageView(ServiceProvider)); // An admin exists so app will start normally at the frontpage
                }
            }

            else
            {
                MainPage = new NavigationPage(new FrontPageView(ServiceProvider));
            }
        }
    }
}