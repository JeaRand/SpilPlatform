using SpilPlatform.Mvvm.Models;
using SpilPlatform.Mvvm.ViewModels;
using SpilPlatform.Mvvm.Views;
using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SpilPlatform.Services;

namespace SpilPlatform.Mvvm.Views
{
    public partial class FrontPageView : ContentPage
    {
        private readonly IServiceProvider _serviceProvider;
        public FrontPageView(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

            BindingContext ??= new AggregationViewModel(serviceProvider);
        }

        private async void OnAddGameClicked(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button button)
                {
                    await Navigation.PushAsync(new AddGameView(_serviceProvider));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
            }
        }

        private async void OnEditGameClicked(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button button && button.BindingContext is Game selectedGame)
                {
                    await Navigation.PushAsync(new EditGameView(_serviceProvider, selectedGame));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
            }
        }

        private async void OnDeleteGameClicked(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button button && button.BindingContext is Game selectedGame)
                {
                    bool answer = await DisplayAlert("Bekræft sletning", $"Er du sikker på, at du vil slette {selectedGame.Title}?", "Ja", "Nej");
                    if (answer)
                    {
                        if (BindingContext is AggregationViewModel aggregationViewModel)
                        {
                            aggregationViewModel.GamesViewModel.DeleteGameAsync(selectedGame);
                            await DisplayAlert("Slettet", "Spillet er blevet slettet.", "OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in deleting game: {ex.Message}");
                await DisplayAlert("Fejl", "Der opstod en fejl under sletningen af spillet.", "OK");
            }
        }

        private async void OnPlayGameClicked(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button button && button.BindingContext is Game selectedGame)
                {
                    await Navigation.PushAsync(new GameView(selectedGame));
                }
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
                await Navigation.PushAsync(new SettingsView(_serviceProvider));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is AggregationViewModel aggregationViewModel)
            {
                await aggregationViewModel.InitializeViewModelsAsync();
            }
        }

        //void OnGameCategoryClicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new GameView(_serviceProvider));
        //}
    }
}