using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpilPlatform.Mvvm.Models;

namespace SpilPlatform.Services
{
    public static class SeedDataService
    {
        public static List<Game> Initialize()
        {
            var games = new List<Game>
            {
                new Game {Id = Guid.NewGuid(), Title = "Puslespil", Description = "Puslespil hvor du kan lære mere om forskellige møller rundt omkring i Danmark", Link = "https://i.simmer.io/@Leaske/mossgame"}
            };
            return games;
        }
    }
}