using Microsoft.Maui.Controls;
using SpilPlatform.Mvvm.ViewModels;

namespace SpilPlatform.Mvvm.Views
{
    public partial class LoginView : ContentPage
    {
        private readonly LoginViewModel loginViewModel;

        public LoginView()
        {
            InitializeComponent();
            loginViewModel = App.ServiceProvider.GetService<LoginViewModel>();

            if (loginViewModel == null)
            {
                throw new InvalidOperationException("LoginViewModel not found. Ensure it's registered with DI container.");
            }

            BindingContext = loginViewModel;
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (loginViewModel.CanLogin()) // Kontroller, om login er tilladt baseret på LoginViewModel
            {
                await loginViewModel.Authenticate(); // Udfør autentifikation
                if (loginViewModel.IsAuthenticated)
                {
                    await Navigation.PushAsync(new FrontPageView()); // Naviger til næste visning ved vellykket login
                }
                else
                {
                    // Håndter fejl ved login, f.eks. vise en besked til brugeren
                    await DisplayAlert("Fejl", "Forkert brugernavn eller adgangskode. Prøv igen.", "OK");
                }
            }
            else
            {
                // Håndter tilfælde, hvor login ikke er tilladt (felter ikke udfyldt)
                await DisplayAlert("Advarsel", "Du skal udfylde både brugernavn og adgangskode for at logge ind.", "OK");
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