using SpilPlatform.Mvvm.Views;

namespace SpilPlatform
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new ForsideView());
        }
    }
}