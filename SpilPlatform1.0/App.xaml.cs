using SpilPlatform.Mvvm.Views;

namespace SpilPlatform1._0
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ForsideView();
        }
    }
}