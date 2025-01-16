using expenzo.Database;
using expenzo.Models;
using expenzo.Services.Interfaces;
using System.Collections.Concurrent;

namespace expenzo.Services
{
    public class UserService : IUserService
    {
        private readonly UserDao _userDao; // Data access object for user-related operations
        private readonly EncryptionService _encryptionService; // Service for encrypting passwords
        private static ConcurrentDictionary<string, bool> _loggedInUsers = new ConcurrentDictionary<string, bool>(); // Thread-safe dictionary to track logged-in users
        // Constructor to initialize the user data access object and encryption service
        public UserService(UserDao userDao, EncryptionService encryptionService)
        {
            _userDao = userDao;
            _encryptionService = encryptionService;
            SeedAdminUser(encryptionService).Wait(); // Seed the admin user if user doesn't exist
        }

        public async Task<bool> RegisterUser(User user)
        {
            if (await UserExists(user.UserName))
            {
                return false; // Return false if the user already exists
            }

            user.Password = _encryptionService.Encrypt(user.Password); // Encrypt the password
            _userDao.AddUser(user); // Add the user to the database
            return true; // Return true if the user is successfully registered
        }

        public async Task<User> LoginUser(string username, string password)
        {
            var encryptedPassword = _encryptionService.Encrypt(password); // Encrypt the password for comparison
            var user = await _userDao.GetUser(username, encryptedPassword); // Retrieve the user from the database
            if (user != null)
            {
                _loggedInUsers[username] = true; // Mark the user as logged in
            }
            return user; // Return the user if login is successful, otherwise return null
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userDao.UserExists(username); // Check if the user exists in the database
        }

        public Task<bool> IsUserLoggedIn()
        {
            return Task.FromResult(_loggedInUsers.ContainsKey("username") && _loggedInUsers["username"]); // Check if the user is logged in
        }

        public async Task<bool> UpdateUser(User user)
        {
            user.Password = _encryptionService.Encrypt(user.Password); // Encrypt the password before updating
            return await _userDao.UpdateUser(user); // Update the user in the database
        }

        public async Task<bool> AnyUserExists()
        {
            return await _userDao.AnyUserExists(); // Check if any user exists in the database
        }

        private async Task SeedAdminUser(EncryptionService encryptionService)
        {
            if (!await AnyUserExists())
            {
                var adminUser = new User
                {
                    UserName = "admin",
                    Password = encryptionService.Encrypt("admin"), // Encrypt the default admin password
                    Currency = "NPR"
                };
                await RegisterUser(adminUser); // Register the admin user
            }
        }
    }
}