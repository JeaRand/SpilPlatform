using SpilPlatform.Mvvm.Models;
using SpilPlatform.Mvvm.ViewModels;
using SpilPlatform.Mvvm.Views;
using System;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace SpilPlatform.Mvvm.Views;

public partial class GameView : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public GameView(IServiceProvider serviceProvider, Guid gameId)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        BindingContext = new GameViewModel(serviceProvider, gameId);
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
