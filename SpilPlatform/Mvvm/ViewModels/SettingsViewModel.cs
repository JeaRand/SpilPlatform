using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpilPlatform.Mvvm.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly IServiceProvider _serviceProvider;
        public SettingsViewModel(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
