using SpilPlatform.Mvvm.Models;
using SpilPlatform.Mvvm.ViewModels;
using SpilPlatform.Mvvm.Views;
using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace SpilPlatform.Mvvm.Views
{
    public partial class FrontPageView : ContentPage
    {
        public FrontPageView()
        {
            InitializeComponent();
            BindingContext = App.ServiceProvider.GetService<AggregationViewModel>();
        }

        private async void OnGameClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new GameView());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
            }
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new LoginView());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
            }
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            try 
            { 
                if (BindingContext is AggregationViewModel aggregationViewModel) 
                {
                    System.Diagnostics.Debug.WriteLine("Does this work?");
                    // Use the public property to access the SessionManagementService
                    aggregationViewModel.LogoutUser();
                }
            }
            catch (Exception ex) 
            { 
                System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
            }
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new SettingsView());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
            }
        }

        void OnPuzzleCategoryClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GameView());
        }
    }
}