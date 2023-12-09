using SpilPlatform.Mvvm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace SpilPlatform.Services
{
    public class CategoryDataService
    {
        private readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "categorydata.json");

        public async void CategoryDataFileCheck()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    var seedCategories = SeedDataService.InitializeCategories();
                    var seedCategoryData = JsonConvert.SerializeObject(seedCategories);
                    await File.WriteAllTextAsync(filePath, seedCategoryData);
                    System.Diagnostics.Debug.WriteLine($"File Path: {filePath}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating file: {ex.Message}");
            }
        }

        public async Task<List<Category>> LoadCategoriesAsync()
        {
            CategoryDataFileCheck();

            using var streamReader = new StreamReader(filePath);
            var categoryData = await streamReader.ReadToEndAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(categoryData);
            return categories ?? new List<Category>();
        }

        public async Task DeleteCategoryDataAsync(Category category)
        {
            var categories = await LoadCategoriesAsync();
            var categoryToDelete = categories.FirstOrDefault(g => g.Id == category.Id);
            if (categoryToDelete != null)
            {
                categories.Remove(categoryToDelete);
                var categoryData = JsonConvert.SerializeObject(categories);
                using var streamWriter = new StreamWriter(filePath, false);
                await streamWriter.WriteAsync(categoryData);
            }
        }

        public async Task SaveCategoryDataAsync(Category category)
        {
            var categories = await LoadCategoriesAsync();

            categories.Add(category);

            var categoryData = JsonConvert.SerializeObject(categories);
            using var streamWriter = new StreamWriter(filePath, false);
            await streamWriter.WriteAsync(categoryData);
        }
    }
}