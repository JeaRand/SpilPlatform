using SpilPlatform.Mvvm.ViewModels;
using Microsoft.Identity.Client;
using SpilPlatform.Mvvm.Models;
using SpilPlatform.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpilPlatform.Mvvm.Views
{
    public class AggregationViewModel : INotifyPropertyChanged
    {
        private readonly SessionManagementService sessionManagementService;

        public GameViewModel GameViewModel { get; set; }
        public CategoryViewModel CategoryViewModel { get; set; }

        public bool IsUserLoggedIn => sessionManagementService.IsUserLoggedIn;
        public bool IsLoggedInUserAdmin => sessionManagementService.IsLoggedInUserAdmin();

        public AggregationViewModel(SessionManagementService sessionManagementService)
        {
            this.sessionManagementService = sessionManagementService;
            GameViewModel = new GameViewModel();
            CategoryViewModel = new CategoryViewModel();

            // Subscribe to PropertyChanged event of sessionManagementService
            sessionManagementService.PropertyChanged += OnSessionServicePropertyChanged;
        }

        public void LogoutUser()
        {
            sessionManagementService.LogOut();
        }

        private void OnSessionServicePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SessionManagementService.IsUserLoggedIn) ||
                e.PropertyName == nameof(SessionManagementService.LoggedInUser))
            {
                OnPropertyChanged(nameof(IsUserLoggedIn));
                OnPropertyChanged(nameof(IsLoggedInUserAdmin));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}