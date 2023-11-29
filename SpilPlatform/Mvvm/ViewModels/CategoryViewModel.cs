using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpilPlatform.Mvvm.ViewModels
{
    public class CategoryViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }

        public CategoryViewModel()
        {
            Categories = new ObservableCollection<Category>
        {
            new Category("0-1 Klasse"),
            new Category("2-3 Klasse"),
            new Category("4-5 Klasse"),
            new Category("6-7 Klasse")
        };
        }
    }

}
