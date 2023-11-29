using SpilPlatform.Mvvm.Models;
using System;
using System.ComponentModel;


namespace SpilPlatform.Mvvm.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Game game;
        public Game Game
        {
            get { return game; }
            set
            {
                if (game != value)
                {
                    game = value;
                    OnPropertyChanged(nameof(Game));
                }
            }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }

        public GameViewModel()
        {
            Game = new Game
            {
                Title = "Pusslespil",
                Description = "Pusslespil hvor du kan lære mere om de forskellige Møller i Danmark",
                Link = "https://simmer.io/@Leaske/mossgame",
            };
    
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
