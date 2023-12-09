using SpilPlatform.Mvvm.ViewModels;

namespace SpilPlatform.Mvvm.Views;

public partial class AddGameView : ContentPage
{
	private readonly IServiceProvider _serviceProvider;

	public AddGameView(IServiceProvider serviceProvider)
	{
		InitializeComponent();
        _serviceProvider = serviceProvider;
        BindingContext = new AddGameViewModel(serviceProvider);
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {

        if (BindingContext is not AddGameViewModel addGameViewModel)
        {
            System.Diagnostics.Debug.WriteLine("BindingContext is not an AddGameViewModel.");
            return;
        }

        if (addGameViewModel.AddGameCommand.CanExecute(null))
        {
            bool confirm = await DisplayAlert("Bekræftelse", "Er du sikker på at du ønsker at tilføje dette spil?", "Ja", "Nej");
            if (confirm)
            {
                addGameViewModel.AddGameCommand.Execute(null);
                await addGameViewModel.AddGameTask;
                await Navigation.PushAsync(new FrontPageView(_serviceProvider));
            }
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}