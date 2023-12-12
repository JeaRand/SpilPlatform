using Microsoft.Extensions.DependencyInjection;
using SpilPlatform.Mvvm.ViewModels;

namespace SpilPlatform.Mvvm.Views;

public partial class RegistrationView : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public RegistrationView(IServiceProvider serviceProvider)
    {
		InitializeComponent();
        _serviceProvider = serviceProvider;
        BindingContext = new RegistrationViewModel(serviceProvider);
    }

    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is RegistrationViewModel registrationViewModel)
        {
            if (registrationViewModel.CanRegister())
            {
                registrationViewModel.RegisterUser();

                await Navigation.PushAsync(new LoginView(_serviceProvider));
            }
            else
            {
                await DisplayAlert("Fejl ved registrering", "Tjek venligst om alle felter er udfyldt og om adgangskoden er den samme i begge felter.", "OK");
            }
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("RegisterButton condition failed");
        }
    }
}