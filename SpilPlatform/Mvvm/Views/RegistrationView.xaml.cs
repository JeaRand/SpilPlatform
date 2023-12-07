using SpilPlatform.Mvvm.ViewModels;

namespace SpilPlatform.Mvvm.Views;

public partial class RegistrationView : ContentPage
{
	public RegistrationView()
	{
		InitializeComponent();
        BindingContext = new RegistrationViewModel();
    }

    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is RegistrationViewModel vm)
        {
            if (vm.CanRegister())
            {
                vm.RegisterUser();

                await Navigation.PushAsync(new FrontPageView());
            }
            else
            {
                await DisplayAlert("Fejl ved registrering", "Tjek venligst om alle felter er udfyldt og om adgangskoden er den samme i begge felter.", "OK");
            }
        }
    }

    private async void OnRegisterButtonTapped(object sender, EventArgs e)
    {
        const uint animationDuration = 100; // Duration in milliseconds

        var button = (Button)sender;
        // Shrink the button to 95% of its size
        await button.ScaleTo(0.95, animationDuration, Easing.Linear);
        // Return to original size
        await button.ScaleTo(1, animationDuration, Easing.Linear);
    }
}