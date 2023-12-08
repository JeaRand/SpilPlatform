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
            if (BindingContext is not LoginViewModel loginViewModel)
            {
                System.Diagnostics.Debug.WriteLine("BindingContext is not a LoginViewModel.");
                return;
            }

            if (loginViewModel.AuthenticateCommand.CanExecute(null))
            {
                loginViewModel.AuthenticateCommand.Execute(null);
                bool isAuthenticated = await loginViewModel.WaitForAuthenticationAsync();

                if (isAuthenticated)
                {
                    await Navigation.PushAsync(new FrontPageView(_serviceProvider)); // Successful login
                }
                else
                {
                    // Authentication failed
                    await DisplayAlert("Fejl", "Forkert brugernavn eller adgangskode. Prøv igen.", "OK");
                }
            }
            else
            {
                // Login conditions not met
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