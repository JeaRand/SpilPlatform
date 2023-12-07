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
        private readonly IServiceProvider _serviceProvider;
        public FrontPageView(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            BindingContext = new AggregationViewModel(serviceProvider);
        }

        private async void OnGameClicked(object sender, EventArgs e)
        {
            try
            {
                //if (BindingContext is AggregationViewModel aggregationViewModel)
                //{
                //    aggregationViewModel.GamesViewModel.Games
                //    await Navigation.PushAsync(new GameView(_serviceProvider, ));
                //}
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
                await Navigation.PushAsync(new LoginView(_serviceProvider));
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

        //void OnGameCategoryClicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new GameView(_serviceProvider));
        //}
    }
}