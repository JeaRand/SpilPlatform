using SpilPlatform.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SpilPlatform.Mvvm.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace SpilPlatform.Mvvm.ViewModels
{
    public class EditGameViewModel : INotifyPropertyChanged
    {
        private readonly IServiceProvider _serviceProvider;
        private TaskCompletionSource<bool> editGameCompletionSource;

        private Game originalGame;

        // Properties for displaying original game values
        public string OriginalTitle => originalGame?.Title;
        public string OriginalDescription => originalGame?.Description;
        public long OriginalIconFileName => originalGame.IconFileName;
        public string OriginalLink => originalGame?.Link;
        public Category[] OriginalCategories => originalGame?.Categories;
        public ObservableCollection<Category> OriginalCategoriesDisplay { get; private set; }

        // Properties bound to input fields for new values
        private string newTitle;
        private string newDescription;
        private long newIconFileName;
        private string newLink;
        public ObservableCollection<CategorySelectionViewModel> NewCategories { get; private set; }

        public Game OriginalGame
        {
            get => originalGame;
            set
            {
                originalGame = value;
                OnPropertyChanged(nameof(Game));
            }
        }
        // New value properties
        public string NewTitle
        {
            get => newTitle;
            set
            {
                newTitle = value;
                OnPropertyChanged();
            }
        }
        public string NewDescription
        {
            get => newDescription;
            set 
            { 
                newDescription = value; 
                OnPropertyChanged(); 
            }
        }
        public long NewIconFileName
        {
            get => newIconFileName;
            set 
            { 
                newIconFileName = value; 
                OnPropertyChanged(); 
            }
        }
        public string NewLink
        {
            get => newLink;
            set 
            { 
                newLink = value; 
                OnPropertyChanged(); 
            }
        }

        public ICommand UpdateGameCommand { get; }

        public EditGameViewModel(IServiceProvider serviceProvider, Game game)
        {
            _serviceProvider = serviceProvider;
            OriginalGame = game;
            NewCategories = new ObservableCollection<CategorySelectionViewModel>();
            OriginalCategoriesDisplay = new ObservableCollection<Category>();
            LoadCategories();
            System.Diagnostics.Debug.WriteLine($"{originalGame.Title}, {originalGame.Description}");
            UpdateGameCommand = new Command(async () => await UpdateGame(), CanUpdate);
        }

        private bool CanUpdate()
        {
            return !string.IsNullOrWhiteSpace(NewTitle) &&
                   !string.IsNullOrWhiteSpace(NewLink);
        }

        private async Task UpdateGame()
        {
            var gameDataService = _serviceProvider.GetService<GameDataService>();
            var games = await gameDataService.LoadGamesAsync();
            games.Remove(games.FirstOrDefault(x => x.Id == originalGame.Id));

            if (games.Any(x => x.Title == newTitle) && games.Any(x => x.Link == newLink))
            {
                OnTitleExists();
                OnLinkExists();
            }
            else if (games.Any(x => x.Title == newTitle))
            {
                OnTitleExists();
            }
            else if (games.Any(x => x.Link == newLink))
            {
                OnLinkExists();
            }
            else
            {
                Game newGame = new()
                {
                    Title = newTitle,
                    Description = newDescription,
                    IconFileName = newIconFileName,
                    Link = newLink,
                    Categories = NewCategories.Where(x => x.IsSelected).Select(x => x.Category).ToArray()
                };

                await gameDataService.SaveGameDataAsync(newGame);
            }
        }

        private async void LoadCategories()
        {
            var categoryDataService = _serviceProvider.GetService<CategoryDataService>();
            var categories = await categoryDataService.LoadCategoriesAsync();
            foreach (var category in categories)
            {
                NewCategories.Add(new CategorySelectionViewModel(category));
            }
            foreach (var category in OriginalCategories)
            {
                OriginalCategoriesDisplay.Add(category);
            }
        }

        public Task<bool> EditGameTask => editGameCompletionSource?.Task;
        public event Action TitleExists;
        public event Action LinkExists;

        private void OnTitleExists()
        {
            TitleExists?.Invoke();
        }
        private void OnLinkExists()
        {
            LinkExists?.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}