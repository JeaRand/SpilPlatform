using SpilPlatform.Mvvm.Models;
using SpilPlatform.Mvvm.ViewModels;
using SpilPlatform.Mvvm.Views;
using System;
using Microsoft.Maui.Controls;

namespace SpilPlatform.Mvvm.Views;

public partial class SpilView : ContentPage
{
    public SpilView()
    {
        InitializeComponent();
        BindingContext = new SpilViewModel();
    }
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ForsideView());
    }


}
