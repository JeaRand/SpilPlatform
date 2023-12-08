﻿using SpilPlatform.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SpilPlatform.Mvvm.Models;
using System.Windows.Input;

namespace SpilPlatform.Mvvm.ViewModels
{
    public class AddGameViewModel : INotifyPropertyChanged
    {
        private readonly IServiceProvider _serviceProvider;

        private string title { get; set; }
        private string description { get; set; }
        private long iconFileName { get; set; }
        private string link { get; set; }
        private List<Category> categories { get; set; }

        public string Title
        {
            get => title;
            set { title = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => description;
            set { description = value; OnPropertyChanged(); }
        }

        public long IconFileName
        {
            get => iconFileName;
            set { iconFileName = value; OnPropertyChanged(); }
        }

        public string Link
        {
            get => link;
            set { link = value; OnPropertyChanged(); }
        }

        public List<Category> Categories
        {
            get => categories;
            set { categories = value; OnPropertyChanged(); }
        }
        public ICommand AddGameCommand { get; }

        public AddGameViewModel(IServiceProvider serviceProvider, Guid gameId)
        {
            _serviceProvider = serviceProvider;
            AddGameCommand = new Command(async () => await AddGame(), CanAdd);
        }

        private bool CanAdd()
        {
            return !string.IsNullOrWhiteSpace(Title) ||
                   !string.IsNullOrWhiteSpace(Link);
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
                    Title = title,
                    Description = description,
                    IconFileName = iconFileName,
                    Link = link,
                    Categories = categories.ToArray()
                };

                await gameDataService.SaveGameDataAsync(newGame);
            }
        }

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