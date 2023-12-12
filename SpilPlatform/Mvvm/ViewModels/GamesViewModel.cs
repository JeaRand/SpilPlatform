using SpilPlatform.Mvvm.Models;
using SpilPlatform.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SpilPlatform.Mvvm.ViewModels
{
    public class GamesViewModel : INotifyPropertyChanged
    {
        private readonly IServiceProvider _serviceProvider;

        private ObservableCollection<Game> _games;
        public ObservableCollection<Game> Games
        {
            get => _games;
            private set
            {
                _games = value;
                OnPropertyChanged();
            }
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public GamesViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Games = new ObservableCollection<Game>();
        }

        public async Task InitializeAsync()
        {
            await LoadGamesAsync();
        }

        public async void DeleteGameAsync(Game game)
        {
            var gameDataService = _serviceProvider.GetService<GameDataService>();
            await gameDataService.DeleteGameDataAsync(game);
            await InitializeAsync();
        }

       
        private async Task LoadGamesAsync()
        {
            var gameDataService = _serviceProvider.GetService<GameDataService>();
            var games = await gameDataService.LoadGamesAsync();
            Games.Clear();
            foreach (var game in games)
            {
                Games.Add(game);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}