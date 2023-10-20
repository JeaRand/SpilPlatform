using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpilPlatform.Mvvm.Models
{
    public class Spil
    {
        public int GameId { get; set; }
        public string Titel { get; set; }
        public string Beskrivelse { get; set; }
        public long Ikon { get; set; }
        public string Link { get; set; }

        public string Kategori { get; set; }
    }
}
