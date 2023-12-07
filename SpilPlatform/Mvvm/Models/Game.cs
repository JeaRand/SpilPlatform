using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpilPlatform.Mvvm.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Image Icon { get; set; }
        public string Link { get; set; }
        public Category[] Categories { get; set; }
    }
}