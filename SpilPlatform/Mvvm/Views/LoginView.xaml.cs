using Microsoft.Maui.Controls;
using SpilPlatform.Mvvm.ViewModels;

namespace SpilPlatform.Mvvm.Views
{
    public partial class LoginView : ContentPage
    {
        private readonly IServiceProvider _serviceProvider;

        public LoginView(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            BindingContext = new LoginViewModel(serviceProvider);
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is LoginViewModel loginViewModel)
            {
                if (loginViewModel.CanLogin()) // Kontroller, om login er tilladt baseret p� LoginViewModel
                {
                    await loginViewModel.Authenticate(); // Udf�r autentifikation
                    if (loginViewModel.IsAuthenticated)
                    {
                        await Navigation.PushAsync(new FrontPageView(_serviceProvider)); // Naviger til n�ste visning ved vellykket login
                    }
                    else
                    {
                        // H�ndter fejl ved login, f.eks. vise en besked til brugeren
                        await DisplayAlert("Fejl", "Forkert brugernavn eller adgangskode. Pr�v igen.", "OK");
                    }
                }
                else
                {
                    // H�ndter tilf�lde, hvor login ikke er tilladt (felter ikke udfyldt)
                    await DisplayAlert("Advarsel", "Du skal udfylde b�de brugernavn og adgangskode for at logge ind.", "OK");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("LoginButton condition failed");
            }
        }
        // Event handler for the Back button
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            // Navigate back to the previous page
            await Navigation.PopAsync();
        }
    }
}