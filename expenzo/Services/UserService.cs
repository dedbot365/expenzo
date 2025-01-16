using expenzo.Database;
using expenzo.Models;
using expenzo.Services.Interfaces;
using System.Collections.Concurrent;

namespace expenzo.Services
{
    public class UserService : IUserService
    {
        private readonly UserDao _userDao;
        private static ConcurrentDictionary<string, bool> _loggedInUsers = new ConcurrentDictionary<string, bool>();

        public UserService(UserDao userDao, EncryptionService encryptionService)
        {
            _userDao = userDao;
            SeedAdminUser(encryptionService).Wait();
        }

        public async Task<bool> RegisterUser(User user)
        {
            if (await UserExists(user.UserName))
            {
                return false;
            }

            user.Password = new EncryptionService().Encrypt(user.Password); // Encrypt the password
            _userDao.AddUser(user);
            return true;
        }

        public async Task<User> LoginUser(string username, string password)
        {
            var encryptedPassword = new EncryptionService().Encrypt(password); // Encrypt the password for comparison
            var user = await _userDao.GetUser(username, encryptedPassword);
            if (user != null)
            {
                _loggedInUsers[username] = true;
            }
            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userDao.UserExists(username);
        }

        public Task<bool> IsUserLoggedIn()
        {
            return Task.FromResult(_loggedInUsers.ContainsKey("username") && _loggedInUsers["username"]);
        }

        public async Task<bool> AnyUserExists()
        {
            return await _userDao.AnyUserExists();
        }

        private async Task SeedAdminUser(EncryptionService encryptionService)
        {
            if (!await AnyUserExists())
            {
                var adminUser = new User
                {
                    UserName = "admin",
                    Password = encryptionService.Encrypt("admin"),
                    Currency = "NPR"
                };
                await RegisterUser(adminUser);
            }
        }
    }
}