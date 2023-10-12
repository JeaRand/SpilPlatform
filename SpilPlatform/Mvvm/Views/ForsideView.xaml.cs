using SpilPlatform.Mvvm.Models;
using SpilPlatform.Mvvm.ViewModels;
using SpilPlatform.Mvvm.Views;
using System;
using Microsoft.Maui.Controls;

namespace SpilPlatform.Mvvm.Views
{
    public partial class ForsideView : ContentPage
    {
        public ForsideView()
        {
            InitializeComponent();

            BindingContext = new SpilViewModel();
        }

        private async void OnOpenGameClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SpilView());

        }

        private async void OnOpenLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginView());
        }


    }
}
