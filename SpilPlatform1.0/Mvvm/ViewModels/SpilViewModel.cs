using SpilPlatform.Mvvm.Models;
using System;
using System.ComponentModel;


namespace SpilPlatform.Mvvm.ViewModels
{
    public class SpilViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Spil spil;
        public Spil Spil
        {
            get { return spil; }
            set
            {
                if (spil != value)
                {
                    spil = value;
                    OnPropertyChanged(nameof(Spil));
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

        public SpilViewModel()
        {
            Spil = new Spil
            {
                Titel = "Pusslespil",
                Beskrivelse = "Pusslespil hvor du kan lære mere om de forskellige Møller i Danmark",
                Link = "https://simmer.io/@Leaske/mosspuzzle",
            };
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
