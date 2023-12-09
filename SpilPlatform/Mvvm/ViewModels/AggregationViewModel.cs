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
        private readonly IServiceProvider _serviceProvider;

        public GamesViewModel GamesViewModel { get; set; }
        public CategoryViewModel CategoryViewModel { get; set; }

        public bool IsUserLoggedIn
        {
            get
            {
                var sessionManagementService = _serviceProvider.GetService<SessionManagementService>();
                return sessionManagementService.IsUserLoggedIn;
            }
        }

        public bool IsLoggedInUserAdmin
        {
            get
            {
                var sessionManagementService = _serviceProvider.GetService<SessionManagementService>();
                return sessionManagementService.IsLoggedInUserAdmin();
            }
        }

        public AggregationViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            GamesViewModel = new GamesViewModel(serviceProvider);
            CategoryViewModel = new CategoryViewModel(serviceProvider);

            var sessionManagementService = _serviceProvider.GetService<SessionManagementService>();
            sessionManagementService.PropertyChanged += OnSessionServicePropertyChanged;
        }

        public void LogoutUser()
        {
            var sessionManagementService = _serviceProvider.GetService<SessionManagementService>();
            sessionManagementService.LogOut();
        }

        public async Task InitializeViewModelsAsync()
        {
            await GamesViewModel.InitializeAsync();
            
            // Initialize other view models if needed
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}