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

            var aggregationViewModel = new AggregationViewModel
            {
                SpilVM = new SpilViewModel(),
                KategoriVM = new KategoriViewModel()
            };

            BindingContext = aggregationViewModel;
        }

        private async void OnOpenGameClicked(object sender, EventArgs e)
        {
            // Du kan nu få adgang til SpilViewModel via AggregationViewModel
            await Navigation.PushAsync(new SpilView());
        }

        private async void OnOpenLoginClicked(object sender, EventArgs e)
        {
            // Du kan også få adgang til KategoriViewModel via AggregationViewModel
            await Navigation.PushAsync(new LoginView());
        }
    }



}

