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

            var aggregationViewModel = new AggregationViewModel
            {
                GameVM = new GameViewModel(),
                CategoryVM = new CategoryViewModel()
            };

            BindingContext = aggregationViewModel;
        }

        private async void OnOpenGameClicked(object sender, EventArgs e)
        {
            // Du kan nu få adgang til SpilViewModel via AggregationViewModel
            await Navigation.PushAsync(new GameView());
        }

        private async void OnOpenLoginClicked(object sender, EventArgs e)
        {
            // Du kan også få adgang til KategoriViewModel via AggregationViewModel
            await Navigation.PushAsync(new LoginView());
        }
        void OnPuzzleCategoryClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GameView());
        }
    }
}

