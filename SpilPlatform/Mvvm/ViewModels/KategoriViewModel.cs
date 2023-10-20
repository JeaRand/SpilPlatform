using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpilPlatform.Mvvm.ViewModels
{
    public class KategoriViewModel
    {
        public ObservableCollection<KategoriModel> Kategorier { get; set; }

        public KategoriViewModel()
        {
            Kategorier = new ObservableCollection<KategoriModel>
        {
            new KategoriModel("0-1 Klasse"),
            new KategoriModel("2-3 Klasse"),
            new KategoriModel("4-5 Klasse"),
            new KategoriModel("6-7 Klasse")
            
        };
        }
    }

}
