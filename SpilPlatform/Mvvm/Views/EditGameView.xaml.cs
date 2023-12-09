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

    public EditGameView(IServiceProvider serviceProvider, Game game)
	{
		InitializeComponent();
        _serviceProvider = serviceProvider;
        BindingContext = new EditGameViewModel(serviceProvider, game);
    }

    private async void OnUpdateButtonClicked(object sender, EventArgs e)
    {
        if (BindingContext is not EditGameViewModel editGameViewModel)
        {
            System.Diagnostics.Debug.WriteLine("BindingContext is not an EditGameViewModel.");
            return;
        }

        if (editGameViewModel.UpdateGameCommand.CanExecute(null))
        {
            bool confirm = await DisplayAlert("Bekræftelse", "Er du sikker på, at du ønsker at foretage ændringer på dette spil?", "Ja", "Nej");
            if (confirm)
            {
                editGameViewModel.UpdateGameCommand.Execute(null);
                await editGameViewModel.EditGameTask;
                await Navigation.PushAsync(new FrontPageView(_serviceProvider));
            }
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}