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
        private readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "gamedata.json");

        public void GameDataFileCheck()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    // Create the file with an empty JSON array
                    File.WriteAllText(filePath, "[]");
                    System.Diagnostics.Debug.WriteLine($"File Path: {filePath}");

                    var seedGames = SeedDataService.Initialize();
                    var seedGameData = JsonConvert.SerializeObject(seedGames);
                    using (var streamWriter = new StreamWriter(filePath, false))
                    {
                        streamWriter.Write(seedGameData);
                    }
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

            using (var streamReader = new StreamReader(filePath))
            {
                var gameData = await streamReader.ReadToEndAsync();
                var games = JsonConvert.DeserializeObject<List<Game>>(gameData);
                return games ?? new List<Game>(); // Return the list of games or an empty list if null
            }
        }

        public async Task SaveGameDataAsync(Game game)
        {
            var games = await LoadGamesAsync();

            // Add the new game to the list
            games.Add(game);

            var gameData = JsonConvert.SerializeObject(games);
            using (var streamWriter = new StreamWriter(filePath, false))
            {
                await streamWriter.WriteAsync(gameData);
            }
        }
    }
}