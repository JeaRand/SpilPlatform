using SpilPlatform.Mvvm.Models;
using SpilPlatform.Mvvm.ViewModels;
using SpilPlatform.Mvvm.Views;
using System;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace SpilPlatform.Mvvm.Views;

public partial class GameView : ContentPage
{
    public GameView(Game game)
    {
        InitializeComponent();
        BindingContext = new GameViewModel(game);
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}