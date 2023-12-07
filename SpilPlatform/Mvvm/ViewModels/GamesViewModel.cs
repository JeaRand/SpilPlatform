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
        public ObservableCollection<Game> Games { get; private set; }
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
            LoadGames();
        }

        private async void LoadGames()
        {
            var gameDataService = _serviceProvider.GetService<GameDataService>();
            var games = await gameDataService.LoadGamesAsync();
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