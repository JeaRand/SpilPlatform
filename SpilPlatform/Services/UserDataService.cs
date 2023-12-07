using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Newtonsoft.Json;
using System.Security.Cryptography;
using SpilPlatform.Mvvm.Models;

namespace SpilPlatform.Services
{
    public class UserDataService
    {
        private readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "userdata.json");

        public bool CheckAdminExists()
        {
            if (!File.Exists(filePath))
            {
                return false; // File doesn't exist
            }

            var userData = File.ReadAllText(filePath);
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            return users?.Any(u => u.IsAdmin) ?? false; // Check if any admin user exists
        }

        public void UserDataFileCheck()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    // Create the file with an empty JSON array
                    File.WriteAllText(filePath, "[]");
                    System.Diagnostics.Debug.WriteLine($"File Path: {filePath}");

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating file: {ex.Message}");
            }
        }

        public async Task<List<User>> LoadUsersAsync()
        {
            if (!File.Exists(filePath))
            {
                return new List<User>(); // Return an empty list if the file doesn't exist
            }

            using (var streamReader = new StreamReader(filePath))
            {
                var userData = await streamReader.ReadToEndAsync();
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                return users ?? new List<User>(); // Return the list of users or an empty list if null
            }
        }

        public async Task SaveUserDataAsync(User user, string password)
        {
            var users = await LoadUsersAsync();

            // Add the new user to the list
            user.PasswordHash = HashPassword(password);
            users.Add(user);

            var userData = JsonConvert.SerializeObject(users);
            using (var streamWriter = new StreamWriter(filePath, false))
            {
                await streamWriter.WriteAsync(userData);
            }
        }

        public async Task<User> LoadUserDataAsync(string username, string password)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            using (var streamReader = new StreamReader(filePath))
            {
                var userData = await streamReader.ReadToEndAsync();
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                var user = users?.FirstOrDefault(u => u.Username == username);

                // Verify the password
                if (user != null && VerifyPassword(password, user.PasswordHash))
                {
                    return user;
                }
                return null;
            }
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
