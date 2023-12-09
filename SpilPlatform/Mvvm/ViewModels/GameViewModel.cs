using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SpilPlatform.Mvvm.Models;
using SpilPlatform.Services;

namespace SpilPlatform.Mvvm.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private readonly IServiceProvider _serviceProvider;
        private Game game;
        public Game Game
        {
            get => game;
            set
            {
                game = value;
                OnPropertyChanged(nameof(Game));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public GameViewModel(IServiceProvider serviceProvider, Game game)
        {
            _serviceProvider = serviceProvider;
            Game = game;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}