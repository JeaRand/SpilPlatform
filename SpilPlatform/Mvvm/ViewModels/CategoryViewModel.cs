using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SpilPlatform.Services;

namespace SpilPlatform.Mvvm.ViewModels
{
    public class CategoryViewModel : INotifyPropertyChanged
    {
        private readonly IServiceProvider _serviceProvider;
        public ObservableCollection<Category> Categories { get; private set; }

        public CategoryViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Categories = new ObservableCollection<Category>();
            LoadCategories();
        }

        public async void DeleteCategory(Category category)
        {
            var categoryDataService = _serviceProvider.GetService<CategoryDataService>();
            await categoryDataService.DeleteCategoryDataAsync(category);
            Categories.Clear();
            LoadCategories();
        }

        private async void LoadCategories()
        {
            var categoryDataService = _serviceProvider.GetService<CategoryDataService>();
            var categories = await categoryDataService.LoadCategoriesAsync();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}