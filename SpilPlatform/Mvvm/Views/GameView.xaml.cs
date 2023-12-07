using SpilPlatform.Mvvm.Models;
using SpilPlatform.Mvvm.ViewModels;
using SpilPlatform.Mvvm.Views;
using System;
using Microsoft.Maui.Controls;

namespace SpilPlatform.Mvvm.Views;

public partial class GameView : ContentPage
{
    public GameView()
    {
        InitializeComponent();
        BindingContext = new GameViewModel();
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
