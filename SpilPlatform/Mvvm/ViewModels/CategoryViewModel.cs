using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpilPlatform.Mvvm.ViewModels
{
    public class CategoryViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<GameCategory> Categories { get; set; }

        public CategoryViewModel()
        {
            Categories = new ObservableCollection<GameCategory>
        {
            new GameCategory("0.-1. klasse"),
            new GameCategory("2.-3. klasse"),
            new GameCategory("4.-5. klasse"),
            new GameCategory("6.+ klasse")
        };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
