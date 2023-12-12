using SpilPlatform.Mvvm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpilPlatform.Services
{
    public class GameDataService
    {
        private readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "spilplatformgamedata.json");

        public async void GameDataFileCheck()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    var seedGames = SeedDataService.InitializeGames();
                    var seedGameData = JsonConvert.SerializeObject(seedGames);
                    await File.WriteAllTextAsync(filePath, seedGameData);
                    System.Diagnostics.Debug.WriteLine($"File Path: {filePath}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating file: {ex.Message}");
            }
        }

        public async Task<List<Game>> LoadGamesAsync()
        {
            GameDataFileCheck();

            using var streamReader = new StreamReader(filePath);
            var gameData = await streamReader.ReadToEndAsync();
            var games = JsonConvert.DeserializeObject<List<Game>>(gameData);
            return games ?? new List<Game>();
        }

        public async Task DeleteGameDataAsync(Game game)
        {
            var games = await LoadGamesAsync();
            var gameToDelete = games.FirstOrDefault(g => g.Id == game.Id);
            if (gameToDelete != null)
            {
                games.Remove(gameToDelete);
                var gameData = JsonConvert.SerializeObject(games);
                using var streamWriter = new StreamWriter(filePath, false);
                await streamWriter.WriteAsync(gameData);
            }
        }

        public async Task SaveGameDataAsync(Game game)
        {
            var games = await LoadGamesAsync();

            games.Add(game);

            var gameData = JsonConvert.SerializeObject(games);
            using var streamWriter = new StreamWriter(filePath, false);
            await streamWriter.WriteAsync(gameData);
        }
    }
}