using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpilPlatform.Mvvm.Models;

namespace SpilPlatform.Services
{
    public static class SeedDataService
    {
        public static List<Game> InitializeGames()
        {
            var games = new List<Game>
            {
                new Game {Id = Guid.NewGuid(),Title = "Puslespil", Description = "Puslespil hvor du kan lære mere om forskellige møller rundt omkring i Danmark", Link = "https://i.simmer.io/@Leaske/mossgame"}
            };
            return games;
        }

        public static List<Category> InitializeCategories() 
        {
            var categories = new List<Category>
            {
                new Category("0.-1. klasse") {Id = Guid.NewGuid()},
                new Category("2.-3. klasse") {Id = Guid.NewGuid()},
                new Category("4.-5. klasse") {Id = Guid.NewGuid()},
                new Category("6.+ klasse") {Id = Guid.NewGuid()},
            };
            return categories;
        }
    }
}