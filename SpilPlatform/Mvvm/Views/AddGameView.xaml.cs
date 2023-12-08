using SpilPlatform.Mvvm.ViewModels;

namespace SpilPlatform.Mvvm.Views;

public partial class AddGameView : ContentPage
{
	private readonly IServiceProvider _serviceProvider;

	public AddGameView(IServiceProvider serviceProvider, Guid gameId)
	{
		InitializeComponent();
        _serviceProvider = serviceProvider;
        BindingContext = new AddGameViewModel(serviceProvider, gameId);
    }

    private async void OnSaveButton_Clicked(object sender, EventArgs e)
    {

        if (BindingContext is not AddGameViewModel addGameViewModel)
        {
            System.Diagnostics.Debug.WriteLine("BindingContext is not an AddGameViewModel.");
            return;
        }

        if (addGameViewModel.AddGameCommand.CanExecute(null))
        {
            bool confirm = await DisplayAlert("Bekr�ftelse", "Er du sikker p� at du �nsker at tilf�je dette spil?", "Ja", "Nej");
            if (confirm)
            {
                addGameViewModel.AddGameCommand.Execute(null);
                await Navigation.PushAsync(new FrontPageView(_serviceProvider));
            }
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}