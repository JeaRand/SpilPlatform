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
        public ObservableCollection<Category> Categories { get; set; }

        public CategoryViewModel()
        {
            Categories = new ObservableCollection<Category>
        {
            new Category("0.-1. klasse"),
            new Category("2.-3. klasse"),
            new Category("4.-5. klasse"),
            new Category("6.+ klasse")
        };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
