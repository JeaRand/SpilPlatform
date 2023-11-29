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

        public async Task<bool> CheckAdminExists()
        {
            if (!File.Exists(filePath))
            {
                return false; // File doesn't exist
            }

            var userData = await File.ReadAllTextAsync(filePath);
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            return users?.Any(u => u.IsAdmin) ?? false; // Check if any admin user exists
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

        public async Task SaveUserDataAsync(User user)
        {
            // Hash the user's password before saving
            user.PasswordHash = HashPassword(user.PasswordHash);

            var userData = JsonConvert.SerializeObject(user);
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

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
