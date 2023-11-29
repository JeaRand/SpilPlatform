using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpilPlatform.Mvvm.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Icon { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }
    }
}
