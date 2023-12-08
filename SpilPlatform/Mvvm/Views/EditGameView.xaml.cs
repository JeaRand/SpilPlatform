using SpilPlatform.Mvvm.Models;
using SpilPlatform.Mvvm.ViewModels;
using SpilPlatform.Mvvm.Views;
using System;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace SpilPlatform.Mvvm.Views;

public partial class EditGameView : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public EditGameView(IServiceProvider serviceProvider, Guid gameId)
	{
		InitializeComponent();
        _serviceProvider = serviceProvider;
        BindingContext = new EditGameViewModel(serviceProvider, gameId);
    }

    private async void OnUpdateButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is not EditGameViewModel editGameViewModel)
        {
            System.Diagnostics.Debug.WriteLine("BindingContext is not an EditGameViewModel.");
            return;
        }

        if (editGameViewModel.UpdateGameCommand.CanExecute(null))
        {
            bool confirm = await DisplayAlert("Bekr�ftelse", "Er du sikker p�, at du �nsker at foretage �ndringer p� dette spil?", "Ja", "Nej");
            if (confirm)
            {
                editGameViewModel.UpdateGameCommand.Execute(null);
                await Navigation.PushAsync(new FrontPageView(_serviceProvider));
            }
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}