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
    public class AddGameViewModel : INotifyPropertyChanged
    {
        private readonly IServiceProvider _serviceProvider;
        private TaskCompletionSource<bool> addGameCompletionSource;

        private string title;
        private string description;
        private long iconFileName;
        private string link;
        public ObservableCollection<CategorySelectionViewModel> Categories { get; private set; }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
                ((Command)AddGameCommand).ChangeCanExecute();
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public long IconFileName
        {
            get => iconFileName;
            set
            {
                iconFileName = value;
                OnPropertyChanged();
            }
        }

        public string Link
        {
            get => link;
            set 
            { 
                link = value; 
                OnPropertyChanged();
                ((Command)AddGameCommand).ChangeCanExecute();
            }
        }

        public ICommand AddGameCommand { get; }

        public AddGameViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Categories = new ObservableCollection<CategorySelectionViewModel>();
            LoadCategories();
            AddGameCommand = new Command(async () => await ExecuteAddGameCommand(), CanAdd);
        }

        private bool CanAdd()
        {
            return !string.IsNullOrWhiteSpace(Title) &&
                   !string.IsNullOrWhiteSpace(Link);
        }
        private async Task ExecuteAddGameCommand(object parameter = null)
        {
            addGameCompletionSource = new TaskCompletionSource<bool>();
            await AddGame();
        }
        private async Task AddGame()
        {
            var gameDataService = _serviceProvider.GetService<GameDataService>();
            var games = await gameDataService.LoadGamesAsync();

            if (games.Any(x => x.Title == title) && games.Any(x => x.Link == link))
            {
                OnTitleExists();
                OnLinkExists();
            }
            else if (games.Any(x => x.Title == title))
            {
                OnTitleExists();
            }
            else if (games.Any(x => x.Link == link))
            {
                OnLinkExists();
            }
            else
            {
                Game newGame = new()
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    Description = description,
                    IconFileName = iconFileName,
                    Link = link,
                    Categories = Categories.Where(x => x.IsSelected).Select(x => x.Category).ToArray()
                };
                await gameDataService.SaveGameDataAsync(newGame);
            }
            addGameCompletionSource.SetResult(true);
        }

        private async void LoadCategories()
        {
            var categoryDataService = _serviceProvider.GetService<CategoryDataService>();
            var categories = await categoryDataService.LoadCategoriesAsync();
            foreach (var category in categories)
            {
                Categories.Add(new CategorySelectionViewModel(category));
            }
        }

        public Task<bool> AddGameTask => addGameCompletionSource?.Task;
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